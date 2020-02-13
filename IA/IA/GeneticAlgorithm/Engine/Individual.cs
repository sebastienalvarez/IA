/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite Individual : pour un problème donné, la classe représentant un individu doit hériter de cette classe
 */


using System.Collections.Generic;
using System.Text;
using IA.GeneticAlgorithm.Interfaces;

namespace IA.GeneticAlgorithm.Engine
{
    public abstract class Individual
    {
        // PROPRIETES
        protected double fitness;
        /// <summary>
        /// Fitness resultationg of the evaluation of the individual
        /// </summary>
        public double Fitness
        {
            get { return fitness; }
        }

        /// <summary>
        /// Genes constituting the Genome of the individual
        /// </summary>
        public List<IGene> Genome { get; set; }

        // METHODES
        /// <summary>
        /// Mutate method for an individual
        /// </summary>
        public abstract void Mutate();

        /// <summary>
        /// Evaluate method to evaluate the fitness of an individual
        /// </summary>
        /// <returns>Computed fitness</returns>
        public abstract double Evaluate();

        /// <summary>
        /// String in the format "fitness : gene1-gene2-gene3...genex"
        /// </summary>
        /// <returns>String in the format "fitness : gene1-gene2-gene3...genex"</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fitness + " : " + string.Join(" - ", Genome));
            return sb.ToString();
        }
    }
}