/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface pour les parsers de données d'exemples pour les réseaux de neurones
 */


using System;
using System.Collections.Generic;
using IA.NeuralNetwork.Engine;

namespace IA.NeuralNetwork.Interfaces
{
    public interface IExempleDataParserRepository
    {
        bool CreateExempleData(Object a_source, int a_inputsNumber, int a_outputsNumber);
        List<DataPoint> GetData();
    }
}