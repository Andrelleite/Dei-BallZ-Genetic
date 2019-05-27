using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ValidateCarControl : MonoBehaviour {

	// instances
	public static ValidateCarControl instance = null;
	public Text infoText;
	public bool simulating = false;
	public string savePathBluePlayer;
    public string savePathRedPlayer;
    public GameObject simulationPrefab;
	private GameObject bestSimulation;
	private NeuralNetwork BlueController;
    private NeuralNetwork RedController;
    public int TheTimeScale = 6;

	void Awake(){
		// deal with the singleton part
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy (gameObject);    
		}
		DontDestroyOnLoad(gameObject);
		loadBest ();
		simulating = false;

	}

	void loadBest() {
		if(File.Exists(savePathBluePlayer))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(savePathBluePlayer, FileMode.Open);
			this.BlueController = (NeuralNetwork) bf.Deserialize(file);
			file.Close();
		}

        if (File.Exists(savePathRedPlayer))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePathRedPlayer, FileMode.Open);
            this.RedController = (NeuralNetwork)bf.Deserialize(file);
            file.Close();
        }

    }

	private SimulationInfo createSimulation(int sim_i, Rect location)
	{
		GameObject sim = Instantiate (simulationPrefab, transform.position + new Vector3 (0, 0, (sim_i * 1000)), transform.rotation);
        NeuralController player_script = sim.GetComponentInChildren<NeuralController> ();
		sim.GetComponentInChildren<Camera> ().rect = location;
        NeuralController[] controllers = sim.GetComponentsInChildren<NeuralController>();
        if (controllers[0].enabled)
        {// BluePlayer Controller
            controllers[0].neuralController = BlueController;
            controllers[0].running = true;
        }
        if (controllers.Length > 1 && (controllers[1].enabled || (savePathRedPlayer != null && savePathRedPlayer.Trim().Length != 0)))
        {// RedController Controller
            controllers[1].neuralController = RedController;
            sim.GetComponentsInChildren<CarSimpleController>()[1].enabled = false;
            controllers[1].enabled = true;
            controllers[1].running = true;

        }

        return new SimulationInfo (sim, sim.GetComponentInChildren<NeuralController> (),0);
	}

	void Update () {
		infoText.text = "Showing Individual Found";
		// show best.. in loop
		if (!simulating) {
			SimulationInfo info = createSimulation (0, new Rect (0.0f, 0.0f, 1f, 1f));
			bestSimulation = info.sim;
			Time.timeScale = TheTimeScale;
			simulating = true;

		} else if (simulating) {

			if (!bestSimulation.GetComponentInChildren<NeuralController> ().running && bestSimulation.GetComponentInChildren<NeuralController> ().gameOver) {
				simulating = false;
				Destroy (bestSimulation);
			}
		}
	}
	}




