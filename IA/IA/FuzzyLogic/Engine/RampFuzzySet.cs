/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe RampFuzzySet dérivant de FuzzySet et représentant un ensemble flou prédéfini de type rampe
 */


using System;

namespace IA.FuzzyLogic.Engine
{
    public class RampFuzzySet : FuzzySet
    {
        // CONSTRUCTEURS
        /// <summary>
        /// Creates an instance of the RampFuzzySet class : it is a predefined fuzzy set with a ramp shape, an Argument Exception is raised if a_oneX = a_zeroX or if a_min et a_max are included in [a_oneX, a_zeroX]
        /// </summary>
        /// <param name="a_oneX">X coordinate where Y coordinate equals 1, a_oneX has to be different of a_zeroX (otherwise an Argument Exception is raised)</param>
        /// <param name="a_zeroX">X coordinate where Y coordinate equals 0, a_zeroX has to be different of a_oneX (otherwise an Argument Exception is raised)</param>
        /// <param name="a_min">Minimum X coordinate of the fuzzy set, note that this value has to be lower than the a_oneX X coordinate (otherwise an Argument Exception is raised)</param>
        /// <param name="a_max">Maximum X coordinate of the fuzzy set, note that this value has to be greater than the a_zeroX X coordinate (otherwise an Argument Exception is raised)</param>
        public RampFuzzySet(double a_oneX, double a_zeroX, double a_min, double a_max)
        {
            if(a_oneX == a_zeroX)
            {
                throw new ArgumentException("a_oneX cannot be equal to a_zeroX");
            }
            bool isLeft = a_oneX < a_zeroX;
            AddPoint(new Point2D(a_oneX, 1));
            AddPoint(new Point2D(a_zeroX, 0));
            Points.Sort();
            ComputeMinMax();
            if (a_min < min)
            {
                min = a_min;
                if (isLeft)
                {
                    AddPoint(new Point2D(min, 1));
                }
                else
                {
                    AddPoint(new Point2D(min, 0));
                }
            }
            else
            {
                throw new ArgumentException("a_min cannot be inside [a_oneX, a_zeroX]");
            }
            if (a_max > max)
            {
                max = a_max;
                if (isLeft)
                {
                    AddPoint(new Point2D(max, 0));
                }
                else
                {
                    AddPoint(new Point2D(max, 1));
                }
            }
            else
            {
                throw new ArgumentException("a_max cannot be inside [a_oneX, a_zeroX]");
            }
        }
    }
}