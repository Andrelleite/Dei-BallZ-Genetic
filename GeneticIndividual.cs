using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual
{

    int number_of_crossover = 0;
    public GeneticIndividual(int[] topology) : base(topology)
    {
    }

    public override void Initialize()
    {
        for (int i = 0; i < totalSize; i++)
        {
            genotype[i] = Random.Range(-1.0f, 1.0f);
        }
        Debug.Log(genotype);
    }

    public override void Crossover(Individual partner, float probability, int pontos)
    {
        number_of_crossover++;
        int mid_point;
        int region_to_cross;

        if(Random.Range(0.0f,1.0f) < probability) {

            mid_point = totalSize / 3;
            region_to_cross = mid_point * 2;

            if (number_of_crossover == 1)
            {
                for (int i = mid_point; i <= region_to_cross; i++)
                {
                    genotype[i] = partner.genotype[i];
                }
            }
            else
            {
                for (int i = 0; i <= region_to_cross; i++)
                {
                    genotype[i] = partner.genotype[i];
                }
            }
        }   
    }

    public override void Mutate(float probability)
    {
        for (int i = 0; i < totalSize; i++)
        {
            if (Random.Range(0.0f, 1.0f) < probability)
            {
                genotype[i] = Random.Range(-1.0f, 1.0f);
            }
        }
    }

    public override Individual Clone()
    {
        GeneticIndividual clone = new GeneticIndividual(this.topology);

        genotype.CopyTo(clone.genotype, 0);
        clone.fitness = this.Fitness;
        clone.evaluated = false;

        return clone;
    }

}