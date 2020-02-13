/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe statique pour les paramètres de l'algorithme génétique
 */


using System;

namespace IA.GeneticAlgorithm.Engine
{
    static public class Parameters
    {
        /// <summary>
        /// Default individual number
        /// </summary>
        public static int IndividualsNb = 20;
        /// <summary>
        /// Default maximaum generation number
        /// </summary>
        public static int GenerationMaxNb = 50;
        /// <summary>
        /// Default initial gene number
        /// </summary>
        public static int InitialGenesNb = 10;
        /// <summary>
        /// Default minimum fitness to get (this genetic algorithm searches to minimize the fitness)
        /// </summary>
        public static int MinFitness = 0;
        /// <summary>
        /// Default mutation rate of a gene
        /// </summary>
        public static double MutationsRate = 0.10;
        /// <summary>
        /// Default mutation rate aimed at adding a gene
        /// </summary>
        public static double MutationAddRate = 0.20;
        /// <summary>
        /// Default mutation rate aimed at deleting a gene
        /// </summary>
        public static double MutationDeleteRate = 0.10;
        /// <summary>
        /// Default crossover rate
        /// </summary>
        public static double CrossoverRate = 0.60;
        /// <summary>
        /// Random tool for evolutionary process
        /// </summary>
        public static Random RandomGenerator = new Random();
    }
}