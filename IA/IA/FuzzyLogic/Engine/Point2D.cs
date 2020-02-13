/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe Point2D représentant un point d'une fonction d'appartenance d'un ensemble flou
 */


using System;
using System.Text;

namespace IA.FuzzyLogic.Engine
{
    public class Point2D: IComparable
    {
        // PROPRIETES
        /// <summary>
        /// X coordinate of the membership function point
        /// </summary>
        public double X { get; }
        /// <summary>
        /// Y coordinate of the membership function point
        /// </summary>
        public double Y { get; }

        // CONSTUCTEUR
        /// <summary>
        /// Creates an instance of the Point2D class
        /// </summary>
        /// <param name="a_x">X coordinate of the membership function point</param>
        /// <param name="a_y">Y coordinate of the membership function point</param>
        public Point2D(double a_x, double a_y)
        {
            if(a_y < 0 || a_y > 1)
            {
                throw new ArgumentException("a_y argument should be between 0 et 1.");
            }
            X = a_x;
            Y = a_y;
        }

        // OPERATEURS
        /// <summary>
        /// Redefines Equals method : 2 Point2D objects are equals if they have same coordinates
        /// </summary>
        /// <param name="obj">Point2D object to compare</param>
        /// <returns>Indicates if 2 Point2D objets are equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Point2D point = (Point2D)obj;
            if (X != point.X || Y != point.Y)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Redefines GetHashCode method.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            StringBuilder hashString = new StringBuilder();
            hashString.Append(X);
            hashString.Append(";");
            hashString.Append(Y);
            return hashString.GetHashCode();
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : 2 Point2D objects are equals if they have same coordinates
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if a == b</returns>
        static public bool operator ==(Point2D a, Point2D b)
        {
            if (Object.Equals(a, null))
            {
                if (Object.Equals(b, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return a.Equals(b);
            }
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : 2 Point2D objects are equals if they have same coordinates
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if a != b</returns>
        static public bool operator !=(Point2D a, Point2D b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : a > b if X coordinate of a is greater than X coordinate of b
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if a > b</returns>
        static public bool operator >(Point2D a, Point2D b)
        {
            return a.X > b.X;
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : b > a if X coordinate of b is greater than X coordinate of a
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if b > a</returns>
        static public bool operator <(Point2D a, Point2D b)
        {
            return a.X < b.X; ;
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : a >= b if X coordinate of a is equal or greater than X coordinate of b
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if a >= b</returns>
        static public bool operator >=(Point2D a, Point2D b)
        {
            return (a == b || a > b);
        }

        /// <summary>
        /// Compares 2 Point2D objects a and b : b >= a if X coordinate of b is equal or greater than X coordinate of a
        /// </summary>
        /// <param name="a">Point2D a to compare</param>
        /// <param name="b">Point2D b to compare</param>
        /// <returns>Indicates if b >= a</returns>
        static public bool operator <=(Point2D a, Point2D b)
        {
            return (a == b || a > b);
        }

        // METHODE
        /// <summary>
        /// Compares 2 Point2D objects for sorting purpose 
        /// </summary>
        /// <param name="obj">Point2D object to compare</param>
        /// <returns>Indicates if the given Point2D object is lesser, greater or equal to the instance</returns>
        public int CompareTo(object obj)
        {
            return (int)(X - ((Point2D)obj).X);
        }

        /// <summary>
        /// Provides a string object with Point2D coordinates
        /// </summary>
        /// <returns>String object with Point2D coordinates</returns>
        public override string ToString()
        {
            return "(" + X + ";" + Y + ")";
        }
    }
}