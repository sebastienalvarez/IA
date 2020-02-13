/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe Arc représentant un arc entre 2 noeuds
 */


namespace IA.PathFinding.Engine
{
    public class Arc
    {
        // PROPRIETES
        /// <summary>
        /// Source Node
        /// </summary>
        public Node FromNode { get; set; }
        /// <summary>
        /// Destination node
        /// </summary>
        public Node ToNode { get; set; }
        /// <summary>
        /// Cost to reach destination node from source node
        /// </summary>
        public double Cost { get; set; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Arc class
        /// </summary>
        /// <param name="a_fromNode">Source node</param>
        /// <param name="a_toNode">Destination node</param>
        /// <param name="a_cost">Cost to reach destination node from source node</param>
        public Arc(Node a_fromNode, Node a_toNode, double a_cost)
        {
            FromNode = a_fromNode;
            ToNode = a_toNode;
            Cost = a_cost;
        }
    }
}