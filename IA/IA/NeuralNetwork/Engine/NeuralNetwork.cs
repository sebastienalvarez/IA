/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe NeuralNetwork représentant un réseau de neurones artificiels
 */


using System;

namespace IA.NeuralNetwork.Engine
{
    [Serializable]
    public class NeuralNetwork
    {
        // PROPRIETES
        protected Neuron[] hiddenNeurons; // Neurones cachés, 1ère couche de neurones reliés aux entrées
        protected Neuron[] outputNeurons; // Neurones de sortie, 2ème couche de neurones reliés aux neurones cachés et fournissant les sorties
        protected int inputsNumber; // Nombres d'entrée pour le problème donné (ce sont les entrées allant sur chaque neurone caché)
        protected int hiddenNeuronsNumber; // Nombre de neurones cachés de la 1ère couche
        protected int outputNeuronsNumber; // Nombre de neurones de sortie de la 2ème couche

        // CONSTUCTEUR
        /// <summary>
        /// Creates an instance of the NeuronNetwork class
        /// </summary>
        /// <param name="a_inputsNumber">Number of inputs for the given problem</param>
        /// <param name="a_hiddenNeuronsNumber">Number of hidden neurons (1st layer)</param>
        /// <param name="a_outputNeuronsNumber">Number of output neurons (2nd layer), Note that this value must match outputs number for the given problem (example : if the neural network should return a single numeric value, then set this argument to 1)</param>
        public NeuralNetwork(int a_inputsNumber, int a_hiddenNeuronsNumber, int a_outputNeuronsNumber)
        {
            inputsNumber = a_inputsNumber;
            hiddenNeuronsNumber = a_hiddenNeuronsNumber;
            outputNeuronsNumber = a_outputNeuronsNumber;

            // Création des neurones cachés
            hiddenNeurons = new Neuron[hiddenNeuronsNumber];
            for(int i = 0; i < hiddenNeuronsNumber; i++)
            {
                hiddenNeurons[i] = new Neuron(inputsNumber);
            }

            // Création des neurones de sortie
            outputNeurons = new Neuron[outputNeuronsNumber];
            for (int i = 0; i < outputNeuronsNumber; i++)
            {
                outputNeurons[i] = new Neuron(hiddenNeuronsNumber);
            }
        }

        // METHODES PUBLIQUES
        /// <summary>
        /// Evaluates outputs of an example
        /// </summary>
        /// <param name="a_dataPoint">DataPoint object constituting the example</param>
        /// <returns>Computed outputs</returns>
        public double[] Evaluate(DataPoint a_dataPoint)
        {
            double[] inputs = a_dataPoint.Inputs;
            return Evaluate(inputs);
        }

        /// <summary>
        /// Evaluates outputs of a problem
        /// </summary>
        /// <param name="a_inputs">Inputs for the problem</param>
        /// <returns>Computed outputs</returns>
        public double[] Evaluate(double[] a_inputs)
        {
            ClearNetwork();

            // Calcul des valeurs de sorties des neurones cachés
            double[] hiddenOutputs = new double[hiddenNeuronsNumber];
            for (int i = 0; i < hiddenNeuronsNumber; i++)
            {
                hiddenOutputs[i] = hiddenNeurons[i].Evaluate(a_inputs);
            }

            // Calcul des valeurs de sorties des neurones cachés
            double[] outputs = new double[outputNeuronsNumber];
            for (int i = 0; i < outputNeuronsNumber; i++)
            {
                outputs[i] = outputNeurons[i].Evaluate(hiddenOutputs);
            }
            return outputs;
        }

        /// <summary>
        /// Adjusts the weights of neurons of the network during learning process
        /// </summary>
        /// <param name="a_point">DataPoint object representing an exemple</param>
        /// <param name="a_learningRate">Learning rate to adjust weights, the more the learning rate, the more adjustment is</param>
        public void AdjustWeights(DataPoint a_point, double a_learningRate)
        {
            // Calcul des deltas des neurones de sortie
            double[] outputDeltas = new double[outputNeuronsNumber];
            for(int i = 0; i < outputNeuronsNumber; i++)
            {
                double output = outputNeurons[i].Output;
                double expectedOutput = a_point.Outputs[i];
                outputDeltas[i] = output * (1 - output) * (expectedOutput - output);
            }

            // Calcul des deltas des neurones cachés
            double[] hiddenDeltas = new double[hiddenNeuronsNumber];
            for (int i = 0; i < hiddenNeuronsNumber; i++)
            {
                double hiddenOutput = hiddenNeurons[i].Output;
                double sum = 0;
                for (int j = 0; j < outputNeuronsNumber; j++)
                {
                    sum += outputDeltas[j] * outputNeurons[j].Weights[i];
                }
                hiddenDeltas[i] = hiddenOutput * (1 - hiddenOutput) * sum;
            }

            // Ajustement des poids des neurones de sortie
            for(int i = 0; i < outputNeuronsNumber; i++)
            {
                for(int j = 0; j < hiddenNeuronsNumber; j++)
                {
                    outputNeurons[i].Weights[j] += a_learningRate * outputDeltas[i] * hiddenNeurons[j].Output;
                }
                outputNeurons[i].Weights[hiddenNeuronsNumber] += a_learningRate * outputDeltas[i] * 1.0;
            }

            // Ajustement des poids des neurones cachés
            for (int i = 0; i < hiddenNeuronsNumber; i++)
            {
                for (int j = 0; j < inputsNumber; j++)
                {
                    hiddenNeurons[i].Weights[j] += a_learningRate * hiddenDeltas[i] * a_point.Inputs[j];
                }
                hiddenNeurons[i].Weights[inputsNumber] += a_learningRate * hiddenDeltas[i] * 1.0;
            }
        }

        // METHODES PRIVEES
        /// <summary>
        /// Clear the neural network to build a new neural network
        /// </summary>
        protected void ClearNetwork()
        {
            foreach(Neuron neuron in hiddenNeurons)
            {
                neuron.Clear();
            }
            foreach(Neuron neuron in outputNeurons)
            {
                neuron.Clear();
            }
        }
    }
}