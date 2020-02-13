/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe BellmanFord pour l'algorithme de recherche de chemin "Bellman-Ford"
 */


using System;
using System.Collections.Generic;
using IA.PathFinding.Interfaces;

namespace IA.PathFinding.Engine
{
    public class BellmanFord : Algorithm
    {
        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the BellmanFord class
        /// </summary>
        /// <param name="a_graph">Object that implements IGraph interface : the graph to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public BellmanFord(IGraph a_graph, IUserInterface a_gui) : base(a_graph, a_gui)
        {
        }

        // METHODE
        /// <summary>
        /// Specific algorithm for Bellman-Ford path finding method
        /// </summary>
        protected override void Run()
        {
            // Initialisation des champs
            bool distanceChanged = true;
            int i = 0;
            List<Arc> arcList = graph.GetAllArcs();

            // Algorithme de recherche Bellman-Ford
            int loopMaxNumber = graph.CountNodes() - 1;
            while (i < loopMaxNumber && distanceChanged)
            {
                distanceChanged = false;
                foreach(Arc arc in arcList)
                {
                    if(arc.FromNode.DistanceFromBeginning + arc.Cost < arc.ToNode.DistanceFromBeginning)
                    {
                        arc.ToNode.DistanceFromBeginning = arc.FromNode.DistanceFromBeginning + arc.Cost;
                        arc.ToNode.Precursor = arc.FromNode;
                        distanceChanged = true;
                    }
                }
                i++;
            }

            // Levée d'une exception si il n'existe pas de chemin le plus court (distance négative)
            foreach(Arc arc in arcList)
            {
                if(arc.FromNode.DistanceFromBeginning + arc.Cost < arc.ToNode.DistanceFromBeginning)
                {
                    throw new ApplicationException("Presence of negative distance.");
                }
            }
        }
    }
}