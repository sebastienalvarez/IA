/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Interface qu'une application utilisant la technique des métaheuristiques d'optimisation de la bibliothèque doit implémenter pour afficher les résultats
 */


namespace IA.Metaheuristic.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Method to print the result of the optimization problem
        /// </summary>
        /// <param name="a_message">Message to print</param>
        void PrintMessage(string a_message);
    }
}