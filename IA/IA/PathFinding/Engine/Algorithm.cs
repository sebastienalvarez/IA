/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite Algorithm représentant un algorithme de recherche de chemin
 */


using IA.PathFinding.Interfaces;

namespace IA.PathFinding.Engine
{
    public abstract class Algorithm
    {
        // PROPRIETES
        /// <summary>
        /// Object that implements IGraph interface : the graph to resolve
        /// </summary>
        protected IGraph graph;
        /// <summary>
        /// Reference to the application object instance that must implement the IUserInterface
        /// </summary>
        protected IUserInterface gui;

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Algorithm class
        /// </summary>
        /// <param name="a_graph">Object that implements IGraph interface : the graph to resolve</param>
        /// <param name="a_gui">Reference to the application object instance that must implement the IUserInterface</param>
        public Algorithm(IGraph a_graph, IUserInterface a_gui)
        {
            graph = a_graph;
            gui = a_gui;
        }

        // METHODES
        /// <summary>
        /// Solves path finding problem for the given graph
        /// </summary>
        public void Solve()
        {
            graph.Clear();
            Run();
            gui.PrintResult(graph.ReconstructPath(), graph.GetExitNode().DistanceFromBeginning);

        }

        /// <summary>
        /// Specific algorithm for a path finding method 
        /// </summary>
        protected abstract void Run();
    }
}