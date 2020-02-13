/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe BreadthFirst pour l'algorithme de recherche de chemin "Parcours en largeur"
 */


using System.Collections.Generic;
using IA.PathFinding.Interfaces;

namespace IA.PathFinding.Engine
{
    public class BreadthFirst : Algorithm
    {
        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the BreadthFirst class
        /// </summary>
        /// <param name="a_graph">Object that implements IGraph interface : the graph to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public BreadthFirst(IGraph a_graph, IUserInterface a_gui) : base(a_graph, a_gui)
        {
        }

        // METHODE
        /// <summary>
        /// Specific algorithm for Breadth first path finding method
        /// </summary>
        protected override void Run()
        {
            // Initialisation des collections : démarrage au noeud départ
            List<Node> notVisitedNodes = graph.GetAllNodes();
            Queue<Node> nodesToVisit = new Queue<Node>();
            nodesToVisit.Enqueue(graph.GetBeginningNode());
            notVisitedNodes.Remove(graph.GetBeginningNode());

            // Algorithme de recherche en largeur
            Node exitNode = graph.GetExitNode();
            bool isExitReached = false;
            while (nodesToVisit.Count != 0 && !isExitReached)
            {
                Node currentNode = nodesToVisit.Dequeue();
                if (currentNode.Equals(exitNode))
                {
                    isExitReached = true;
                }
                else
                {
                    foreach (Node node in graph.GetNodesAroundNode(currentNode))
                    {
                        if (notVisitedNodes.Contains(node))
                        {
                            notVisitedNodes.Remove(node);
                            node.Precursor = currentNode;
                            node.DistanceFromBeginning = currentNode.DistanceFromBeginning + graph.GetCostBetweenNodes(currentNode, node);
                            nodesToVisit.Enqueue(node);
                        }
                    }
                }
            }
        }
    }
}