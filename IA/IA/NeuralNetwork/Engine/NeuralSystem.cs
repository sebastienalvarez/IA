/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe NeuralSystem représentant le système de gestion du réseau de neurones artificiels
 */


using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using IA.NeuralNetwork.Interfaces;

namespace IA.NeuralNetwork.Engine
{
    [Serializable]
    public class NeuralSystem
    {
        // PROPRIETES
        [NonSerialized]
        protected DataCollection data;
        protected NeuralNetwork network;
        [NonSerialized]
        protected IUserInterface gui;
        protected int iterationNumberWithoutProgress = 0;
        protected bool regressionTask;

        // Configuration du réseau de neurones
        public double LearningRate { get; set; }
        public double MaxError { get; set; }
        public int MaxIterations { get; set; }

        // CONSTRUCTEUR
        public NeuralSystem(int a_inputsNumber, int a_hiddenNeuronsNumber, int a_outputNeuronsNumber, IExempleDataParserRepository a_parser, double a_learningRatio, IUserInterface a_gui, bool a_regressionTask)
        {
            data = new DataCollection(a_parser, a_inputsNumber, a_outputNeuronsNumber, a_learningRatio);
            network = new NeuralNetwork(a_inputsNumber, a_hiddenNeuronsNumber, a_outputNeuronsNumber);
            gui = a_gui;
            regressionTask = a_regressionTask;
            LearningRate = 0.3;
            MaxError = 0.005;
            MaxIterations = 10001;
        }

        // Pour instancier un système de réseau de neurone entrainé
        public NeuralSystem(string a_fileName, IUserInterface a_gui)
        {
            NeuralSystem system = null;
            if (File.Exists(a_fileName))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(a_fileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                system = (NeuralSystem)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }

            if(system != null)
            {
                data = null;
                network = system.network ;
                gui = a_gui;
                regressionTask = system.regressionTask;
                LearningRate = system.LearningRate;
                MaxError = system.MaxError;
                MaxIterations = system.MaxIterations;
            }
        }

        // METHODES PUBLIQUES
        protected double totalGeneralisationError = double.PositiveInfinity;
        protected double oldGeneralisationError = double.PositiveInfinity;
        protected double totalError = double.PositiveInfinity;
        protected double oldError = double.PositiveInfinity;

        public void Run()
        {
            int i = 0;
            while(i < MaxIterations && totalError > MaxError && iterationNumberWithoutProgress < 5)
            {
                InitIteration();

                foreach(DataPoint point in data.TrainingPoints)
                {
                    Evaluate(point);
                    network.AdjustWeights(point, LearningRate);
                }

                ApplyModelOnGeneralisationDataSet();

                if(totalGeneralisationError > oldGeneralisationError)
                {
                    iterationNumberWithoutProgress++;
                }
                else
                {
                    iterationNumberWithoutProgress = 0;
                }

                gui.PrintMessage("Iteration n°" + i + " - Total error : " + totalError + " - Generalisation error : " + totalGeneralisationError + " - Rate : " + LearningRate + " - Mean : " + string.Format("{0:0.00}", Math.Sqrt(totalError / data.TrainingPoints.Length), "%2"));
                i++;
            }

            // Réseau de neurones entrainé : affichage et sauvegarde des valeurs
            Console.WriteLine("Réseau de neurones entrainé !");
            //Console.WriteLine("Neurones cachés :");
            
        }

        public double[] ComputeOutput(double[] a_inputs)
        {
            double[] result = network.Evaluate(a_inputs);
            for (int outNb = 0; outNb < result.Length; outNb++)
            {
                if (!regressionTask)
                {
                    if (result[outNb] > 0.5)
                    {
                        result[outNb] = 1.0;
                    }
                    else
                    {
                        result[outNb] = 0.0;
                    }
                }
            }
            return result; 
        }

        /// <summary>
        /// Saves a trained neural network with the specified filename
        /// </summary>
        /// <param name="a_fileName">Filename path to save the trained neural network</param>
        /// <returns>Result of the saving (true =  success, false = error)</returns>
        public bool SaveTrainedNeuralNetwork(string a_fileName)
        {
            Stream SaveFileStream = null;
            try
            {
                SaveFileStream = File.Create(a_fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(SaveFileStream, this);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if(SaveFileStream != null)
                {
                    SaveFileStream.Close();
                }
            }
            return true;
        }

        // METHODES PRIVEES
        protected void InitIteration()
        {
            oldError = totalError;
            totalError = 0;
            oldGeneralisationError = totalGeneralisationError;
            totalGeneralisationError = 0;
        }

        protected void Evaluate(DataPoint a_point)
        {
            double[] outputs = network.Evaluate(a_point);
            for(int outNb = 0; outNb < outputs.Length; outNb++)
            {
                if (!regressionTask)
                {
                    if(outputs[outNb] > 0.5)
                    {
                        outputs[outNb] = 1.0;
                    }
                    else
                    {
                        outputs[outNb] = 0.0;
                    }
                }
                double error = a_point.Outputs[outNb] - outputs[outNb];
                totalError += error * error;
            }
        }

        protected void ApplyModelOnGeneralisationDataSet()
        {
            foreach(DataPoint point in data.GeneralisationPoints)
            {
                double[] outputs = network.Evaluate(point);
                for (int outNb = 0; outNb < outputs.Length; outNb++)
                {
                    double error = point.Outputs[outNb] - outputs[outNb];
                    totalGeneralisationError += error * error;
                }
            }
        }
    }
}