/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe GradientDescentAlgorithmForKnapSack dérivée de la classe GradientDescentAlgorithm pour exemple du problème d'optimisation du sac à dos
 */


using IA.Metaheuristic.Algorithms;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class GradientDescentAlgorithmForKnapSack : GradientDescentAlgorithm
    {
        // PROPRIETES
        protected int iterationNumberWithoutUpdate = 0;
        protected const int MAX_ITERATIONS_WHITHOUT_UPDATE = 50;

        // METHODES
        /// <summary>
        /// Updates criteria that determine if the optimization has to stop
        /// </summary>
        protected override void Increment()
        {
            iterationNumberWithoutUpdate++;
        }

        /// <summary>
        /// Dtetermines if optimization has to stop
        /// </summary>
        /// <returns>Bool value that indicates if optimization has to stop</returns>
        protected override bool IsDone()
        {
            return iterationNumberWithoutUpdate >= MAX_ITERATIONS_WHITHOUT_UPDATE;
        }

        /// <summary>
        /// Creates the result for the given optimization problem
        /// </summary>
        protected override void SendResult()
        {
            gui.PrintMessage(currentSolution.ToString());
        }

        /// <summary>
        /// Updates the solution if a better solution was found
        /// </summary>
        /// <param name="a_bestSolution">Best solution found for the current iteration</param>
        protected override void UpdateSolution(ISolution a_bestSolution)
        {
            if(a_bestSolution.Value > currentSolution.Value)
            {
                currentSolution = a_bestSolution;
                iterationNumberWithoutUpdate = 0;
            }
        }
    }
}