/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe TileTypeConverter pour exemple du problème de la carte
 */


using System;

namespace IA.PathFinding.ExempleMap
{
    static public class TileTypeConverter
    {
        static public TileType TypeFromChar(Char a_c)
        {
            switch (a_c)
            {
                case ' ':
                    return TileType.GRASS;
                case '*':
                    return TileType.TREE;
                case 'X':
                    return TileType.WATER;
                case '=':
                    return TileType.BRIDGE;
                case '.':
                    return TileType.PATH;
            }
            throw new FormatException("Incorrect Tile character.");
        }
    }
}