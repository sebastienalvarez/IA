/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe TabuSearchAlgorithmForKnapSack dérivée de la classe TabuSearchAlgorithm pour exemple du problème d'optimisation du sac à dos
 */


using System.Collections.Generic;
using System.Linq;
using IA.Metaheuristic.Algorithms;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class TabuSearchAlgorithmForKnapSack : TabuSearchAlgorithm
    {
        // PROPRIETES
        protected int iterationNumberWithoutUpdate = 1;
        protected int iterationNumber = 1;
        protected const int MAX_ITERATIONS_WHITHOUT_UPDATE = 30;
        protected const int MAX_ITERATIONS = 30;
        protected const int TABU_SEARCH_MAX_SIZE = 50;

        protected List<KnapSackSolution> tabuSolutions = new List<KnapSackSolution>();

        // METHODES
        /// <summary>
        /// Adds the current solution to the tabu collection
        /// </summary>
        /// <param name="a_currentSolution">Current solution</param>
        protected override void AddToTabuList(ISolution a_currentSolution)
        {
            while(tabuSolutions.Count >= TABU_SEARCH_MAX_SIZE)
            {
                tabuSolutions.RemoveAt(0);
            }
            tabuSolutions.Add((KnapSackSolution)a_currentSolution);
        }

        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected override void Increment()
        {
            iterationNumberWithoutUpdate++;
            iterationNumber++;
        }

        /// <summary>
        /// Dtetermines if optimization has to stop
        /// </summary>
        /// <returns>Bool value that indicates if optimization has to stop</returns>
        protected override bool IsDone()
        {
            return iterationNumberWithoutUpdate >= MAX_ITERATIONS_WHITHOUT_UPDATE && iterationNumber >= MAX_ITERATIONS;
        }

        /// <summary>
        /// Removes tabu solutions to neighboured solutions
        /// </summary>
        /// <param name="a_neighbouredSolutions">Neighboured solutions</param>
        /// <returns>Neighboured solutions with tabu solutions removed</returns>
        protected override List<ISolution> RemoveSolutionsInTabuList(List<ISolution> a_neighbouredSolutions)
        {
            return a_neighbouredSolutions.Except(tabuSolutions).ToList();
        }

        /// <summary>
        /// Creates the result for the given optimization problem
        /// </summary>
        protected override void SendResult()
        {
            gui.PrintMessage(bestSoFarSolution.ToString());
        }

        /// <summary>
        /// Updates the solutions if a better solution was found
        /// </summary>
        /// <param name="a_bestSolution">Best solution found for the current iteration</param>
        protected override void UpdateSolution(ISolution a_bestSolution)
        {
            if (!tabuSolutions.Contains((KnapSackSolution)a_bestSolution))
            {
                currentSolution = a_bestSolution;
                AddToTabuList((KnapSackSolution)a_bestSolution);
                if(a_bestSolution.Value > bestSoFarSolution.Value)
                {
                    bestSoFarSolution = a_bestSolution;
                    iterationNumberWithoutUpdate = 0;
                }
            }
        }
    }
}