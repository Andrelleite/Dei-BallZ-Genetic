using UnityEngine;
using System.Collections;
using System.Collections.Generic;    

public class HillClimber : MetaHeuristic
{
	public float mutationProbability;

	public override void InitPopulation(){
		population = new List<Individual> ();
		// jncor 
		while (population.Count < populationSize) {
			HillClimberIndividual new_ind= new HillClimberIndividual (topology);
			new_ind.Initialize ();
			population.Add (new_ind);
		}

	}

	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public override void Step()
	{
		List<Individual> new_pop = new List<Individual> ();
			
		updateReport (); //called to get some stats
		// fills the rest with mutations of the best !
		for (int i = 0; i < populationSize ; i++) {
			HillClimberIndividual tmp = (HillClimberIndividual) overallBest.Clone ();
			tmp.Mutate (mutationProbability);
			new_pop.Add (tmp.Clone());
		}

		population = new_pop;

		generation++;
	}

}

