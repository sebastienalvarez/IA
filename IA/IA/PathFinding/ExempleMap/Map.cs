/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe Map pour exemple du problème de la carte
 */


using System;
using System.Collections.Generic;
using System.Text;
using IA.PathFinding.Engine;
using IA.PathFinding.Interfaces;

namespace IA.PathFinding.ExempleMap
{
    public class Map : IGraph
    {
        // PROPRIETES
        protected Tile[,] tiles;
        protected int rowNumber;
        protected int columnNumber;
        protected Tile beginningNode;
        protected Tile exitNode;
        protected List<Node> nodesList = null;
        protected List<Arc> arcsList = null;
        protected string[] mapRows;

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Map class
        /// </summary>
        /// <param name="a_map">String representing the map</param>
        /// <param name="a_beginningRow">Row of the beginning</param>
        /// <param name="a_beginningColumn">Column of the beginning</param>
        /// <param name="a_exitRow">Row of the exit</param>
        /// <param name="a_exitColumn">Column of the exit</param>
        public Map(string a_map, int a_beginningRow, int a_beginningColumn, int a_exitRow, int a_exitColumn)
        {
            CreateTilesArray(a_map);
            FillMap();
            InitBeginningAndExitNodes(a_beginningRow, a_beginningColumn, a_exitRow, a_exitColumn);
            InitNodesAndArcsList();
        }

        // METHODES
        /// <summary>
        /// Creates tile array from the string that represents the map
        /// </summary>
        /// <param name="a_map">String representing the map</param>
        protected void CreateTilesArray(string a_map)
        {
            mapRows = a_map.Split('\n');
            rowNumber = mapRows.Length;
            columnNumber = mapRows[0].Length;
            tiles = new Tile[rowNumber, columnNumber];
        }

        /// <summary>
        /// Fills the map from string content
        /// </summary>
        protected void FillMap()
        {
            for(int row = 0; row < rowNumber; row++)
            {
                for(int column = 0; column < columnNumber; column++)
                {
                    tiles[row, column] = new Tile(TileTypeConverter.TypeFromChar(mapRows[row][column]), row, column);
                }
            }
        }

        /// <summary>
        /// Inits beginning node and exit node of the map
        /// </summary>
        /// <param name="a_beginningRow">Row of the beginning</param>
        /// <param name="a_beginningColumn">Column of the beginning</param>
        /// <param name="a_exitRow">Row of the exit</param>
        /// <param name="a_exitColumn">Column of the exit</param>
        protected void InitBeginningAndExitNodes(int a_beginningRow, int a_beginningColumn, int a_exitRow, int a_exitColumn)
        {
            beginningNode = tiles[a_beginningRow, a_beginningColumn];
            beginningNode.DistanceFromBeginning = beginningNode.GetCost();
            exitNode = tiles[a_exitRow, a_exitColumn];
        }

        /// <summary>
        /// Inits collections of nodes and arcs
        /// </summary>
        protected void InitNodesAndArcsList()
        {
            GetAllNodes();
            GetAllArcs();
        }

        public void Clear()
        {
            nodesList = null;
            arcsList = null;
            for(int row = 0; row < rowNumber; row++)
            {
                for(int column = 0; column < columnNumber; column++)
                {
                    tiles[row, column].DistanceFromBeginning = double.PositiveInfinity;
                    tiles[row, column].Precursor = null;
                }
            }
            beginningNode.DistanceFromBeginning = beginningNode.GetCost();
        }

        public void ComputeEstimatedDistanceToExit()
        {
            foreach(Tile tile in tiles)
            {
                tile.EstimatedDistanceToExit = Math.Abs(exitNode.Row - tile.Row) + Math.Abs(exitNode.Column - tile.Column);
            }
        }

        public int CountNodes()
        {
            return rowNumber * columnNumber;
        }

        public List<Arc> GetAllArcs()
        {
            if(arcsList == null)
            {
                arcsList = new List<Arc>();

                for (int row = 0; row < rowNumber; row++)
                {
                    for (int column = 0; column < columnNumber; column++)
                    {
                        if(tiles[row, column].IsValidPath())
                        {
                            // Arc haut
                            if(row - 1 >= 0 && tiles[row - 1, column].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, column], tiles[row - 1, column], tiles[row - 1, column].GetCost()));
                            }

                            // Arc bas
                            if (row + 1 < rowNumber && tiles[row + 1, column].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, column], tiles[row + 1, column], tiles[row + 1, column].GetCost()));
                            }

                            // Arc gauche
                            if (column - 1 >= 0 && tiles[row, column - 1].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, column], tiles[row, column - 1], tiles[row, column - 1].GetCost()));
                            }

                            // Arc droite
                            if (column + 1 < columnNumber && tiles[row, column + 1].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, column], tiles[row, column + 1], tiles[row, column + 1].GetCost()));
                            }
                        }
                    }
                }
            }
            return arcsList;
        }

        public List<Node> GetAllNodes()
        {
            if(nodesList == null)
            {
                nodesList = new List<Node>();
                foreach(Node node in tiles)
                {
                    nodesList.Add(node);
                }
            }
            return nodesList;
        }

        public List<Arc> GetArcsFromNode(Node a_currentNode)
        {
            arcsList = new List<Arc>();
            int currentRow = ((Tile)a_currentNode).Row;
            int currentColumn = ((Tile)a_currentNode).Column;

            // Arc haut
            if (currentRow - 1 >= 0 && tiles[currentRow - 1, currentColumn].IsValidPath())
            {
                arcsList.Add(new Arc(a_currentNode, tiles[currentRow - 1, currentColumn], tiles[currentRow - 1, currentColumn].GetCost()));
            }

            // Arc haut
            if (currentRow + 1 < rowNumber && tiles[currentRow + 1, currentColumn].IsValidPath())
            {
                arcsList.Add(new Arc(a_currentNode, tiles[currentRow + 1, currentColumn], tiles[currentRow + 1, currentColumn].GetCost()));
            }

            // Arc gauche
            if (currentColumn - 1 >= 0 && tiles[currentRow, currentColumn - 1].IsValidPath())
            {
                arcsList.Add(new Arc(a_currentNode, tiles[currentRow, currentColumn - 1], tiles[currentRow, currentColumn - 1].GetCost()));
            }

            // Arc gauche
            if (currentColumn + 1 < columnNumber && tiles[currentRow, currentColumn + 1].IsValidPath())
            {
                arcsList.Add(new Arc(a_currentNode, tiles[currentRow, currentColumn + 1], tiles[currentRow, currentColumn + 1].GetCost()));
            }     
            return arcsList;
        }

        public Node GetBeginningNode()
        {
            return beginningNode;
        }

        public double GetCostBetweenNodes(Node a_fromNode, Node a_toNode)
        {
            return ((Tile)a_toNode).GetCost();
        }

        public Node GetExitNode()
        {
            return exitNode;
        }

        public List<Node> GetNodesAroundNode(Node a_currentNode)
        {
            List<Node> nodesList = new List<Node>();

            int currentRow = ((Tile)a_currentNode).Row;
            int currentColumn = ((Tile)a_currentNode).Column;

            // Noeud haut
            if(currentRow - 1 >= 0 && tiles[currentRow -1, currentColumn].IsValidPath())
            {
                nodesList.Add(tiles[currentRow - 1, currentColumn]);
            }

            // Noeud bas
            if (currentRow + 1 < rowNumber && tiles[currentRow + 1, currentColumn].IsValidPath())
            {
                nodesList.Add(tiles[currentRow + 1, currentColumn]);
            }

            // Noeud gauche
            if (currentColumn - 1 >= 0 && tiles[currentRow, currentColumn - 1].IsValidPath())
            {
                nodesList.Add(tiles[currentRow, currentColumn - 1]);
            }

            // Noeud droite
            if (currentColumn + 1 < columnNumber && tiles[currentRow, currentColumn + 1].IsValidPath())
            {
                nodesList.Add(tiles[currentRow, currentColumn + 1]);
            }

            return nodesList;
        }

        public string ReconstructPath()
        {
            StringBuilder sb = new StringBuilder();
            Tile currentNode = exitNode;
            Tile previousNode = (Tile)exitNode.Precursor;
            while(previousNode != null)
            {
                sb.Append("-" + currentNode.ToString());
                currentNode = previousNode;
                previousNode = (Tile)currentNode.Precursor;
            }
            sb.Append(currentNode.ToString());
            return sb.ToString();
        }
    }
}