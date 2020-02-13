/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface pour les classes représentant des gènes
 */


namespace IA.GeneticAlgorithm.Interfaces
{
    public interface IGene
    {
        /// <summary>
        /// Mutate method for a gene type class
        /// </summary>
        void Mutate();
    }
}