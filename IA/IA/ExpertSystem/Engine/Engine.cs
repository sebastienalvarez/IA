/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : Version 2.0 de Février 2019 => modifications legères pour ajouter la possibilité d'ajouter des faits connus avant résolution du problème d'identification
 * 
 * Description : Classe Engine fournissant le moteur d'inférence à chainage avant
 */


using System;
using System.Collections.Generic;
using IA.ExpertSystem.Interfaces;

namespace IA.ExpertSystem.Engine
{
    public class Engine
    {
        // CHAMPS
        protected RulesBase rulesBase;
        protected FactsBase initialFactsBase;
        protected FactsBase factsBase;
        protected IUserInterface ihm;
        protected int currentLevel = -1;

        // PROPRIETE
        /// <summary>
        /// Rules base : collection of rules for a given identification problem
        /// </summary>
        public RulesBase RulesBase
        {
            get { return rulesBase; }
            set { rulesBase = value; }
        }

        // CONSTRUCTEURS
        /// <summary>
        /// Creates an instance of the Engine class
        /// </summary>
        /// <param name="a_ihm">Reference to the application object instance that must implement the IUserInterface</param>
        /// <param name="a_rulesBase">RulesBase instance for the given identification problem</param>
        /// <param name="a_facts">IFact collection of already known facts for the given identification problem</param>
        public Engine(IUserInterface a_ihm, RulesBase a_rulesBase, List<IFact> a_facts)
        {
            if (a_rulesBase != null)
            {
                rulesBase = a_rulesBase;
            }
            else
            {
                rulesBase = new RulesBase();
            }
            if(a_facts != null && a_facts.Count != 0)
            {
                initialFactsBase = new FactsBase();
                initialFactsBase.Facts = new List<IFact>(a_facts);
                factsBase = new FactsBase();
                factsBase.Facts = new List<IFact>(initialFactsBase.Facts);
            }
            else
            {
                initialFactsBase = null;
                factsBase = new FactsBase();
            }
            ihm = a_ihm;
        }
        /// <summary>
        /// Creates an instance of the Engine class
        /// </summary>
        /// <param name="a_ihm">Reference to the application object instance that must implement the IUserInterface</param>
        /// <param name="a_rulesBase">RulesBase instance for the given identification problem</param>
        /// <param name="a_factsBase">FactsBase instance including already known facts for the given identification problem</param>
        public Engine(IUserInterface a_ihm, RulesBase a_rulesBase, FactsBase a_factsBase)
        {
            if (a_rulesBase != null)
            {
                rulesBase = a_rulesBase;
            }
            else
            {
                rulesBase = new RulesBase();
            }
            if (a_factsBase != null && a_factsBase.Facts.Count != 0)
            {
                initialFactsBase = a_factsBase;
                factsBase = new FactsBase();
                factsBase.Facts = new List<IFact>(initialFactsBase.Facts);
            }
            else
            {
                initialFactsBase = null;
                factsBase = new FactsBase();
            }

            ihm = a_ihm;
        }
        /// <summary>
        /// Creates an instance of the Engine class
        /// </summary>
        /// <param name="a_ihm">Reference to the application object instance that must implement the IUserInterface</param>
        /// <param name="a_rulesBase">RulesBase instance for the given identification problem</param>
        public Engine(IUserInterface a_ihm, RulesBase a_rulesBase)
        {
            if(a_rulesBase != null)
            {
                rulesBase = a_rulesBase;
            }
            else
            {
                rulesBase = new RulesBase();
            }
            initialFactsBase = null;
            factsBase = new FactsBase();
            ihm = a_ihm;
        }
        /// <summary>
        /// Creates an instance of the Engine class
        /// </summary>
        /// <param name="a_ihm">Reference to the application object instance that must implement the IUserInterface</param>
        public Engine(IUserInterface a_ihm)
        {
            rulesBase = new RulesBase();
            initialFactsBase = null;
            factsBase = new FactsBase();
            ihm = a_ihm;
        }

        // METHODES
        /// <summary>
        /// Sets new already known facts in the facts base for a new identification problem, these known facts replace former already known facts (if exist)
        /// </summary>
        /// <param name="a_factsBase">FactsBase object including already known facts</param>
        public void SetFactsBase(FactsBase a_factsBase)
        {
            if(a_factsBase != null && a_factsBase.Facts.Count != 0)
            {
                initialFactsBase = new FactsBase();
                initialFactsBase.Facts = new List<IFact>(a_factsBase.Facts);
            }
        }

        /// <summary>
        /// Sets new already known facts in the facts base for a new identification problem, these known facts replace former already known facts (if exist)
        /// </summary>
        /// <param name="a_factsBase">IFact object collection of already known facts</param>
        public void SetFactsBase(List<IFact> a_facts)
        {
            if (a_facts != null && a_facts.Count != 0)
            {
                initialFactsBase = new FactsBase();
                foreach(var fact in a_facts)
                {
                    initialFactsBase.Facts.Add(fact);
                }
            }
        }

        /// <summary>
        /// Calls the AskIntValue method defined in the application using the IA library that must implement IUserInterface
        /// </summary>
        /// <param name="a_question">Question from an IntFact object</param>
        /// <returns></returns>
        protected int AskIntValue(String a_question)
        {
            return ihm.AskIntValue(a_question);
        }

        /// <summary>
        /// Calls the AskBoolValue method defined in the application using the IA library that must implement IUserInterface
        /// </summary>
        /// <param name="a_question">Question from a BoolFact object</param>
        /// <returns></returns>
        protected bool AskBoolValue(String a_question)
        {
            return ihm.AskBoolValue(a_question);
        }

        /// <summary>
        /// Creates a new fact from a user response to a fact question
        /// </summary>
        /// <param name="a_fact">Ifact object with a valid question</param>
        /// <returns></returns>
        protected IFact CreateFact(IFact a_fact)
        {
            IFact newFact;
            if (a_fact.GetType().Equals(typeof(IntFact)))
            {
                int value = AskIntValue(a_fact.Question);
                newFact = new IntFact(a_fact.Name, value, 0, null);
            }
            else
            {
                bool value = AskBoolValue(a_fact.Question);
                newFact = new BoolFact(a_fact.Name, value, 0, null);
            }
            return newFact;
        }

        /// <summary>
        /// Determines if a given rule is applicable or not : if applicable the computed level of the new deduced fact is returned, otherwise -1 is returned
        /// </summary>
        /// <param name="a_rule">Rule to check</param>
        /// <returns>Level of the new deduced fact or -1 if the rule is not applicable</returns>
        protected int CanApply(Rule a_rule)
        {
            int maxLevel = -1;
            foreach (IFact fact in a_rule.Premises)
            {
                IFact foundFact = factsBase.SearchFact(fact.Name);
                if (foundFact == null)
                {
                    // Le fait n'est pas présent dans la base de faits
                    if (fact.Question != null)
                    {
                        // Si le fait dans la prémisse a une question, on créé un nouveau fait dans la base de faits
                        foundFact = CreateFact(fact);
                        factsBase.Facts.Add(foundFact);
                        if (foundFact.Value.Equals(fact.Value))
                        {
                            maxLevel = Math.Max(maxLevel, 0);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        // Si le fait dans la prémisse n'a pas de question, c'est un fait inférable la règle ne peut pas s'appliquer
                        // Ceci implique que la base de règles est correctement structurée pour assurer une convergence !!
                        return -1;
                    }
                }
                else
                {
                    // Le fait est présent dans la base de faits
                    if (foundFact.Value.Equals(fact.Value))
                    {
                        // Si la valeur correspond, on passe à l'examen du fait suivant
                        maxLevel = Math.Max(maxLevel, foundFact.Level);
                    }
                    else
                    {
                        // Si la valeur ne correspond pas, la règle ne peut pas s'appliquer
                        return -1;
                    }
                }
            }
            return maxLevel;
        }

        /// <summary>
        /// Loops the rules base to find all applicable rules
        /// </summary>
        /// <param name="a_rulesBase">RulesBase object of the given identification problem</param>
        /// <returns></returns>
        protected Rule FindUsableRule(RulesBase a_rulesBase)
        {
            foreach (Rule rule in a_rulesBase.Rules)
            {
                currentLevel = CanApply(rule);
                if (currentLevel != -1)
                {
                    return rule;
                }
            }
            return null;
        }

        /// <summary>
        /// Solves a given identification problem
        /// </summary>
        public void Solve()
        {
            factsBase.Facts.Clear();
            if (initialFactsBase != null)
            {
                factsBase.Facts = new List<IFact>(initialFactsBase.Facts);
            }

            // Copie de la base de règles pour pouvoir faire des manipulations dedans (suppression de règles)
            RulesBase usableRules = new RulesBase();
            usableRules.Rules = new List<Rule>(rulesBase.Rules);
            bool moreRules = true;

            while (moreRules)
            {
                Rule ruleToApply = FindUsableRule(usableRules);
                if (ruleToApply != null)
                {
                    // Une règle à appliquer a été trouvée
                    IFact newFact = ruleToApply.Conclusion;
                    newFact.Level = currentLevel + 1;
                    factsBase.Facts.Add(newFact);
                    usableRules.Rules.Remove(ruleToApply);
                    currentLevel = -1;
                }
                else
                {
                    // Il n'y a plus de règle à appliquer
                    moreRules = false;
                }
            }
            ihm.PrintFacts(factsBase.Facts);
        }
    }
}