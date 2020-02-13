/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Interface IProblem pour les classes représentant un problème d'optimisation
 */


using System.Collections.Generic;

namespace IA.Metaheuristic.Interfaces
{
    public interface IProblem
    {
        /// <summary>
        /// Gets a random solution to initialize the optimization problem 
        /// </summary>
        /// <returns>Random solution</returns>
        ISolution GetRandomSolution();

        /// <summary>
        /// Gets neighboured solutions from a given solution
        /// </summary>
        /// <param name="a_currentSolution">Solution to get neighboured solutions</param>
        /// <returns>Neighboured solutions</returns>
        List<ISolution> GetNeighbourhood(ISolution a_currentSolution);

        /// <summary>
        /// Gets the best solution among a collection of solutions
        /// </summary>
        /// <param name="a_solutions">Collection of solutions</param>
        /// <returns>Best solution</returns>
        ISolution GetBestSolution(List<ISolution> a_solutions);
    }
}