/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Classe FactsBase représentant une base de faits
 */


using System;
using System.Collections.Generic;
using System.Linq;
using IA.ExpertSystem.Interfaces;

namespace IA.ExpertSystem.Engine
{
    public class FactsBase
    {
        // PROPRIETE
        /// <summary>
        /// Collection of facts (IFact objects)
        /// </summary>
        public List<IFact> Facts { get; set; } // Collection de Faits

        // CONSTUCTEUR
        /// <summary>
        /// Creates an instance of the FactsBase class
        /// </summary>
        public FactsBase()
        {
            Facts = new List<IFact>();
        }

        // METHODES
        /// <summary>
        /// Gets (if exists) the IFact object in the base with the specified name
        /// </summary>
        /// <param name="a_name">Name of the searched fact</param>
        /// <returns>IFact object in the base with the specified name (or null if not found)</returns>
        public IFact SearchFact(String a_name)
        {
            return Facts.FirstOrDefault(x => x.Name.Equals(a_name));
        }

        /// <summary>
        /// Gets (if exists) the value of the IFact object in the base with the specified name
        /// </summary>
        /// <param name="a_name">Name of the searched fact</param>
        /// <returns>Value of the IFact object in the base with the specified name (or null if not found)</returns>
        public Object GetValueOfFact(string a_name)
        {
            IFact fact = SearchFact(a_name);
            if (fact != null)
            {
                return fact.Value;
            }
            else
            {
                return null;
            }
        }
    }
}