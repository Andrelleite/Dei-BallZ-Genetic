using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public abstract class MetaHeuristic : MonoBehaviour
{
	public int populationSize;
	public int[] topology;
	public int numGenerations;
	[HideInInspector] public int generation;
	[HideInInspector] public string logFilename;

	protected List<Individual> population;
	protected int evaluatedIndividuals;
	protected string report = "Generation,PopBest,PopAvg,BestOverall\n";
	protected string best = "";
	protected SelectionMethod selection;
    protected SelectionMethod selection_tornament;


	public Individual overallBest{ get; set;}

	public List<Individual> Population
	{
		get
		{
			return population;
		}
	}
		

	public Individual GenerationBest
	{
		get
		{
			float max = float.MinValue;
			Individual max_ind = null;
			foreach (Individual indiv in population) {
				if (indiv.Fitness > max) {
					max = indiv.Fitness;
					max_ind = indiv;
				}
			}
			return max_ind;
		}
	}

	public float PopAvg
	{
		get
		{
			float sum = 0.0f;
			foreach (Individual indiv in population) {
				sum += indiv.Fitness;
			}
			return (sum / populationSize);
		}
	}

	void Start()
	{
		generation = 0;
        selection_tornament = new TournamentSelection();
    }

	//You have to implement these 2 methods
	public abstract void InitPopulation ();
	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public abstract void Step();


	public void updateReport() {
		if (overallBest == null || overallBest.Fitness < GenerationBest.Fitness) {
			overallBest = GenerationBest.Clone();
		}
		float populationBest = GenerationBest.Fitness;
		best = overallBest.ToString();
		report +=  string.Format("{0},{1},{2},{3}\n", generation,populationBest, PopAvg, overallBest.Fitness);
		Debug.Log (report);
		Debug.Log (best);
	}

	private void writeToFile(string path, string data) {
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(data);
		writer.Close();
	}


	public void dumpStats() {
		if (!BatchmodeConfig.batchmode) {
			System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
			writeToFile (string.Format ("EvolutionaryStatistics_{0}.csv", cur_time), report);
			writeToFile(string.Format ("EvolutionaryRunBest_{0}.txt", cur_time), best);
			dumpOverallBest(string.Format ("Best_{0}.dat", cur_time));
		} else {
			writeToFile (string.Format ("{0}.csv", logFilename), report);
			writeToFile(string.Format ("{0}.txt", logFilename), best);
			dumpOverallBest(string.Format ("Best_{0}.dat", logFilename));
		}

		Debug.Log (report);
	}

	public void dumpOverallBest(string path) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(path);
		bf.Serialize(file, overallBest.getIndividualController());
		file.Close();
	
	}



}

