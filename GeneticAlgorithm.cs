using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic
{

    public float mutationProbability;
    public float crossoverProbability;
    public int tournamentSize;
    public bool elitist;
    public bool tournament;
    public int elitism_card;
    public int pontos;
    SelectionMethod selector;

    public override void InitPopulation()
    {

        population = new List<Individual>();
        while (population.Count < populationSize)
        {
            GeneticIndividual gene = new GeneticIndividual(topology);
            gene.Initialize();
            population.Add(gene);
        }
        Debug.Log("-------------------------------INITIALIZATION END -------------------------------");

    }

    //The Step function assumes that the fitness values of all the individuals in the population have been calculated.
    public override void Step()
    {

        List<Individual> Mutated_population = new List<Individual>();
        Individual first_partner;
        Individual second_partner;
        Individual individual;
        int gene_count;

        updateReport();



        if (generation < numGenerations)
        {
            Debug.Log("-------------------------------SELECTION START -------------------------------");

            //Selection
            if (!elitist)
            {
                elitism_card = 0;
            }

            Debug.Log(tournament);

            if (tournament)
            {
                Mutated_population = selection_tornament.selectIndividuals(population, populationSize - elitism_card, tournamentSize);
            }
            else
            {

                for (int i = 0; i < populationSize-elitism_card; i++)
                {
                    individual = population[Random.Range(0, populationSize)];
                    while (Mutated_population.Contains(individual))
                    {
                        individual = population[Random.Range(0, populationSize)];
                    }
                    Mutated_population.Add(individual.Clone());
                }

            }


            Debug.Log("-------------------------------CROSSOVER START -------------------------------");


            gene_count = Mutated_population.Count;
            
            //2-point crossover to a new gene
            for (int i = 0; i < populationSize - elitism_card; i+=2)
            {

                first_partner = Mutated_population[i];

                if(gene_count > 1)
                {
                    second_partner = Mutated_population[i];
                    first_partner.Crossover(second_partner, crossoverProbability, pontos);
                    gene_count -= 2;
                }

            }

            Debug.Log("-------------------------------MUTATION START -------------------------------");

            //Random gene Mutation
            for (int i = 0; i < populationSize - elitism_card; i++)
            {
                Mutated_population[i].Mutate(mutationProbability);
            }

            Debug.Log("-------------------------------ELITISM START -------------------------------");

            //Elitismo
            if (elitist)
            {
                population.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
                for(int i = 0; i < elitism_card; i++)
                {
                    Mutated_population.Add(population[i].Clone());
                }
            }

            population = Mutated_population;
            generation++;
        }

        
    }

}
