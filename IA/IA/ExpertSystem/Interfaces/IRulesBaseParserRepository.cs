/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface pour les parsers de base de règles
 */


using System;
using IA.ExpertSystem.Engine;

namespace IA.ExpertSystem.Interfaces
{
    public interface IRulesBaseParserRepository
    {
        RulesBase CreateRulesBase(Object a_source);
    }
}