/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe abstraite Node représentant un noeud d'un graphe
 */


namespace IA.PathFinding.Engine
{
    public abstract class Node
    {
        // PROPRIETES
        private Node precursor = null;
        /// <summary>
        /// Precursor node to this node
        /// </summary>
        public Node Precursor
        {
            get { return precursor; }
            set { precursor = value; }
        }

        private double distanceFromBeginning = double.PositiveInfinity;
        /// <summary>
        /// Distance From the Beginning node
        /// </summary>
        public double DistanceFromBeginning
        {
            get { return distanceFromBeginning; }
            set { distanceFromBeginning = value; }
        }

        private double estimatedDistanceToExit;
        /// <summary>
        /// Estimated Distance To Exit (this property is used in the A* algorithm)
        /// </summary>
        public double EstimatedDistanceToExit
        {
            get { return estimatedDistanceToExit; }
            set { estimatedDistanceToExit = value; }
        }
    }
}