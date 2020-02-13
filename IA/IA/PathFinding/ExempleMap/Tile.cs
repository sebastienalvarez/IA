/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe Tile pour exemple du problème de la carte
 */


using IA.PathFinding.Engine;

namespace IA.PathFinding.ExempleMap
{
    public class Tile : Node
    {
        // PROPRIETES
        /// <summary>
        /// Tile type
        /// </summary>
        protected TileType tileType;
        /// <summary>
        /// Tile row
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// Tile column
        /// </summary>
        public int Column { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Tile class
        /// </summary>
        /// <param name="a_tileType">Tile type</param>
        /// <param name="a_row">Tile row</param>
        /// <param name="a_column">Tile column</param>
        public Tile(TileType a_tileType, int a_row, int a_column)
        {
            tileType = a_tileType;
            Row = a_row;
            Column = a_column;
        }

        // METHODES
        /// <summary>
        /// Indicates if it is a valid path (only for Bridge, Grass and Path tile types)
        /// </summary>
        /// <returns>Indicates if it is a valid path</returns>
        public bool IsValidPath()
        {
            return tileType.Equals(TileType.BRIDGE) || tileType.Equals(TileType.GRASS) || tileType.Equals(TileType.PATH);
        }

        /// <summary>
        /// Gets the cost for the tile
        /// </summary>
        /// <returns>Cost for the tile</returns>
        public double GetCost()
        {
            switch (tileType)
            {
                case TileType.PATH :
                    return 1;
                case TileType.BRIDGE :
                case TileType.GRASS :
                    return 2;
                default :
                    return double.PositiveInfinity;
            }
        }

        public override string ToString()
        {
            return "[" + Row + ";" + Column + ";" + tileType.ToString() + "]";
        }
    }
}