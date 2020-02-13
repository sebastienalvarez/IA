/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Parser de données d'exemples pour les réseaux de neurones fourni dans un fichier texte
 */


using System;
using System.Collections.Generic;
using System.IO;
using IA.NeuralNetwork.Engine;
using IA.NeuralNetwork.Interfaces;

namespace IA.NeuralNetwork.Repositories
{
    public class TextExempleDataParser : IExempleDataParserRepository
    {
        /// <summary>
        /// Collection of DataPoint objects with data of exemples for a given problem
        /// </summary>
        protected List<DataPoint> data;

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the TextExempleDataParser class
        /// </summary>
        public TextExempleDataParser()
        {
            data = new List<DataPoint>();
        }

        // METHODES
        /// <summary>
        /// Parses a text file containing numerical values of inputs and outputs of exemples for a given problem
        /// </summary>
        /// <param name="a_source">Text source file path</param>
        /// <param name="a_inputsNumber">Inputs number of an exemple</param>
        /// <param name="a_outputsNumber">Outputs number of an exemple</param>
        /// <returns>Result of the parsing</returns>
        public bool CreateExempleData(object a_source, int a_inputsNumber, int a_outputsNumber)
        {
            if (a_inputsNumber < 1 || a_outputsNumber < 1)
            {
                throw new ArgumentException("Invalid number for a_inputsNumber or a_outputsNumber arguments, both should be greater than 0.");
            }
            data.Clear();
            StreamReader sr = null;
            try
            {
                using (sr = new StreamReader((string)a_source))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] contentData = sr.ReadLine().Split('\t');
                        if (contentData.Length != (a_inputsNumber + a_outputsNumber))
                        {
                            data.Clear();
                            sr.Close();
                            return false;
                        }
                        double[] inputs = new double[a_inputsNumber];
                        double[] outputs = new double[a_outputsNumber];
                        for(int i =0; i < a_inputsNumber; i++)
                        {
                            double value = 0;
                            if(!double.TryParse(contentData[i], out value))
                            {
                                data.Clear();
                                sr.Close();
                                return false;
                            }
                            inputs[i] = value;
                        }
                        for (int i = 0; i < a_outputsNumber; i++)
                        {
                            double value = 0;
                            if (!double.TryParse(contentData[i + a_inputsNumber], out value))
                            {
                                data.Clear();
                                sr.Close();
                                return false;
                            }
                            outputs[i] = value;
                        }
                        data.Add(new DataPoint(inputs, outputs));
                    }
                    return true;
                }

            }
            catch (Exception)
            {
                if(sr != null)
                {
                    sr.Close();
                }
                data.Clear();
                return false;
            }
        }

        /// <summary>
        /// Returns a collection of DataPoint objects with data of exemples for a given problem
        /// </summary>
        /// <returns>Collection of DataPoint objects with data of exemples for a given problem</returns>
        public List<DataPoint> GetData()
        {
            return data;
        }
    }
}