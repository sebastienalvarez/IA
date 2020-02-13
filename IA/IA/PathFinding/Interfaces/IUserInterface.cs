/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Interface qu'une application utilisant la technique de recherche de chemin de la bibliothèque doit implémenter pour afficher les résultats
 */


namespace IA.PathFinding.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Method to print the result : the path and the distance to the exit
        /// </summary>
        /// <param name="a_path">Path to the exit</param>
        /// <param name="a_distance">Distance to the exit</param>
        void PrintResult(string a_path, double a_distance);
    }
}