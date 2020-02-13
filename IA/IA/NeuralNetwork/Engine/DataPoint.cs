/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe DataPoint représentant les données d'un exemple pour l'apprentissage du réseau de neurones
 */


namespace IA.NeuralNetwork.Engine
{
    public class DataPoint
    {
   
        // PROPRIETES
        /// <summary>
        /// Input values of an example for the neural network training
        /// </summary>
        public double[] Inputs { get; }

        /// <summary>
        /// Output values of an eaemple for the neural network training
        /// </summary>
        public double[] Outputs { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the DataPoint class
        /// </summary>
        /// <param name="a_inputs">Input values of an example for the neural network training</param>
        /// <param name="a_outputs">Output values of an example for the neural network training</param>
        public DataPoint(double[] a_inputs, double[] a_outputs)
        {
            Inputs = a_inputs;
            Outputs = a_outputs;
        }
    }
}