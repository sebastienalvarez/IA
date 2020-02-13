/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe TSP pour exemple du problème TSP
 */


 using System.Collections.Generic;

namespace IA.GeneticAlgorithm.ExempleTSP
{
    static public class TSP
    {
        static public List<City> Cities;
        static int[][] Distances;

        static public void Init()
        {
            Cities = new List<City>()
            {
                new City("Paris"),
                new City("Lyon"),
                new City("Marseille"),
                new City("Nantes"),
                new City("Bordeaux"),
                new City("Toulouse"),
                new City("Lille")
            };

            Distances = new int[Cities.Count][];
            Distances[0] = new int[] { 0, 462, 772, 379, 546, 678, 215 }; // Paris
            Distances[1] = new int[] { 462, 0, 326, 598, 842, 506, 664 }; // Lyon
            Distances[2] = new int[] { 772, 326, 0, 909, 555, 407, 1005 }; // Marseille
            Distances[3] = new int[] { 379, 598, 909, 0, 338, 540, 584 }; // Nantes
            Distances[4] = new int[] { 546, 842, 555, 338, 0, 250, 792 }; // Bordeaux
            Distances[5] = new int[] { 678, 506, 407, 540, 250, 0, 926 }; // Toulouse
            Distances[6] = new int[] { 215, 664, 1005, 548, 792, 926, 0 }; // Lille
        }

        static public int GetDistance(City a_city1, City a_city2)
        {
            return Distances[Cities.IndexOf(a_city1)][Cities.IndexOf(a_city2)];
        }

        static public List<City> GetCities()
        {
            List<City> listCities = new List<City>();
            listCities.AddRange(Cities);
            return listCities;
        }
    }
}