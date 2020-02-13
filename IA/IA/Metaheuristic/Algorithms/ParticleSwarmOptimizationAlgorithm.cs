/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite ParticleSwarmOptimizationAlgorithm représentant l'algorithme métaheuristique d'optimisation "Optimisation par essaims particulaires"
 */


 using System.Collections.Generic;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.Algorithms
{
    public abstract class ParticleSwarmOptimizationAlgorithm : Algorithm
    {
        // PROPRIETES
        /// <summary>
        /// Current computed solutions
        /// </summary>
        protected List<ISolution> currentSolutions;
        /// <summary>
        /// Best solution found so far
        /// </summary>
        protected ISolution bestSoFarSolution;
        /// <summary>
        /// Best actual solution
        /// </summary>
        protected ISolution bestActualSolution;
        /// <summary>
        /// Number of individuals in the computed solutions
        /// </summary>
        protected const int INDIVIDUAL_NUMBER = 30;

        // METHODES
        /// <summary>
        /// Solves a given optimization problem
        /// </summary>
        /// <param name="a_problem">Object that implements IProblem interface : the optimization problem to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public override sealed void Solve(IProblem a_problem, IUserInterface a_gui)
        {
            base.Solve(a_problem, a_gui);

            currentSolutions = new List<ISolution>();
            for(int i = 0; i < INDIVIDUAL_NUMBER; i++)
            {
                ISolution newSolution = problem.GetRandomSolution();
                currentSolutions.Add(newSolution);
            }

            bestActualSolution = problem.GetBestSolution(currentSolutions);
            bestSoFarSolution = bestActualSolution;

            while (!IsDone())
            {
                UpdateGeneralVariables();
                UpdateSolutions();
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
        /// Updates the solutions if better solutions were found
        /// </summary>
        protected abstract void UpdateSolutions();

        /// <summary>
        /// Updates general variables
        /// </summary>
        protected abstract void UpdateGeneralVariables();

        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected abstract void Increment();
    }
}