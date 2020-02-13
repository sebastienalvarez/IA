/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe FuzzySet représentant un ensemble flou
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IA.FuzzyLogic.Engine
{
    public class FuzzySet
    {
        // PROPRIETES
        // Variables statiques utilisées pour le calcul des opérateurs flous ET (&) et OU (|) sur 2 ensembles flous
        static protected FuzzySet result;
        static protected List<Point2D>.Enumerator enum1;
        static protected List<Point2D>.Enumerator enum2;
        static protected Point2D oldPt1;
        static protected int relativePosition;
        static protected int newRelativePosition;
        static protected double x1;
        static protected double x2;
        static protected bool endOfList1;
        static protected bool endOfList2;

        // Variables utilisées pour le calcul du barycentre d'un ensemble flou (c'est à dire la valeur cherchée pour un problème donné)
        protected double ponderatedArea;
        protected double totalArea;
        protected Point2D oldPoint;
        protected Point2D newPoint;

        /// <summary>
        /// Collection of Point2D objects defining the fuzzy set
        /// </summary>
        public List<Point2D> Points { get; }

        protected double min;
        /// <summary>
        /// Minimum X coordinate for the definition of the fuzzy set, Y coordinate is 0 if X coordinate is less than Min. Note that the set value has to be lower
        /// than the minimum X coordinate found in the Points collection (otherwise the set value is not applied)
        /// </summary>
        public double Min
        {
            get { return min; }
            set
            {
                if (value < min)
                {
                    min = value;
                }
            }
        }

        protected double max;
        /// <summary>
        /// Maximum X coordinate for the definition of the fuzzy set, Y coordinate is 0 if X coordinate is greater than Max. Note that the set value has to be greater
        /// than the maximum X coordinate found in the Points collection (otherwise the set value is not applied)
        /// </summary>
        public double Max
        {
            get { return max; }
            set
            {
                if (value > max)
                {
                    max = value;
                }
            }
        }

        /// <summary>
        /// Barycenter of the fuzzy set : it is the wanted value for a given problem 
        /// </summary>
        public double Barycenter
        {
            get
            {
                return ComputeBarycenter();
            }
        }

        // CONSTUCTEURS
        /// <summary>
        /// Creates an instance of the FuzzySet class
        /// </summary>
        /// <param name="a_points">Collection of Point2D objects defining the fuzzy set, the collection must have minimum 2 elements (otherwise an ArgumentException occurs)</param>
        /// <param name="a_min">Minimum X coordinate of the fuzzy set, note that this value has to be lower than the minimum X coordinate found in the a_points collection (otherwise the given value is overriden)</param>
        /// <param name="a_max">Maximum X coordinate of the fuzzy set, note that this value has to be greater than the maximum X coordinate found in the a_points collection (otherwise the given value is overriden)</param>
        public FuzzySet(List<Point2D> a_points, double a_min, double a_max)
        {
            if (a_points.Count < 2)
            {
                throw new ArgumentException("Invalid element number in the collection : there should be a minimum of 2 elements");
            }
            Points = new List<Point2D>(a_points);
            Points.Sort();
            ComputeMinMax();
            if (a_min < min)
            {
                min = a_min;
            }
            if (a_max > max)
            {
                max = a_max;
            }
        }

        /// <summary>
        /// Creates an instance of the FuzzySet class
        /// </summary>
        /// <param name="a_points">Collection of Point2D objects defining the fuzzy set, the collection must have minimum 2 elements (otherwise an ArgumentException occurs), Min and Max values are computed from the a_points collection</param>
        public FuzzySet(List<Point2D> a_points)
        {
            if (a_points.Count < 2)
            {
                throw new ArgumentException("Invalid element number in the collection : there should be a minimum of 2 elements");
            }
            Points = new List<Point2D>(a_points);
            Points.Sort();
            ComputeMinMax();
        }

        /// <summary>
        /// Creates an instance of the FuzzySet class
        /// </summary>
        /// <param name="a_points">Array of Point2D objects defining the fuzzy set, the array must have minimum 2 elements (otherwise an ArgumentException occurs)</param>
        /// <param name="a_min">Minimum X coordinate of the fuzzy set, note that this value has to be lower than the minimum X coordinate found in the a_points array (otherwise the given value is overriden)</param>
        /// <param name="a_max">Maximum X coordinate of the fuzzy set, note that this value has to be greater than the maximum X coordinate found in the a_points array (otherwise the given value is overriden)</param>
        public FuzzySet(Point2D[] a_points, double a_min, double a_max)
        {
            if (a_points.Length < 2)
            {
                throw new ArgumentException("Invalid element number in the array : there should be a minimum of 2 elements");
            }
            Points = new List<Point2D>(a_points);
            Points.Sort();
            ComputeMinMax();
            if (a_min < min)
            {
                min = a_min;
            }
            if (a_max > max)
            {
                max = a_max;
            }
        }

        /// <summary>
        /// Creates an instance of the FuzzySet class
        /// </summary>
        /// <param name="a_points">Array of Point2D objects defining the fuzzy set, the array must have minimum 2 elements (otherwise an ArgumentException occurs), Min and Max values are computed from the a_points array</param>
        public FuzzySet(Point2D[] a_points)
        {
            if (a_points.Length < 2)
            {
                throw new ArgumentException("Invalid element number in the array : there should be a minimum of 2 elements");
            }
            Points = new List<Point2D>(a_points);
            Points.Sort();
            ComputeMinMax();
        }

        /// <summary>
        /// Creates an instance of the FuzzySet class, instance is empty and should be set with AddPoint method and Min and Max properties 
        /// </summary>
        public FuzzySet()
        {
            Points = new List<Point2D>();
            min = 0;
            max = 0;
        }

        // OPERATEURS
        /// <summary>
        /// Redefines Equals method : 2 FuzzySet objects are equals if they have same property values
        /// </summary>
        /// <param name="obj">FuzzySet object to compare</param>
        /// <returns>Indicates if 2 FuzzySet objets are equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            FuzzySet fs = (FuzzySet)obj;
            if (ToString() != fs.ToString())
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
            hashString.Append(Min);
            hashString.Append(Max);
            foreach (var pt in Points)
            {
                hashString.Append(pt);
            }
            return hashString.GetHashCode();
        }

        /// <summary>
        /// Compares 2 FuzzySet objects a and b : 2 FuzzySet objects are equals if they have same property values
        /// </summary>
        /// <param name="a">FuzzySet a to compare</param>
        /// <param name="b">FuzzySet b to compare</param>
        /// <returns>Indicates if a == b</returns>
        static public bool operator ==(FuzzySet a, FuzzySet b)
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
        /// Compares 2 FuzzySet objects a and b : 2 FuzzySet objects are equals if they have same property values
        /// </summary>
        /// <param name="a">FuzzySet a to compare</param>
        /// <param name="b">FuzzySet b to compare</param>
        /// <returns>Indicates if a != b</returns>
        static public bool operator !=(FuzzySet a, FuzzySet b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Multiplies Y coordinate of all Point2D in the FuzzySet instance by a value
        /// </summary>
        /// <param name="a_fuzzySet">FuzzySet to multiply</param>
        /// <param name="a_value">Value to multiply</param>
        /// <returns>Computed a_fuzzySet x a_value</returns>
        static public FuzzySet operator *(FuzzySet a_fuzzySet, double a_value)
        {
            FuzzySet newFuzzySet = new FuzzySet();
            foreach (var pt in a_fuzzySet.Points)
            {
                newFuzzySet.AddPoint(pt.X, pt.Y * a_value);
            }
            newFuzzySet.Min = a_fuzzySet.Min;
            newFuzzySet.Max = a_fuzzySet.Max;
            return newFuzzySet;
        }

        /// <summary>
        /// Multiplies Y coordinate of all Point2D in the FuzzySet instance by a value
        /// </summary>
        /// <param name="a_value">Value to multiply</param>
        /// <param name="a_fuzzySet">FuzzySet to multiply</param>
        /// <returns>Computed a_value x a_fuzzySet</returns>
        static public FuzzySet operator *(double a_value, FuzzySet a_fuzzySet)
        {
            return a_fuzzySet * a_value;
        }

        /// <summary>
        /// Computes the fuzzy NOT operator
        /// </summary>
        /// <param name="a_fuzzySet">FuzzySet to compute</param>
        /// <returns>Computed NOT a_fuzzySet</returns>
        static public FuzzySet operator !(FuzzySet a_fuzzySet)
        {
            FuzzySet newFuzzySet = new FuzzySet();
            foreach (var pt in a_fuzzySet.Points)
            {
                newFuzzySet.AddPoint(pt.X, 1 - pt.Y);
            }
            newFuzzySet.Min = a_fuzzySet.Min;
            newFuzzySet.Max = a_fuzzySet.Max;
            return newFuzzySet;
        }

        /// <summary>
        /// Computes the fuzzy AND operator
        /// </summary>
        /// <param name="a">First FuzzySet a</param>
        /// <param name="b">Second FuzzySet b</param>
        /// <returns>Computed a AND b</returns>
        static public FuzzySet operator &(FuzzySet a, FuzzySet b)
        {
            return Merge(a, b, Math.Min);
        }

        /// <summary>
        /// Computes the fuzzy OR operator
        /// </summary>
        /// <param name="a">First FuzzySet a</param>
        /// <param name="b">Second FuzzySet b</param>
        /// <returns>Computed a OR b</returns>
        static public FuzzySet operator |(FuzzySet a, FuzzySet b)
        {
            return Merge(a, b, Math.Max);
        }

        // METHODES PUBLIQUES
        /// <summary>
        /// Adds a Point2D object to the Point collection property
        /// </summary>
        /// <param name="a_point">Point2D to add</param>
        public void AddPoint(Point2D a_point)
        {
            Points.Add(a_point);
            Points.Sort();
            double lastMinValue = min;
            double lastMaxValue = max;
            ComputeMinMax();
            if(min > lastMinValue)
            {
                min = lastMinValue;
            }
            if(max < lastMaxValue)
            {
                max = lastMaxValue;
            }
        }

        /// <summary>
        /// Adds a Point2D object to the Point collection property
        /// </summary>
        /// <param name="a_x">X coordinate of the Point2D object to add</param>
        /// <param name="a_y">Y coordinate of the Point2D object to add</param>
        public void AddPoint(double a_x, double a_y)
        {
            Points.Add(new Point2D(a_x, a_y));
            Points.Sort();
            double lastMinValue = min;
            double lastMaxValue = max;
            ComputeMinMax();
            if (min > lastMinValue)
            {
                min = lastMinValue;
            }
            if (max < lastMaxValue)
            {
                max = lastMaxValue;
            }
        }

        /// <summary>
        /// Computes Y coordinate for a given X coordinate in the fuzzy set
        /// </summary>
        /// <param name="a_value">X coordinate</param>
        /// <returns>Computed Y coordinate</returns>
        public double DegreeAtValue(double a_value)
        {
            if(a_value < Min || a_value > Max)
            {
                return 0;
            }
            else
            {
                Point2D beforePoint = Points.LastOrDefault(pt => pt.X <= a_value);
                Point2D afterPoint = Points.FirstOrDefault(pt => pt.X >= a_value);
                if(beforePoint == afterPoint)
                {
                    return beforePoint.Y;
                }
                else
                {
                    // Simple interpolation linéaire entre les 2 Point2D
                    return ((beforePoint.Y - afterPoint.Y) * (afterPoint.X - a_value) / (afterPoint.X - beforePoint.X)) + afterPoint.Y;
                }
            }
        }

        /// <summary>
        /// Provides a string object with fuzzy set properties
        /// </summary>
        /// <returns>String object with fuzzy set properties</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[" + Min + " - " + Max + "] : ");
            foreach(var pt in Points)
            {
                sb.Append(pt.ToString() + " ");
            }
            return sb.ToString();
        }

        // METHODES PRIVEES
        /// <summary>
        /// Computes min and max X coordinates from Points collection
        /// </summary>
        protected void ComputeMinMax()
        {
            min = Points[0].X;
            max = Points[Points.Count - 1].X;
        }

        /// <summary>
        /// Generic method for fuzzy operators AND and OR to merge 2 fuzzy sets
        /// </summary>
        /// <param name="a">First FuzzySet object a</param>
        /// <param name="b">First FuzzySet object b</param>
        /// <param name="a_mergeFunction">Computation method corresponding to the required fuzzy operator</param>
        /// <returns>Computed merged FuzzySet corresponding to a & B or to a | b</returns>
        static protected FuzzySet Merge(FuzzySet a, FuzzySet b, Func<double, double, double> a_mergeFunction)
        {
            // Initialisation de l'ensemble flou à calculer
            //double minNewFuzzySet = Math.Min(a.Min, b.Min);
            //double maxNewFuzzySet = Math.Max(a.Max, b.Max);
            result = new FuzzySet();

            // Initialisation des Iterators pour le parcours en parallèle de chacun des points des ensembles flous
            enum1 = a.Points.GetEnumerator();
            enum2 = b.Points.GetEnumerator();
            enum1.MoveNext();
            enum2.MoveNext();

            oldPt1 = enum1.Current;

            // Initialisation des positions relatives
            relativePosition = 0;
            newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

            // Parcours en parallèle des points des ensembles flous jusqu'à ce que l'on arrive à la fin d'un des 2 ensembles flous
            // Plusieurs cas de figure se présentent :
            // 1 - L'abscisse X du point de l'ensemble flou a est égal à l'abscisse X du point de l'ensemble flou b => on ajoute le point
            // 2 - L'abscisse X du point de l'ensemble flou a est avant l'abscisse X du point de l'ensemble flou b => on ajoute le point de l'ensemble flou a suivant le calcul fourni dans l'argument a_mergeFunction et on passe au point suivant de l'ensemble flou a
            // 3 - L'abscisse X du point de l'ensemble flou a est après l'abscisse X du point de l'ensemble flou b => on ajoute le point de l'ensemble flou b suivant le calcul fourni dans l'argument a_mergeFunction et on passe au point suivant de l'ensemble flou b
            // 4 - Un croisement de courbe est détecté => on calcul le point d'intersection et on ajoute le point
            endOfList1 = false;
            endOfList2 = false;
            while (!endOfList1 && !endOfList2)
            {
                ComputeNewValuesAndPositions();

                if(relativePosition != newRelativePosition && relativePosition != 0 && newRelativePosition != 0) // Détection d'une intersection : cas de figure n°4
                {
                    AddIntersectionToResult(a, b);
                    GoToNextPoints();
                }
                else if (x1 == x2) // Les points ont le même abscisse : cas de figure n°1
                {
                    result.AddPoint(x1, a_mergeFunction(enum1.Current.Y, enum2.Current.Y));
                    oldPt1 = enum1.Current;
                    GoToNextPointOnA();
                    GoToNextPointOnB();
                }
                else if(x1 < x2) // Le point de a est avant le point de b : cas de figure n°2
                {
                    result.AddPoint(x1, a_mergeFunction(enum1.Current.Y, b.DegreeAtValue(x1)));
                    oldPt1 = enum1.Current;
                    GoToNextPointOnA();
                }
                else // Le point de b est avant le point de a : cas de figure n°3
                {
                    result.AddPoint(x2, a_mergeFunction(a.DegreeAtValue(x2), enum2.Current.Y));
                    GoToNextPointOnB();
                }
            }

            // Un des 2 ensembles flous n'a plus de points, on ajoute les points du dernier ensemble flou suivant le calcul fourni dans l'argument a_mergeFunction
            AddEndPoints(a_mergeFunction);

            //result.Min = minNewFuzzySet;
            //result.Max = maxNewFuzzySet;
            return result;
        }
        
        /// <summary>
        /// Computes new values and positions following a point change in a fuzzy set
        /// </summary>
        static protected void ComputeNewValuesAndPositions()
        {
            x1 = enum1.Current.X;
            x2 = enum2.Current.X;
            relativePosition = newRelativePosition;
            newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);
        }

        /// <summary>
        /// Computes an intersection point
        /// </summary>
        /// <param name="a">First FuzzySet a</param>
        /// <param name="b">First FuzzySet b</param>
        static protected void AddIntersectionToResult(FuzzySet a, FuzzySet b)
        {
            // Calcul des coordonnées des points
            double x = (x1 == x2 ? oldPt1.X : Math.Min(x1, x2));
            double xPrime = Math.Max(x1, x2);
            
            // Calcul de l'intersection
            double slope1 = 0;
            double slope2 = 0;
            double delta = 0;
            if((xPrime - x) != 0)
            {
                slope1 = (a.DegreeAtValue(xPrime) - a.DegreeAtValue(x)) / (xPrime - x);
                slope2 = (b.DegreeAtValue(xPrime) - b.DegreeAtValue(x)) / (xPrime - x);
            }
            if(slope1 != slope2)
            {
                delta = (b.DegreeAtValue(x) - a.DegreeAtValue(x)) / (slope1 - slope2);
            }

            // Ajout du point
            result.AddPoint(x + delta, a.DegreeAtValue(x + delta));
        }

        /// <summary>
        /// Increments a point in fuzzy sets
        /// </summary>
        static protected void GoToNextPoints()
        {
            if(x1 < x2)
            {
                oldPt1 = enum1.Current;
                GoToNextPointOnA();
            }
            else if(x1 > x2)
            {
                GoToNextPointOnB();
            }
        }

        /// <summary>
        /// Increments a point in fuzzy set a
        /// </summary>
        static protected void GoToNextPointOnA()
        {
            endOfList1 = !(enum1.MoveNext());
        }

        /// <summary>
        /// Increments a point in fuzzy set b
        /// </summary>
        static protected void GoToNextPointOnB()
        {
            endOfList2 = !(enum2.MoveNext());
        }

        /// <summary>
        /// Adds all points of the last fuzzy set with the given computation method
        /// </summary>
        /// <param name="a_mergeFunction">Computation method corresponding to the required fuzzy operator</param>
        static protected void AddEndPoints(Func<double, double, double> a_mergeFunction)
        {
            if (!endOfList1)
            {
                while (!endOfList1)
                {
                    result.AddPoint(enum1.Current.X, a_mergeFunction(enum1.Current.Y, 0));
                    GoToNextPointOnA();
                }
            }
            else if (!endOfList2)
            {
                while (!endOfList2)
                {
                    result.AddPoint(enum2.Current.X, a_mergeFunction(0, enum2.Current.Y));
                    GoToNextPointOnA();
                }
            }
        }

        /// <summary>
        /// Computes the barycenter of a fuzzy set : it is the wanted value for a given problem
        /// </summary>
        /// <returns>Computed barycenter : the wanted value for a given problem</returns>
        protected double ComputeBarycenter()
        {
            ponderatedArea = 0;
            totalArea = 0;
            double localArea;
            oldPoint = null;
            foreach (var pt in Points)
            {
                newPoint = pt;
                if (oldPoint != null)
                {
                    if (oldPoint.Y == newPoint.Y) // Cas d'une forme rectangulaire seule
                    {
                        localArea = newPoint.Y * (newPoint.X - oldPoint.X);
                        IncrementAreas(localArea, 0.5);
                    }
                    else // Cas d'une forme rectangulaire et d'une forme triangulaire
                    {
                        // Cas de la 1ère forme rectangulaire
                        localArea = Math.Min(oldPoint.Y, newPoint.Y) * (newPoint.X - oldPoint.X);
                        IncrementAreas(localArea, 0.5);

                        // Cas de la 2ème forme triangulaire
                        localArea = Math.Abs(oldPoint.Y - newPoint.Y) * (newPoint.X - oldPoint.X) / 2;
                        if (newPoint.Y > oldPoint.Y)
                        {
                            IncrementAreas(localArea, (2.0 / 3.0));
                        }
                        else
                        {
                            IncrementAreas(localArea, (1.0 / 3.0));
                        }
                    }
                }
                oldPoint = newPoint;
            }
            return ponderatedArea / totalArea;
        }

        /// <summary>
        /// Computes areas for barycentric computation
        /// </summary>
        /// <param name="a_localArea">Local area for a single area (rectangle or triangle)</param>
        /// <param name="a_factor">Factor for computation of X coordinate depending on the area (0.5 for rectangle, 1/3 or 2/3 for a triangle)</param>
        protected void IncrementAreas(double a_localArea, double a_factor)
        {
            totalArea += a_localArea;
            ponderatedArea += ((newPoint.X - oldPoint.X) * a_factor + oldPoint.X) * a_localArea;
        }
    }
}