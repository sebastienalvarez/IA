/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Interface ISolution pour les classes représentant des solutions du problème d'optimisation
 */


namespace IA.Metaheuristic.Interfaces
{
    public interface ISolution
    {
        /// <summary>
        /// Value of the solution
        /// </summary>
        double Value { get; }
    }
}