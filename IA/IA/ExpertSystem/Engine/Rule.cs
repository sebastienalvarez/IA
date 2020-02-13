/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Classe Rule représentant une règle
 */

 
using System;
using System.Collections.Generic;
using System.Text;
using IA.ExpertSystem.Interfaces;

namespace IA.ExpertSystem.Engine
{
    public class Rule
    {
        // PROPRIETES
        /// <summary>
        /// Name of the rule
        /// </summary>
        public String Name { get; } // Nom de la Règle
        /// <summary>
        /// Collection of facts (IFact objects) corresponding to premises
        /// </summary>
        public List<IFact> Premises { get; } // Collection de Faits constituants les prémisses
        /// <summary>
        /// IFact object corresponding to the conclusion
        /// </summary>
        public IFact Conclusion { get; } // Fait constituant la conclusion

        // CONSTUCTEUR
        /// <summary>
        /// Creates an instance of the Rule class
        /// </summary>
        /// <param name="a_name">Name of the rule</param>
        /// <param name="a_premises">Collection of IFact objects corresponding to premises</param>
        /// <param name="a_conclusion">IFact object corresponding to the conclusion</param>
        public Rule(String a_name, List<IFact> a_premises, IFact a_conclusion)
        {
            Name = a_name;
            Premises = new List<IFact>(a_premises);
            Conclusion = a_conclusion;
        }

        // METHODE
        /// <summary>
        /// String in the format "Règle Nom de la règle : SI Nom du fait 1 = vrai ET Nom du fait 2 = valeur entière ALORS Nom du fait"
        /// </summary>
        /// <returns>String in the format "Règle Nom de la règle : SI Nom du fait 1 = vrai ET Nom du fait 2 = valeur entière ALORS Nom du fait"</returns>
        public override string ToString()
        {
            StringBuilder rule = new StringBuilder();
            rule.Append("Règle " + Name + " : SI ");
            for (int i = 0; i < Premises.Count; i++)
            {
                if (i < Premises.Count - 1)
                {
                    if (Premises[i].GetType().Equals(typeof(BoolFact)))
                    {
                        if ((bool)Premises[i].Value)
                        {
                            rule.Append(Premises[i].Name + " = vrai ET ");
                        }
                        else
                        {
                            rule.Append(Premises[i].Name + " = faux ET ");
                        }
                    }
                    else
                    {
                        rule.Append(Premises[i].Name + " = " + Premises[i].Value + "  ET ");
                    }
                }
                else
                {
                    if (Premises[i].GetType().Equals(typeof(BoolFact)))
                    {
                        if ((bool)Premises[i].Value)
                        {
                            rule.Append(Premises[i].Name + " = vrai");
                        }
                        else
                        {
                            rule.Append(Premises[i].Name + " = faux");
                        }
                    }
                    else
                    {
                        rule.Append(Premises[i].Name + " = " + Premises[i].Value);
                    }
                }
            }
            return rule.ToString() + " ALORS " + Conclusion.Name;
        }
    }
}