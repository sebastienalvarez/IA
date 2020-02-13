/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite SimulatedAnnealingAlgorithm représentant l'algorithme métaheuristique d'optimisation "Recuit simulé"
 */


using System.Collections.Generic;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.Algorithms
{
    public abstract class SimulatedAnnealingAlgorithm : Algorithm
    {
        // PROPRIETES
        /// <summary>
        /// Current computed solution
        /// </summary>
        protected ISolution currentSolution;
        /// <summary>
        /// Best solution found so far
        /// </summary>
        protected ISolution bestSoFarSolution;
        /// <summary>
        /// Temperature
        /// </summary>
        protected double temperature;

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
            bestSoFarSolution = currentSolution;

            InitTemperature();
            while (!IsDone())
            {
                List<ISolution> neighbours = problem.GetNeighbourhood(currentSolution);
                if(neighbours != null)
                {
                    ISolution bestSolution = problem.GetBestSolution(neighbours);
                    UpdateSolution(bestSolution);
                }
                Increment();
                UpdateTemperature();
            }
            SendResult();
        }

        /// <summary>
        /// Inits the temperature
        /// </summary>
        protected abstract void InitTemperature();

        /// <summary>
        /// Updates the temperature
        /// </summary>
        protected abstract void UpdateTemperature();

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