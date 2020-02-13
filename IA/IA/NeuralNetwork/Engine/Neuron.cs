/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe Neuron représentant un neurone artificiel
 */


using System;

namespace IA.NeuralNetwork.Engine
{
    [Serializable]
    public class Neuron
    {
        // PROPRIETES
        /// <summary>
        /// Number of inputs processed by the neuron
        /// </summary>
        public int InputsNumber { get; }
        /// <summary>
        /// Weights used by the neuron for each input for the Aggregate function, the last element is the threshold of the Activate function
        /// </summary>
        public double[] Weights{ get; set; }

        [NonSerialized]
        private double output;
        /// <summary>
        /// Output computed by the neuron
        /// </summary>
        public double Output
        {
            get { return output; }
        }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Neuron class
        /// </summary>
        /// <param name="a_inputsNumber">Number of inputs processed by the neuron</param>
        public Neuron(int a_inputsNumber)
        {
            InputsNumber = a_inputsNumber;
            output = double.NaN;

            Random generator = new Random();

            Weights = new double[a_inputsNumber + 1];
            // Initialisation aléatoire des poids et du seuil à une valeur basse
            for(int i = 0; i < a_inputsNumber + 1; i++)
            {
                Weights[i] = (generator.NextDouble() * 2.0 - 1.0) * 0.01;
            }
        }

        // METHODES PUBLIQUES
        /// <summary>
        /// Evaluates the output of an example
        /// </summary>
        /// <param name="a_dataPoint">DataPoint object constituting the example</param>
        /// <returns>Computed output</returns>
        public double Evaluate(DataPoint a_dataPoint)
        {
            double[] inputs = a_dataPoint.Inputs;
            return Evaluate(inputs);
        }

        /// <summary>
        /// Evaluates the output of a problem
        /// </summary>
        /// <param name="a_inputs">Inputs for the problem</param>
        /// <returns>Computed output</returns>
        public double Evaluate(double[] a_inputs)
        {
            if (output.Equals(double.NaN))
            {
                output = Activate(Aggregate(a_inputs));
            }
            return output;
        }

        /// <summary>
        /// Clears the output for a new computation
        /// </summary>
        public void Clear()
        {
            output = double.NaN;
        }

        // METHODES PRIVEES
        /// <summary>
        /// Aggregate function of the neuron, it uses a ponderated sum
        /// </summary>
        /// <param name="a_inputs">Inputs of an example or of the problem</param>
        /// <returns>Aggregated value</returns>
        protected double Aggregate(double[] a_inputs)
        {
            if(a_inputs.Length != InputsNumber)
            {
                throw new ArgumentException("Invalid inputs number : a_inputs argument does not have a correct length.");
            }

            double result = 0.0;
            for(int i = 0; i < a_inputs.Length; i++)
            {
                result += a_inputs[i] * Weights[i];
            }
            return result;
        }

        /// <summary>
        /// Activate function, it uses Sigmoid function
        /// </summary>
        /// <param name="a_aggregatedValue">Aggregated value from the Aggregate function</param>
        /// <returns>Activated value, in other terms the computed neuron output</returns>
        protected double Activate(double a_aggregatedValue)
        {
            return 1 / (1 + Math.Exp(-(Weights[InputsNumber] + a_aggregatedValue)));
        }
    }
}