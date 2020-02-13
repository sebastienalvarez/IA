/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Interface IGraph pour les classes représentant des graphes
 */


using System.Collections.Generic;
using IA.PathFinding.Engine;

namespace IA.PathFinding.Interfaces
{
    public interface IGraph
    {
        /// <summary>
        /// Gets the beginning node
        /// </summary>
        /// <returns>Beginning node</returns>
        Node GetBeginningNode();
        /// <summary>
        /// Gets the exit node
        /// </summary>
        /// <returns>Exit node</returns>
        Node GetExitNode();
        /// <summary>
        /// Gets all nodes of the graph
        /// </summary>
        /// <returns>Collection of all nodes of the graph</returns>
        List<Node> GetAllNodes();
        /// <summary>
        /// Gets nodes around a given node
        /// </summary>
        /// <param name="a_currentNode">Node</param>
        /// <returns>Collection of nodes around a given node</returns>
        List<Node> GetNodesAroundNode(Node a_currentNode);
        /// <summary>
        /// Gets all arcs of the graph
        /// </summary>
        /// <returns>Collection of all arcs of the graph</returns>
        List<Arc> GetAllArcs();
        /// <summary>
        /// Gets arcs of a given node
        /// </summary>
        /// <param name="a_currentNode">Node</param>
        /// <returns>Collection of arcs of a given node</returns>
        List<Arc> GetArcsFromNode(Node a_currentNode);
        /// <summary>
        /// Gets nodes number
        /// </summary>
        /// <returns>Nodes number</returns>
        int CountNodes();
        /// <summary>
        /// Gets the cost between 2 nodes
        /// </summary>
        /// <param name="a_fromNode">Source node</param>
        /// <param name="a_toNode">Destination node</param>
        /// <returns>Cost between source node and destination node</returns>
        double GetCostBetweenNodes(Node a_fromNode, Node a_toNode);
        /// <summary>
        /// Gets the path to the exit
        /// </summary>
        /// <returns>Path to the exit</returns>
        string ReconstructPath();
        /// <summary>
        /// Computes the estimated distance to the exit
        /// </summary>
        void ComputeEstimatedDistanceToExit();
        /// <summary>
        /// Clears the graph
        /// </summary>
        void Clear();
    }
}