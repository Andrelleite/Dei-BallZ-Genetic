using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TournamentSelection : SelectionMethod
{
    public TournamentSelection(): base() { 
    }

    public override List<Individual> selectIndividuals(List<Individual> oldpop, int num, int tournamentSize)
    {
        return tournament_selection(oldpop, num, tournamentSize);
    }

    public List<Individual> tournament_selection(List<Individual> oldpop, int num, int tournamentSize)
    {
        Individual best_gene;
        Individual individual;
        List<Individual> bad_generation;
        List<Individual> homo_novus = new List<Individual>();
        int size;

        Debug.Log("-------------------------------SELECTION TOURNAMENT -------------------------------");


        for (int j = 0; j < num; j++)
        {
            bad_generation = new List<Individual>();

            size = oldpop.Count;


            for (int i = 0; i < tournamentSize; i++)
            {
                individual = oldpop[Random.Range(0, size)];
                while (bad_generation.Contains(individual))
                {
                    individual = oldpop[Random.Range(0, size)];
                }
                bad_generation.Add(individual.Clone());
            }

            best_gene = bad_generation[0];

            for (int i = 0; i < bad_generation.Count; i++)
            {
                if (bad_generation[i].Fitness > best_gene.Fitness)
                {
                    best_gene = bad_generation[i];
                }
            }

            homo_novus.Add(best_gene.Clone());

        }
        Debug.Log("-------------------------------SELECTION FINAL STAGE-------------------------------");
        Debug.Log(homo_novus[0]);
        return homo_novus;
    }
}
    




