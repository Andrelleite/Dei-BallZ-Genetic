using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillClimberIndividual : Individual {

	public HillClimberIndividual(int[] topology) : base(topology) {
		
	}

	public override void Initialize () {
		for (int i = 0; i < totalSize; i++) {
			genotype[i] = Random.Range (-1.0f, 1.0f);
		}
        Debug.Log("-----------------------------------------------------------------"+ totalSize);
	}

	public override void Mutate (float probability)
	{
		for (int i = 0; i < totalSize; i++) {
			if (Random.Range (0.0f, 1.0f) < probability) {
				genotype [i] = Random.Range (-1.0f, 1.0f);
			}
		}
	}

	public override void Crossover (Individual partner, float probability, int pontos)
	{
		throw new System.NotImplementedException ();
	}

	public override Individual Clone ()
	{
		HillClimberIndividual new_ind = new HillClimberIndividual(this.topology);
		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.Fitness;
		new_ind.evaluated = false;
		return new_ind;
	}
}
