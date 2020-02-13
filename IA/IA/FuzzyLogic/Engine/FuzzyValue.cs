/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe FuzzyValue représentant une valeur numérique (abscisse) pour une variable linguistique
 */


namespace IA.FuzzyLogic.Engine
{
    public class FuzzyValue
    {
        // PROPRIETES
        /// <summary>
        /// Linguistic variable
        /// </summary>
        public LinguisticVariable VariableName { get; }
        /// <summary>
        /// Numerical value (X coordinate) of the linguistic variable
        /// </summary>
        public double Value { get; set; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the FuzzyValue class
        /// </summary>
        /// <param name="a_variable">Linguistic variable</param>
        /// <param name="a_value">Numerical value (X coordinate) of the linguistic variable</param>
        public FuzzyValue(LinguisticVariable a_variable, double a_value)
        {
            VariableName = a_variable;
            Value = a_value;
        }

    }
}