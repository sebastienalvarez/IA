/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe DepthFirst pour l'algorithme de recherche de chemin "Parcours en profondeur"
 */


using System.Collections.Generic;
using IA.PathFinding.Interfaces;

namespace IA.PathFinding.Engine
{
    public class DepthFirst : Algorithm
    {
        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the DepthFirst class
        /// </summary>
        /// <param name="a_graph">Object that implements IGraph interface : the graph to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public DepthFirst(IGraph a_graph, IUserInterface a_gui) : base(a_graph, a_gui)
        {
        }

        // METHODE
        /// <summary>
        /// Specific algorithm for Depth first path finding method
        /// </summary>
        protected override void Run()
        {
            // Initialisation des collections : démarrage au noeud départ
            List<Node> notVisitedNodes = graph.GetAllNodes();
            Stack<Node> nodesToVisit = new Stack<Node>();
            nodesToVisit.Push(graph.GetBeginningNode());
            notVisitedNodes.Remove(graph.GetBeginningNode());

            // Algorithme de recherche en profondeur
            Node exitNode = graph.GetExitNode();
            bool isExitReached = false;
            while(nodesToVisit.Count != 0 && !isExitReached)
            {
                Node currentNode = nodesToVisit.Pop();
                if (currentNode.Equals(exitNode))
                {
                    isExitReached = true;
                }
                else
                {
                    foreach(Node node in graph.GetNodesAroundNode(currentNode))
                    {
                        if (notVisitedNodes.Contains(node))
                        {
                            notVisitedNodes.Remove(node);
                            node.Precursor = currentNode;
                            node.DistanceFromBeginning = currentNode.DistanceFromBeginning + graph.GetCostBetweenNodes(currentNode, node);
                            nodesToVisit.Push(node);
                        }
                    }
                }
            }
        }
    }
}