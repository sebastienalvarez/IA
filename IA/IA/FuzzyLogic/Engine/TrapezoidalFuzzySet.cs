/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe TrapezoidalFuzzySet dérivant de FuzzySet et représentant un ensemble flou prédéfini de type trapezoidale
 */


using System;
using System.Collections.Generic;

namespace IA.FuzzyLogic.Engine
{
    public class TrapezoidalFuzzySet : FuzzySet
    {
        // CONSTRUCTEURS
        /// <summary>
        /// Creates an instance of the TrapezoidalFuzzySet class : it is a predefined fuzzy set with a trapezoidal shape, an Argument Exception is raised if set of values are not distinct or if a_min et a_max are included inside inside range defined by a_leftZeroX, a_leftOneX, a_rightOneX and a_rightZeroX
        /// </summary>
        /// <param name="a_leftZeroX">X coordinate of the left point of the trapezoid with value 0, note however that you can enter any point as minimum value among the four values is used for left zero point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_leftOneX">X coordinate of the left point of the trapezoid with value 1, note however that you can enter any point as second minimum value among the four values is used for left one point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_rightOneX">X coordinate of the right point of the trapezoid with value 1, note however that you can enter any point as third minimum value among the four values is used for right one point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_rightZeroX">X coordinate of the right point of the trapezoid with value 0, note however that you can enter any point as maximum value among the four values is used for right zero point, however values have to be different (otherwise an Argument Exception is raised)</param>
        /// <param name="a_min">Minimum X coordinate of the fuzzy set, note that this value has to be lower than the minimum value from the previous arguments (otherwise an Argument Exception is raised)</param>
        /// <param name="a_max">Maximum X coordinate of the fuzzy set, note that this value has to be greater than the maximum value from the previous arguments (otherwise an Argument Exception is raised)</param>
        public TrapezoidalFuzzySet(double a_leftZeroX, double a_leftOneX, double a_rightOneX, double a_rightZeroX, double a_min, double a_max)
        {

            List<double> coordinates = new List<double>();
            coordinates.Add(a_leftZeroX);
            coordinates.Add(a_leftOneX);
            coordinates.Add(a_rightOneX);
            coordinates.Add(a_rightZeroX);
            coordinates.Sort();
            if (coordinates[0] == coordinates[1] || coordinates[0] == coordinates[2] || coordinates[0] == coordinates[3] || coordinates[1] == coordinates[3] || coordinates[2] == coordinates[3])
            {
                throw new ArgumentException("Incorrect set of points (a_leftZeroX, a_leftOneX, a_rightOneX and a_rightZeroX have to be different)");
            }
            AddPoint(new Point2D(coordinates[0], 0));
            AddPoint(new Point2D(coordinates[1], 1));
            AddPoint(new Point2D(coordinates[2], 1));
            AddPoint(new Point2D(coordinates[3], 0));
            Points.Sort();
            ComputeMinMax();
            if (a_min < min)
            {
                min = a_min;
                AddPoint(new Point2D(min, 0));
            }
            else
            {
                throw new ArgumentException("a_min cannot be inside range defined by a_leftZeroX, a_leftOneX, a_rightOneX and a_rightZeroX");
            }
            if (a_max > max)
            {
                max = a_max;
                AddPoint(new Point2D(max, 0));
            }
            else
            {
                throw new ArgumentException("a_max cannot be inside range defined by a_leftZeroX, a_leftOneX, a_rightOneX and a_rightZeroX");
            }
        }
    }
}