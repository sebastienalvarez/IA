/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : NA
 * 
 * Description : Interface qu'une application utilisant la technique du Système Expert de la bibliothèquees doit implémenter pour les questions à poser à l'utilisateur
 */


using System;
using System.Collections.Generic;

namespace IA.ExpertSystem.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Method allowing a user to respond to a fact question (fact of type IntFact), an application using Expert System technique of the IA library should implement this interface
        /// </summary>
        /// <param name="a_question">Question from an IntFact object</param>
        /// <returns>Integer value given by a user</returns>
        int AskIntValue(String a_question);
        /// <summary>
        /// Method allowing a user to respond to a fact question (fact of type BoolFact), an application using Expert System technique of the IA library should implement this interface
        /// </summary>
        /// <param name="a_question">Question from a BoolFact object</param>
        /// <returns>Boolean value given by a user</returns>
        bool AskBoolValue(String a_question);
        /// <summary>
        /// Method allowing to display to the user the facts in a collection (usually the fact base), an application using Expert System technique of the IA library should implement this interface, 
        /// the application has to determine which and how facts are displayed (for exemple : only the last deduced fact is displayed or all facts are displayed to show demonstration)
        /// </summary>
        /// <param name="a_facts">Collection of facts (usually the fact base)</param>
        void PrintFacts(List<IFact> a_facts);
        /// <summary>
        /// Method allowing to display to the user the rules in a collection (usually the rule base), an application using Expert System technique of the IA library should implement this interface, 
        /// </summary>
        /// <param name="a_facts">Collection of rules (usually the rule base)</param>
        void PrintRules(List<IA.ExpertSystem.Engine.Rule> a_rules);
    }
}