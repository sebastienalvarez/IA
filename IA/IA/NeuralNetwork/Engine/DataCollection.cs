/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe DataCollection représentant l'ensemble des exemples pour l'apprentissage du réseau de neurones
 */


using System;
using System.Collections.Generic;
using IA.NeuralNetwork.Interfaces;

namespace IA.NeuralNetwork.Engine
{
    public class DataCollection
    {
        // PROPRIETES
        /// <summary>
        /// Array of DataPoint objects representating examples used for the neural network training
        /// </summary>
        public DataPoint[] TrainingPoints { get; }
        /// <summary>
        /// Array of DataPoint objects representating examples used for detection of overlearning (generalisation of results)
        /// </summary>
        public DataPoint[] GeneralisationPoints { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the DataCollection class
        /// </summary>
        /// <param name="a_parser">Parser for getting examples from a given data source</param>
        /// <param name="a_inputsNumber">Number of inputs contained in an example</param>
        /// <param name="a_outputsNumber">Number of outputs contained in an example</param>
        /// <param name="a_learningRatio">Ratio (between 0 and 1) used for examples used for the neural network training, others examples are used for overlearning detection</param>
        public DataCollection(IExempleDataParserRepository a_parser, int a_inputsNumber, int a_outputsNumber, double a_learningRatio)
        {
            // Validation des arguments
            if (a_learningRatio < 0.0 || a_learningRatio > 1.0)
            {
                throw new ArgumentException("a_trainingRatio argument should be between 0.0 and 1.0 (0.8 value is recommended).");
            }

            List<DataPoint> dataFromParser = a_parser.GetData();
            Random generator = new Random();

            int trainingExempleNumber = (int)(dataFromParser.Count * a_learningRatio);
            int generalisationExempleNumber = dataFromParser.Count - trainingExempleNumber;
            TrainingPoints = new DataPoint[trainingExempleNumber];
            GeneralisationPoints = new DataPoint[generalisationExempleNumber];

            if(trainingExempleNumber > 0)
            {
                int effectiveTrainingExempleNumber = 0;
                // Remplissage du tableau trainingPoints avec des données des exemples de façon aléatoire
                while (trainingExempleNumber > effectiveTrainingExempleNumber)
                {
                    int randomIndex = generator.Next(dataFromParser.Count - 1);
                    TrainingPoints[effectiveTrainingExempleNumber] = dataFromParser[randomIndex];
                    dataFromParser.RemoveAt(randomIndex);
                    effectiveTrainingExempleNumber++;
                }
                for(int i = 0; i < generalisationExempleNumber; i++)
                {
                    GeneralisationPoints[i] = dataFromParser[i];
                }
            }
            else
            {
                TrainingPoints = null;
                GeneralisationPoints = null;
            }
        }
    }
}