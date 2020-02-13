/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe KnapSackProblem implémentant l'interface IProblem pour exemple du problème d'optimisation du sac à dos
 */


using System;
using System.Collections.Generic;
using System.Linq;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class KnapSackProblem : IProblem
    {
        // PROPRIETES
        public double MaxWeight { get; set; }
        public static Random randomGenerator = null;
        public const int NEIGHBOUR_NUMBER = 30;
        /// <summary>
        /// Boxes for the given KnapSack problem
        /// </summary>
        public List<Box> Boxes { get; }

        // CONSTRUCTEURS
        /// <summary>
        /// Creates a default instance of the KnapSackProblem class
        /// </summary>
        public KnapSackProblem()
        {
            Boxes = new List<Box>();
            Boxes.Add(new Box("A", 4, 15));
            Boxes.Add(new Box("B", 7, 15));
            Boxes.Add(new Box("C", 10, 20));
            Boxes.Add(new Box("D", 3, 10));
            Boxes.Add(new Box("E", 6, 11));
            Boxes.Add(new Box("F", 12, 16));
            Boxes.Add(new Box("G", 11, 12));
            Boxes.Add(new Box("H", 16, 22));
            Boxes.Add(new Box("I", 5, 12));
            Boxes.Add(new Box("J", 14, 21));
            Boxes.Add(new Box("K", 4, 10));
            Boxes.Add(new Box("L", 3, 7));

            MaxWeight = 20;
            if(randomGenerator == null)
            {
                randomGenerator = new Random();
            }
        }

        /// <summary>
        /// Creates a random instance of the KnapSackProblem class
        /// </summary>
        /// <param name="a_boxNumber">Number of boxes</param>
        /// <param name="a_maxWeight">Maximum Weight for random generation</param>
        /// <param name="a_maxValue">Maximum Value for random generation</param>
        public KnapSackProblem(int a_boxNumber, double a_maxWeight, double a_maxValue)
        {
            Boxes = new List<Box>();
            MaxWeight = a_maxWeight;
            if (randomGenerator == null)
            {
                randomGenerator = new Random();
            }

            for(int i = 0; i < a_boxNumber; i++)
            {
                Boxes.Add(new Box(i.ToString(), randomGenerator.NextDouble() * a_maxWeight, randomGenerator.NextDouble() * a_maxValue));
            }
        }

        // METHODES
        /// <summary>
        /// Gets the best solution among a collection of solutions
        /// </summary>
        /// <param name="a_solutions">Collection of solutions</param>
        /// <returns>Best solution</returns>
        public ISolution GetBestSolution(List<ISolution> a_solutions)
        {
            return a_solutions.Where(x => x.Value.Equals(a_solutions.Max(y => y.Value))).FirstOrDefault();
        }

        /// <summary>
        /// Gets neighboured solutions from a given solution
        /// </summary>
        /// <param name="a_currentSolution">Solution to get neighboured solutions</param>
        /// <returns>Neighboured solutions</returns>
        public List<ISolution> GetNeighbourhood(ISolution a_currentSolution)
        {
            List<ISolution> neighbours = new List<ISolution>();
            List<Box> possibleBoxes = Boxes;

            for(int i =0; i < NEIGHBOUR_NUMBER; i++)
            {
                KnapSackSolution newSolution = new KnapSackSolution((KnapSackSolution)a_currentSolution);
                int index = randomGenerator.Next(0, newSolution.LoadedContent.Count);
                newSolution.LoadedContent.RemoveAt(index);
                double enableSpace = MaxWeight - newSolution.Weight;
                List<Box> availableBoxes = possibleBoxes.Except(newSolution.LoadedContent).Where(x => x.Weight <= enableSpace).ToList();

                while (enableSpace > 0 && availableBoxes.Count != 0)
                {
                    index = randomGenerator.Next(0, availableBoxes.Count);

                    newSolution.LoadedContent.Add(availableBoxes.ElementAt(index));
                    enableSpace = MaxWeight - newSolution.Weight;
                    availableBoxes = possibleBoxes.Except(newSolution.LoadedContent).Where(x => x.Weight <= enableSpace).ToList();
                }
                neighbours.Add(newSolution);
            }
            return neighbours;
        }

        /// <summary>
        /// Gets a random solution to initialize the optimization problem 
        /// </summary>
        /// <returns>Random solution</returns>
        public ISolution GetRandomSolution()
        {
            KnapSackSolution solution = new KnapSackSolution();
            List<Box> possibleBoxes = Boxes;

            double enableSpace = MaxWeight;
            List<Box> availableBoxes = possibleBoxes.Where(x => x.Weight <= enableSpace).ToList();

            while(enableSpace > 0 && availableBoxes.Count != 0)
            {
                int index = randomGenerator.Next(0, availableBoxes.Count);

                solution.LoadedContent.Add(availableBoxes.ElementAt(index));
                enableSpace = MaxWeight - solution.Weight;
                availableBoxes = possibleBoxes.Except(solution.LoadedContent).Where(x => x.Weight <= enableSpace).ToList();
            }
            return solution;
        }
    }
}