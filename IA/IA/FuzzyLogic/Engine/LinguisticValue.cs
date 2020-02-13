/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe LinguisticValue représentant une valeur linguistique, c'est-à-dire un ensemble flou nommé
 */


namespace IA.FuzzyLogic.Engine
{
    public class LinguisticValue
    {
        // PROPRIETES
        /// <summary>
        /// Name of the linguistic value
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Fuzzy set of the linguistic value
        /// </summary>
        public FuzzySet FS { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the LinguisticValue class
        /// </summary>
        /// <param name="a_name">Name of the linguistic value</param>
        /// <param name="a_fs">Fuzzy set of the linguistic value</param>
        public LinguisticValue(string a_name, FuzzySet a_fs)
        {
            Name = a_name;
            FS = a_fs;
        }

        // METHODE
        /// <summary>
        /// Computes Y coordinate for a given X coordinate in the fuzzy set
        /// </summary>
        /// <param name="a_value">X coordinate</param>
        /// <returns>Computed Y coordinate</returns>
        public double DegreeAtValue(double a_value)
        {
            return FS.DegreeAtValue(a_value);
        }
    }
}