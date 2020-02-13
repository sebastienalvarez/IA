/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe IndividualFactory avec design pattern Singleton, cette classe permet la fabrique des individus
 */


using System.Collections.Generic;
using System.Linq;
using IA.GeneticAlgorithm.Interfaces;

namespace IA.GeneticAlgorithm.Engine
{
    public class EvolutionaryProcess
    {
        // PROPRIETES
        protected List<Individual> population;
        protected int generationNb = 0;
        protected IUserInterface program = null;
        protected string problem;
        private double bestFitness;
        private Individual bestIndividual;
        List<Individual> newGeneration;

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the EvolutionaryProcess class
        /// </summary>
        /// <param name="a_program">Reference to the application object instance that must implement the IUserInterface</param>
        /// <param name="a_problem">Problem type</param>
        public EvolutionaryProcess(IUserInterface a_program, string a_problem)
        {
            program = a_program;
            problem = a_problem;
            IndividualFactory.GetInstance().Init(problem);
            population = new List<Individual>();
            for (int i = 0; i < Parameters.IndividualsNb; i++)
            {
                population.Add(IndividualFactory.GetInstance().GetIndividual(problem));
            }
        }

        // METHODE PUBLIQUE
        /// <summary>
        /// Solves a given problem
        /// </summary>
        public void Run()
        {
            bestFitness = Parameters.MinFitness + 1;
            while (generationNb < Parameters.GenerationMaxNb && bestFitness > Parameters.MinFitness)
            {
                foreach (var ind in population)
                {
                    ind.Evaluate();
                }

                bestIndividual = population.OrderBy(x => x.Fitness).FirstOrDefault();
                program.PrintBestIndividual(bestIndividual, generationNb);
                bestFitness = bestIndividual.Fitness;
                newGeneration = new List<Individual>();
                newGeneration.Add(bestIndividual); // Elitisme : on garde le meilleur pour la génération suivante
                for (int i = 0; i < Parameters.IndividualsNb - 1; i++)
                {
                    Reproduction();
                }

                Survival(newGeneration);
                generationNb++;
            }
        }

        // METHODES PRIVEES
        /// <summary>
        /// Selection of individuals during Survival step
        /// </summary>
        /// <param name="a_newGeneration">New generation population</param>
        protected void Survival(List<Individual> a_newGeneration)
        {
            // Survie : Remplacement total de la population
            population = a_newGeneration;
        }

        /// <summary>
        /// Selection of individuals for reproduction
        /// </summary>
        /// <returns>Selected individual</returns>
        protected Individual Selection()
        {
            // Roulette biaisée sur le rang
            int totalRanks = Parameters.IndividualsNb * (Parameters.IndividualsNb - 1) / 2;
            int rand = Parameters.RandomGenerator.Next(totalRanks);

            int index = 0;
            int nbParts = Parameters.IndividualsNb;
            int totalParts = 0;

            while (totalParts < rand)
            {
                index++;
                totalParts += nbParts;
                nbParts--;
            }
            return population.OrderBy(x => x.Fitness).ElementAt(index);
        }

        /// <summary>
        /// Reproduction step
        /// </summary>
        protected void Reproduction()
        {
            bool twoParents = Parameters.RandomGenerator.NextDouble() < Parameters.CrossoverRate;
            if (twoParents)
            {
                Individual father = Selection();
                Individual mother = Selection();
                newGeneration.Add(IndividualFactory.GetInstance().GetIndividual(problem, father, mother));
            }
            else
            {
                Individual father = Selection();
                newGeneration.Add(IndividualFactory.GetInstance().GetIndividual(problem, father));
            }
        }
    }
}