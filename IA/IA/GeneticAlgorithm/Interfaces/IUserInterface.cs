/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface qu'une application utilisant la technique de l'algorithme génétique de la bibliothèque doit implémenter pour présenter à l'utilisateur le résultat
 */


using IA.GeneticAlgorithm.Engine;

namespace IA.GeneticAlgorithm.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Print method to display best individual provided by evolutionary process
        /// </summary>
        /// <param name="a_individual">Best individual, it is an object from an inherited class from Individual class</param>
        /// <param name="a_generation">Generation number of the evolutionary process</param>
        void PrintBestIndividual(Individual a_individual, int a_generation);
    }
}