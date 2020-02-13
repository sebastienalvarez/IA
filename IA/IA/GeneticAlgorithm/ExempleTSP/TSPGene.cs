/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe TSPGene pour exemple du problème TSP
 */


using System;
using IA.GeneticAlgorithm.Interfaces;

namespace IA.GeneticAlgorithm.ExempleTSP
{
    public class TSPGene : IGene
    {
        protected City city;

        public TSPGene(City a_city)
        {
            city = a_city;
        }

        public TSPGene(TSPGene a_gene)
        {
            city = a_gene.city;
        }

        public int GetDistance(TSPGene a_gene)
        {
            return TSP.GetDistance(city, a_gene.city);
        }

        public void Mutate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return city.ToString();
        }
    }
}