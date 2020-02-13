/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Classe RulesBase représentant une base de règles
 */


using System.Collections.Generic;

namespace IA.ExpertSystem.Engine
{
    public class RulesBase
    {
        // PROPRIETE
        /// <summary>
        /// Collection of rules (Rule objects)
        /// </summary>
        public List<Rule> Rules { get; set; } // Collection de Règles

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the RulesBase class
        /// </summary>
        public RulesBase()
        {
            Rules = new List<Rule>();
        }
    }
}