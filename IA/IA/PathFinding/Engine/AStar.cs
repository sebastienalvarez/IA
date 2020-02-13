/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe AStar pour l'algorithme de recherche de chemin "A*"
 */


using System.Collections.Generic;
using System.Linq;
using IA.PathFinding.Interfaces;

namespace IA.PathFinding.Engine
{
    public class AStar : Algorithm
    {
        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the AStar class
        /// </summary>
        /// <param name="a_graph">Object that implements IGraph interface : the graph to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public AStar(IGraph a_graph, IUserInterface a_gui) : base(a_graph, a_gui)
        {
        }

        // METHODE
        /// <summary>
        /// Specific algorithm for A* path finding method
        /// </summary>
        protected override void Run()
        {
            // Initialisation
            List<Node> nodesToVisit = graph.GetAllNodes();
            bool isExitReached = false;
            Node currentNode;
            graph.ComputeEstimatedDistanceToExit();

            // Algorithme de recherche A*
            while (nodesToVisit.Count != 0 && !isExitReached)
            {
                currentNode = nodesToVisit.FirstOrDefault();
                foreach (Node newNode in nodesToVisit)
                {
                    if (newNode.DistanceFromBeginning + newNode.EstimatedDistanceToExit < (currentNode.DistanceFromBeginning + currentNode.EstimatedDistanceToExit))
                    {
                        currentNode = newNode;
                    }
                }

                if (currentNode == graph.GetExitNode())
                {
                    isExitReached = true;
                }
                else
                {
                    List<Arc> arcsFromCurrentNode = graph.GetArcsFromNode(currentNode);
                    foreach (Arc arc in arcsFromCurrentNode)
                    {
                        if (arc.FromNode.DistanceFromBeginning + arc.Cost < arc.ToNode.DistanceFromBeginning)
                        {
                            arc.ToNode.DistanceFromBeginning = arc.FromNode.DistanceFromBeginning + arc.Cost;
                            arc.ToNode.Precursor = arc.FromNode;
                        }
                    }
                    nodesToVisit.Remove(currentNode);
                }
            }
        }
    }
}