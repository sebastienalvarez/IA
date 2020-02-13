/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Interface pour les classes représentant des faits
 */


using System;

namespace IA.ExpertSystem.Interfaces
{
    /// <summary>
    /// Defines properties needed for a class representing a fact
    /// </summary>
    public interface IFact
    {
        /// <summary>
        /// Name of the fact
        /// </summary>
        String Name { get; } // Nom du Fait
        /// <summary>
        /// Value of the fact
        /// </summary>
        Object Value { get; } // Valeur du Fait
        /// <summary>
        /// Level of the fact (0 is a user entry, >0 is a de deduced fact)
        /// </summary>
        int Level { get; set; } // Niveau du Fait
        /// <summary>
        /// Optional question to ask to the user to get the value
        /// </summary>
        String Question { get; } // Question utilisateur du Fait (peut être vide)
    }
}