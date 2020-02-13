﻿/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite GradientDescentAlgorithm représentant l'algorithme métaheuristique d'optimisation "Descente de gradient"
 */


 using System.Collections.Generic;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.Algorithms
{
    public abstract class GradientDescentAlgorithm : Algorithm
    {
        // PROPRIETES
        /// <summary>
        /// Current computed solution
        /// </summary>
        protected ISolution currentSolution;

        // METHODES
        /// <summary>
        /// Solves a given optimization problem
        /// </summary>
        /// <param name="a_problem">Object that implements IProblem interface : the optimization problem to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public override sealed void Solve(IProblem a_problem, IUserInterface a_gui)
        {
            base.Solve(a_problem, a_gui);

            currentSolution = problem.GetRandomSolution();
            while (!IsDone())
            {
                List<ISolution> neighbours = problem.GetNeighbourhood(currentSolution);
                if(neighbours != null)
                {
                    ISolution bestSolution = problem.GetBestSolution(neighbours);
                    UpdateSolution(bestSolution);
                }
                Increment();
            }
            SendResult();            
        }

        /// <summary>
        /// Dtetermines if optimization has to stop
        /// </summary>
        /// <returns>Bool value that indicates if optimization has to stop</returns>
        protected abstract bool IsDone();

        /// <summary>
        /// Updates the solution if a better solution was found
        /// </summary>
        /// <param name="a_bestSolution">Best solution found for the current iteration</param>
        protected abstract void UpdateSolution(ISolution a_bestSolution);

        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected abstract void Increment();
    }
}