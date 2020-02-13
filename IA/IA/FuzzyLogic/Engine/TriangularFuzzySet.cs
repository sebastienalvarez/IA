/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe TriangularFuzzySet dérivant de FuzzySet et représentant un ensemble flou prédéfini de type triangulaire
 */


using System;
using System.Collections.Generic;

namespace IA.FuzzyLogic.Engine
{
    public class TriangularFuzzySet : FuzzySet
    {
        // CONSTRUCTEURS
        /// <summary>
        /// Creates an instance of the TriangularFuzzySet class : it is a predefined fuzzy set with a triangular shape (only one point at value 1), an Argument Exception is raised if set of values are not distinct or if a_min et a_max are included inside inside range defined by a_leftX, a_centerX and a_rightX
        /// </summary>
        /// <param name="a_leftX">X coordinate of the left point of the triangle, note however that you can enter any point as minimum value among the three values is used for left point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_centerX">X coordinate of the center point of the triangle, note however that you can enter any point as median value among the three values is used for center point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_rightX">X coordinate of the right point of the triangle, note however that you can enter any point as maximum value among the three values is used for right point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_min">Minimum X coordinate of the fuzzy set, note that this value has to be lower than the minimum value from the previous arguments (otherwise an Argument Exception is raised)</param>
        /// <param name="a_max">Maximum X coordinate of the fuzzy set, note that this value has to be greater than the maximum value from the previous arguments (otherwise an Argument Exception is raised)</param>
        public TriangularFuzzySet(double a_leftX, double a_centerX, double a_rightX, double a_min, double a_max)
        {
            if(a_leftX == a_centerX || a_leftX == a_rightX || a_centerX == a_rightX)
            {
                throw new ArgumentException("Incorrect set of points (a_leftX, a_centerX and a_rightX have to be different)");
            }
            List<double> coordinates = new List<double>();
            coordinates.Add(a_leftX);
            coordinates.Add(a_centerX);
            coordinates.Add(a_rightX);
            coordinates.Sort();
            AddPoint(new Point2D(coordinates[0], 0));
            AddPoint(new Point2D(coordinates[1], 1));
            AddPoint(new Point2D(coordinates[2], 0));
            Points.Sort();
            ComputeMinMax();
            if (a_min < min)
            {
                min = a_min;
            }
            else
            {
                throw new ArgumentException("a_min cannot be inside range defined by a_leftX, a_centerX and a_rightX");
            }
            if (a_max > max)
            {
                max = a_max;
            }
            else
            {
                throw new ArgumentException("a_max cannot be inside range defined by a_leftX, a_centerX and a_rightX");
            }
        }
    }
}