/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe LinguisticVariable représentant une variable linguistique
 */

// REVOIR COMPLETEMENT LA GESTION DES MIN ET MAX !!! TRES IMPORTANT !!
using System;
using System.Collections.Generic;

namespace IA.FuzzyLogic.Engine
{
    public class LinguisticVariable
    {
        // PROPRIETES
        /// <summary>
        /// Name of the linguistic variable
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Collection of LinguisticValue objects defining the linguistic values
        /// </summary>
        public List<LinguisticValue> Values { get; }

        private double minValue;
        /// <summary>
        /// Minimum X coordinate for the definition of the linguistic variable. Note that the set value has to be lower than the minimum X coordinate found in the Values collection (otherwise the set value is not applied)
        /// </summary>
        public double MinValue
        {
            get { return minValue; }
            set
            {
                if (value < minValue)
                {
                    minValue = value;
                }
            }
        }

        private double maxValue;
        /// <summary>
        /// Maximum X coordinate for the definition of the linguistic variable. Note that the set value has to be greater than the maximum X coordinate found in the Values collection (otherwise the set value is not applied)
        /// </summary>
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value < maxValue)
                {
                    maxValue = value;
                }
            }
        }

        // CONSTRUCTEURS
        /// <summary>
        /// Creates an instance of the LinguisticVariable class
        /// </summary>
        /// <param name="a_name">Name of the linguistic variable</param>
        /// <param name="a_values">Collection of LinguisticValue objects defining all linguistic value of the variable, the collection must have minimum 1 element (otherwise an ArgumentException occurs)</param>
        /// <param name="a_minValue">Minimum X coordinate of the linguistic variable, note that this value has to be lower than the minimum X coordinate found in fuzzy sets of linguistic values (otherwise the given value is overriden)</param>
        /// <param name="a_maxValue">Maximum X coordinate of the linguistic variable, note that this value has to be greater than the maximum X coordinate found in fuzzy sets of linguistic values (otherwise the given value is overriden)</param>
        public LinguisticVariable(string a_name, List<LinguisticValue> a_values, double a_minValue, double a_maxValue)
        {
            if (a_values.Count < 1)
            {
                throw new ArgumentException("Invalid element number in the collection : there should be a minimum of 1 element");
            }
            Name = a_name;
            Values = a_values;
            ComputeMinMax();
            if(a_minValue < minValue)
            {
                minValue = a_minValue;
            }
            if (a_maxValue > maxValue)
            {
                maxValue = a_maxValue;
            }
        }

        /// <summary>
        /// Creates an instance of the LinguisticVariable class
        /// </summary>
        /// <param name="a_name">>Name of the linguistic variable</param>
        /// <param name="a_values">Collection of LinguisticValue objects defining all linguistic value of the variable, the collection must have minimum 1 element (otherwise an ArgumentException occurs)</param>
        public LinguisticVariable(string a_name, List<LinguisticValue> a_values)
        {
            if (a_values.Count < 1)
            {
                throw new ArgumentException("Invalid element number in the collection : there should be a minimum of 1 element");
            }
            Name = a_name;
            Values = a_values;
            ComputeMinMax();
        }

        /// <summary>
        /// Creates an instance of the LinguisticVariable class
        /// </summary>
        /// <param name="a_name">Name of the linguistic variable</param>
        /// <param name="a_values">Array of LinguisticValue objects defining all linguistic value of the variable, the array must have minimum 1 element (otherwise an ArgumentException occurs)</param>
        /// <param name="a_minValue">Minimum X coordinate of the linguistic variable, note that this value has to be lower than the minimum X coordinate found in fuzzy sets of linguistic values (otherwise the given value is overriden)</param>
        /// <param name="a_maxValue">Maximum X coordinate of the linguistic variable, note that this value has to be greater than the maximum X coordinate found in fuzzy sets of linguistic values (otherwise the given value is overriden)</param>
        public LinguisticVariable(string a_name, LinguisticValue[] a_values, double a_minValue, double a_maxValue)
        {
            if (a_values.Length < 1)
            {
                throw new ArgumentException("Invalid element number in the arrayn : there should be a minimum of 1 element");
            }
            Name = a_name;
            Values = new List<LinguisticValue>(a_values);
            ComputeMinMax();
            if (a_minValue < minValue)
            {
                minValue = a_minValue;
            }
            if (a_maxValue > maxValue)
            {
                maxValue = a_maxValue;
            }
        }

        /// <summary>
        /// Creates an instance of the LinguisticVariable class
        /// </summary>
        /// <param name="a_name">Name of the linguistic variable</param>
        /// <param name="a_values">Array of LinguisticValue objects defining all linguistic value of the variable, the array must have minimum 1 element (otherwise an ArgumentException occurs)</param>
        public LinguisticVariable(string a_name, LinguisticValue[] a_values)
        {
            if (a_values.Length < 1)
            {
                throw new ArgumentException("Invalid element number in the arrayn : there should be a minimum of 1 element");
            }
            Name = a_name;
            Values = new List<LinguisticValue>(a_values);
            ComputeMinMax();
        }

        /// <summary>
        /// Creates an instance of the LinguisticVariable class, instance is empty and should be set with AddValue method and MinValue and MaxValue properties
        /// </summary>
        /// <param name="a_name">Name of the linguistic variable</param>
        public LinguisticVariable(string a_name)
        {
            Name = a_name;
            Values = new List<LinguisticValue>();
            minValue = 0;
            maxValue = 0;
        }

        // METHODE PUBLIQUES
        /// <summary>
        /// Adds a LinguisticValue object to the Values collection property
        /// </summary>
        /// <param name="a_value">LinguisticValue object to add</param>
        public void AddValue(LinguisticValue a_value)
        {
            Values.Add(a_value);
            double lastMinValue = minValue;
            double lastMaxValue = maxValue;
            ComputeMinMax();
            if (minValue > lastMinValue)
            {
                minValue = lastMinValue;
            }
            if (maxValue < lastMaxValue)
            {
                maxValue = lastMaxValue;
            }
        }

        /// <summary>
        /// Adds a LinguisticValue object to the Values collection property
        /// </summary>
        /// <param name="a_name">Name of the linguistic value</param>
        /// <param name="a_fs">Fuzzy set of the linguitic value</param>
        public void AddValue(string a_name, FuzzySet a_fs)
        {
            Values.Add(new LinguisticValue(a_name, a_fs));
            double lastMinValue = minValue;
            double lastMaxValue = maxValue;
            ComputeMinMax();
            if (minValue > lastMinValue)
            {
                minValue = lastMinValue;
            }
            if (maxValue < lastMaxValue)
            {
                maxValue = lastMaxValue;
            }
        }

        /// <summary>
        /// Clear all values instance is then empty and should be set with AddValue method and MinValue and MaxValue properties
        /// </summary>
        public void ClearValues()
        {
            Values.Clear();
            minValue = 0;
            maxValue = 0;
        }

        /// <summary>
        /// Get a linguistic value by its name, if not found null is return
        /// </summary>
        /// <param name="a_name">Name of the linguistic value</param>
        /// <returns></returns>
        public LinguisticValue LinguisticValueByName(string a_name)
        {
            foreach(var value in Values)
            {
                if(value.Name.ToUpper() == a_name.ToUpper())
                {
                    return value;
                }
            }
            return null;
        }

        //  METHODE PRIVEE
        /// <summary>
        /// Computes min and max X coordinates from all the fuzzy sets
        /// </summary>
        protected void ComputeMinMax()
        {
            double minFound = 0;
            double maxFound = 0;
            if (Values != null && Values.Count > 0)
            {
                minFound = Values[0].FS.Min;
                maxFound = Values[0].FS.Max;
                for (var i = 1; i < Values.Count - 1; i++)
                {
                    if (minFound > Values[i].FS.Min)
                    {
                        minFound = Values[i].FS.Min;
                    }
                    if (maxFound < Values[i].FS.Max)
                    {
                        maxFound = Values[i].FS.Max;
                    }
                }
            }
            minValue = minFound;
            maxValue = maxFound;
        }
    }
}