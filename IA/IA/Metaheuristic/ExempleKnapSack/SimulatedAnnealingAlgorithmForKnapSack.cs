/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe SimulatedAnnealingAlgorithmForKnapSack dérivée de la classe SimulatedAnnealingAlgorithm pour exemple du problème d'optimisation du sac à dos
 */


using System;
using IA.Metaheuristic.Algorithms;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class SimulatedAnnealingAlgorithmForKnapSack : SimulatedAnnealingAlgorithm
    {
        // PROPRIETES
        protected int iterationNumberWithoutUpdate = 1;
        protected int iterationNumber = 1;
        protected const int MAX_ITERATIONS_WHITHOUT_UPDATE = 30;
        protected const int MAX_ITERATIONS = 100;

        // METHODES
        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected override void Increment()
        {
            iterationNumberWithoutUpdate++;
            iterationNumber++;
        }

        /// <summary>
        /// Inits the temperature
        /// </summary>
        protected override void InitTemperature()
        {
            temperature = 5;
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
        /// Creates the result for the given optimization problem
        /// </summary>
        protected override void SendResult()
        {
            gui.PrintMessage(bestSoFarSolution.ToString());
        }

        /// <summary>
        /// Updates the solution if a better solution was found
        /// </summary>
        /// <param name="a_bestSolution">Best solution found for the current iteration</param>
        protected override void UpdateSolution(ISolution a_bestSolution)
        {
            double threshold = 0.0;
            if(a_bestSolution.Value < currentSolution.Value)
            {
                threshold = Math.Exp(-1 * (currentSolution.Value - a_bestSolution.Value) / currentSolution.Value / temperature);
            }
            if(a_bestSolution.Value > currentSolution.Value || KnapSackProblem.randomGenerator.NextDouble() < threshold)
            {
                currentSolution = a_bestSolution;
                if(a_bestSolution.Value > bestSoFarSolution.Value)
                {
                    bestSoFarSolution = a_bestSolution;
                    iterationNumberWithoutUpdate = 0;
                }
            }
        }

        /// <summary>
        /// Updates the temperature
        /// </summary>
        protected override void UpdateTemperature()
        {
            temperature *= 0.9;
        }
    }
}