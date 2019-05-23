using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Individual {

	public float[] genotype;
	protected int[] topology;

	protected int totalSize = 1;
	protected float fitness;
	protected bool evaluated;
	protected NeuralNetwork network;

	public int Size
	{
		get { return totalSize;}
	}

	public float Fitness
	{
		get { return fitness;}
		set 
		{
			fitness = value;
			evaluated = true;
		}
	}

	public bool Evaluated
	{
		get { return evaluated;}
	}

	public Individual(int[] topology) {
		foreach(int size in topology)
			totalSize *= size;
		this.topology = topology;
		fitness = 0f;
		evaluated = false;
		genotype = new float[totalSize];
	}


	public NeuralNetwork getIndividualController() {
		network = new NeuralNetwork (topology);
		network.map_from_linear (genotype);
		return network;
	}

	public override string ToString () {
		if (network == null) {
			getIndividualController ();
		}
		string res = "[GeneticIndividual]: [";
		for (int i = 0; i < totalSize; i++) {
			res += genotype [i].ToString ();
			if (i != totalSize - 1) {
				res += ",";
			}
		}
		res += "]\n";
		res += "Neural Network\n" + network.ToString() + "\n";
		return res;

	}


	//override on each specific individual class
	public abstract void Initialize ();
	public abstract void Mutate (float probability);
	public abstract void Crossover (Individual partner, float probability, int pontos);
	public abstract Individual Clone ();
}
