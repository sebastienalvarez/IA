/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe TSPIndividual pour exemple du problème TSP
 */


 using System.Collections.Generic;
using System.Linq;
using IA.GeneticAlgorithm.Engine;

namespace IA.GeneticAlgorithm.ExempleTSP
{
    public class TSPIndividual : Individual
    {
        public TSPIndividual()
        {
            Genome = new List<Interfaces.IGene>();
            List<City> cities = TSP.GetCities();
            while (cities.Count != 0)
            {
                int index = Parameters.RandomGenerator.Next(cities.Count);
                Genome.Add(new TSPGene(cities.ElementAt(index)));
                cities.RemoveAt(index);
            }
        }

        public TSPIndividual(TSPIndividual a_father)
        {
            Genome = new List<Interfaces.IGene>();
            foreach (TSPGene gene in a_father.Genome)
            {
                Genome.Add(new TSPGene(gene));
            }
            Mutate();
        }

        public TSPIndividual(TSPIndividual a_father, TSPIndividual a_mother)
        {
            Genome = new List<Interfaces.IGene>();
            int cuttingPoint = Parameters.RandomGenerator.Next(a_father.Genome.Count);
            foreach (TSPGene gene in a_father.Genome.Take(cuttingPoint))
            {
                Genome.Add(new TSPGene(gene));
            }
            foreach (TSPGene gene in a_mother.Genome)
            {
                if (!Genome.Contains(gene))
                {
                    Genome.Add(gene);
                }
            }
            Mutate();
        }

        public override double Evaluate()
        {
            int totalKm = 0;
            TSPGene oldGene = null;
            foreach (TSPGene gene in Genome)
            {
                if (oldGene != null)
                {
                    totalKm += gene.GetDistance(oldGene);
                }
                oldGene = gene;
            }
            totalKm += oldGene.GetDistance((TSPGene)Genome.FirstOrDefault());
            fitness = totalKm;
            return fitness;
        }

        public override void Mutate()
        {
            if (Parameters.RandomGenerator.NextDouble() < Parameters.MutationsRate)
            {
                int index1 = Parameters.RandomGenerator.Next(Genome.Count);
                TSPGene gene = (TSPGene)Genome.ElementAt(index1);
                Genome.RemoveAt(index1);
                int index2 = Parameters.RandomGenerator.Next(Genome.Count);
                Genome.Insert(index2, gene);
            }
        }
    }
}