/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Structure City pour exemple du problème TSP
 */


namespace IA.GeneticAlgorithm.ExempleTSP
{
    public struct City
    {
        string Name;

        public City(string a_city)
        {
            Name = a_city;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}