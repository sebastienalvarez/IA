/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface pour les parsers de systèmes à logique floue
 */


using System;
using System.Collections.Generic;
using IA.FuzzyLogic.Engine;


namespace IA.FuzzyLogic.Interfaces
{
    interface IFuzzySystemParserRepository
    {
        bool CreateFuzzySystem(Object a_source);
        List<LinguisticVariable> GetInputs();
        LinguisticVariable GetOutput();
        List<FuzzyRule> GetRules();
    }
}