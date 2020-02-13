/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe ParticleSwarmOptimizationAlgorithmForKnapSack dérivée de la classe ParticleSwarmOptimizationAlgorithm pour exemple du problème d'optimisation du sac à dos
 */


using System.Collections.Generic;
using System.Linq;
using IA.Metaheuristic.Algorithms;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class ParticleSwarmOptimizationAlgorithmForKnapSack : ParticleSwarmOptimizationAlgorithm
    {
        // PROPRIETES
        protected int iterationNumber = 1;
        protected const int MAX_ITERATIONS = 200;

        // METHODES
        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected override void Increment()
        {
            iterationNumber++;
        }

        /// <summary>
        /// Dtetermines if optimization has to stop
        /// </summary>
        /// <returns>Bool value that indicates if optimization has to stop</returns>
        protected override bool IsDone()
        {
            return iterationNumber >= MAX_ITERATIONS;
        }

        /// <summary>
        /// Creates the result for the given optimization problem
        /// </summary>
        protected override void SendResult()
        {
            gui.PrintMessage(bestSoFarSolution.ToString());
        }

        /// <summary>
        /// Updates general variables
        /// </summary>
        protected override void UpdateGeneralVariables()
        {
            bestActualSolution = currentSolutions.OrderByDescending(x => x.Value).FirstOrDefault();
            if(bestActualSolution.Value > bestSoFarSolution.Value)
            {
                bestSoFarSolution = bestActualSolution;
            }
        }

        /// <summary>
        /// Updates the solutions if better solutions were found
        /// </summary>
        protected override void UpdateSolutions()
        {
            List<Box> possibleBoxes = ((KnapSackProblem)problem).Boxes;

            foreach(ISolution genericSolution in currentSolutions)
            {
                KnapSackSolution solution = (KnapSackSolution)genericSolution;
                if (!solution.Equals(bestSoFarSolution))
                {
                    AddElementFromBestActualSolution(solution);
                    AddElementFromBestSoFarSolution(solution);

                    while(solution.Weight > ((KnapSackProblem)problem).MaxWeight)
                    {
                        RemoveElement(solution);
                    }
                    AddElementsUntilMaxWeight(possibleBoxes, solution);
                }
            }
        }

        /// <summary>
        /// Adds a random box from bestActualSolution to the given solution
        /// </summary>
        /// <param name="a_solution">Solution to modify</param>
        protected void AddElementFromBestActualSolution(KnapSackSolution a_solution)
        {
            int index = KnapSackProblem.randomGenerator.Next(0, ((KnapSackSolution)bestActualSolution).LoadedContent.Count);
            Box element = ((KnapSackSolution)bestActualSolution).LoadedContent.ElementAt(index);
            if (!a_solution.LoadedContent.Contains(element))
            {
                a_solution.LoadedContent.Add(element);
            }
        }

        /// <summary>
        /// Adds a random box from bestSoFarSolution to the given solution
        /// </summary>
        /// <param name="a_solution">Solution to modify</param>
        protected void AddElementFromBestSoFarSolution(KnapSackSolution a_solution)
        {
            int index = KnapSackProblem.randomGenerator.Next(0, ((KnapSackSolution)bestSoFarSolution).LoadedContent.Count);
            Box element = ((KnapSackSolution)bestSoFarSolution).LoadedContent.ElementAt(index);
            if (!a_solution.LoadedContent.Contains(element))
            {
                a_solution.LoadedContent.Add(element);
            }
        }

        /// <summary>
        /// Removes a random box from the given solution
        /// </summary>
        /// <param name="a_solution">Solution to modify</param>
        protected void RemoveElement(KnapSackSolution a_solution)
        {
            int index = KnapSackProblem.randomGenerator.Next(0, a_solution.LoadedContent.Count);
            a_solution.LoadedContent.RemoveAt(index);
        }

        /// <summary>
        /// Adds random boxes to the given solution until max weight
        /// </summary>
        /// <param name="a_possibleBoxes">Possible boxes to add0/param>
        /// <param name="a_solution">Solution to modify</param>
        protected void AddElementsUntilMaxWeight(List<Box> a_possibleBoxes, KnapSackSolution a_solution)
        {
            double enableSpace = ((KnapSackProblem)problem).MaxWeight - a_solution.Weight;

            List<Box> availableBoxes = a_possibleBoxes.Except(a_solution.LoadedContent).Where(x => x.Weight <= enableSpace).ToList();

            while(enableSpace > 0 && availableBoxes.Count != 0)
            {
                int index = KnapSackProblem.randomGenerator.Next(0, availableBoxes.Count);
                a_solution.LoadedContent.Add(availableBoxes.ElementAt(index));
                enableSpace = ((KnapSackProblem)problem).MaxWeight - a_solution.Weight;
                availableBoxes = a_possibleBoxes.Except(a_solution.LoadedContent).Where(x => x.Weight <= enableSpace).ToList();
            }
        }
    }
}