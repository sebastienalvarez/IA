/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe FuzzyRule représentant une règle floue
 */


using System;
using System.Collections.Generic;

namespace IA.FuzzyLogic.Engine
{
    public class FuzzyRule
    {
        // PROPRIETES
        public List<FuzzyExpression> Premises { get; }
        public FuzzyExpression Conclusion { get; }

        // CONSTRUCTEUR
        public FuzzyRule(List<FuzzyExpression> a_premises, FuzzyExpression a_conclusion)
        {
            Premises = a_premises;
            Conclusion = a_conclusion;
        }

        // METHODE PUBLIQUE
        /// <summary>
        /// Check if the fuzzy rule applies : if yes returns a new FuzzySet object corresponding to the conclusion with max degree value equals to the max degree value among premises
        /// </summary>
        /// <param name="a_problem">FuzzyValue collections of a given problem</param>
        /// <returns>FuzzySet object corresponding to the conclusion with max degree value equals to the max degree value among premises</returns>
        public FuzzySet Apply(List<FuzzyValue> a_problem)
        {
            double ruleDegree = 1;
            foreach(FuzzyExpression localPremise in Premises)
            {
                double localDegree = SearchAndComputePremiseDegree(a_problem, localPremise);
                if(localDegree == double.NaN)
                {
                    return null;
                }
                ruleDegree = Math.Min(ruleDegree, localDegree);
            }
            return Conclusion.VariableName.LinguisticValueByName(Conclusion.ValueName).FS * ruleDegree;
        }

        // METHODE PRIVEE
        /// <summary>
        /// Search and compute the premise degree for given problem : if premise is applicable returns the degree, if not returns double.Nan
        /// </summary>
        /// <param name="a_problem">FuzzyValue collections of a given problem</param>
        /// <param name="a_localPremise">Premise to check</param>
        /// <returns>Degree of the premise or double.Nan if premise not applicable</returns>
        protected double SearchAndComputePremiseDegree(List<FuzzyValue> a_problem, FuzzyExpression a_localPremise)
        {
            List<FuzzyValue>.Enumerator enumerator = a_problem.GetEnumerator();
            LinguisticValue linguisticValue = null;
            while(enumerator.MoveNext())
            {
                if(a_localPremise.VariableName == enumerator.Current.VariableName)
                {
                    linguisticValue = a_localPremise.VariableName.LinguisticValueByName(a_localPremise.ValueName);
                    if(linguisticValue != null)
                    {
                        return linguisticValue.DegreeAtValue(enumerator.Current.Value);
                    }
                    else
                    {
                        return double.NaN;
                    }
                }
            }
            return double.NaN;
        }
    }
}