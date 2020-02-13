/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite GreedyAlgorithm représentant l'algorithme métaheuristique d'optimisation "Glouton"
 */


using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.Algorithms
{
    public abstract class GreedyAlgorithm : Algorithm
    {
        // METHODES
        /// <summary>
        /// Solves a given optimization problem
        /// </summary>
        /// <param name="a_problem">Object that implements IProblem interface : the optimization problem to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public override sealed void Solve(IProblem a_problem, IUserInterface a_gui)
        {
            base.Solve(a_problem, a_gui);
            ConstructSolution();
            SendResult();
        }

        /// <summary>
        /// Constructs the solution element by element
        /// </summary>
        protected abstract void ConstructSolution();
    }
}