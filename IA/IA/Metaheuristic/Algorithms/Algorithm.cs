/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite Algorithm représentant un algorithme métaheuristique d'optimisation
 */


using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.Algorithms
{
    public abstract class Algorithm
    {
        // PROPRIETES
        /// <summary>
        /// Object that implements IProblem interface : the optimization problem to resolve
        /// </summary>
        protected IProblem problem;
        /// <summary>
        /// Reference to the application object instance that must implement the IUserInterface
        /// </summary>
        protected IUserInterface gui;


        // METHODES
        /// <summary>
        /// Solves a given optimization problem
        /// </summary>
        /// <param name="a_problem">Object that implements IProblem interface : the optimization problem to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public virtual void Solve(IProblem a_problem, IUserInterface a_gui)
        {
            problem = a_problem;
            gui = a_gui;
        }

        /// <summary>
        /// Creates the result for the given optimization problem
        /// </summary>
        protected abstract void SendResult();
    }
}