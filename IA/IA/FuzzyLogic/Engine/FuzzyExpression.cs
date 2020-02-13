/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe FuzzyExpression représentant une expression floue constituant les prémisses et la conclusion d'une règle flou, c'est-à dire "Nom Variable IS Nom Value"
 */


namespace IA.FuzzyLogic.Engine
{
    public class FuzzyExpression
    {
        // PROPRIETES
        /// <summary>
        /// Linguistic variable of the fuzzy expression
        /// </summary>
        public LinguisticVariable VariableName { get; }
        /// <summary>
        /// Name of the linguistic value of the fuzzy expression
        /// </summary>
        public string ValueName { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the FuzzyExpression class
        /// </summary>
        /// <param name="a_variable">Linguistic variable of the fuzzy expression</param>
        /// <param name="a_value">Name of the linguistic value of the fuzzy expression</param>
        public FuzzyExpression(LinguisticVariable a_variable, string a_value)
        {
            VariableName = a_variable;
            ValueName = a_value;
        }
    }
}