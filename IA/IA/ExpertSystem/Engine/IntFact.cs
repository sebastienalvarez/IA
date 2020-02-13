/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Classe IntFact représentant un fait dont la valeur est booléenne
 */


using System;

namespace IA.ExpertSystem.Engine
{
    public class IntFact : Interfaces.IFact
    {
        // PROPRIETES
        /// <summary>
        /// Name of the fact
        /// </summary>
        public String Name { get; } // Nom du Fait
        /// <summary>
        /// Value of the fact
        /// </summary>
        public Object Value { get; } // Valeur du Fait
        /// <summary>
        /// Level of the fact (0 is a user entry, >0 is a deduced fact)
        /// </summary>
        public int Level { get; set; } // Niveau du Fait
        /// <summary>
        /// Optional question to ask to the user to get the value
        /// </summary>
        public String Question { get; } // Question utilisateur du Fait (peut être vide)

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the IntFact class
        /// </summary>
        /// <param name="a_name">Name of the fact</param>
        /// <param name="a_value">Value of the fact</param>
        /// <param name="a_level">Level of the fact (0 is a user entry, >0 is a deduced fact)</param>
        /// <param name="a_question">Optional question to ask to the user to get the value (in this case set a_level argument to 0)</param>
        public IntFact(String a_name, int a_value, int a_level, string a_question)
        {
            Name = a_name;
            Value = a_value;
            Level = a_level;
            Question = a_question;
        }

        // METHODE
        /// <summary>
        /// String in the format "Nom du fait = valeur entière, niveau : 0"
        /// </summary>
        /// <returns>String in the format "Nom du fait = valeur entière, niveau : 0"</returns>
        public override string ToString()
        {
            return "Fait " + Name + " = " + ((int)Value).ToString() + ", niveau : " + Level.ToString();
        }
    }
}