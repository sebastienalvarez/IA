using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA.NeuralNetwork.Interfaces;
using IA.NeuralNetwork.Engine;
using IA.NeuralNetwork.Repositories;

namespace NeuralNetworkTestApp
{
    class Program : IUserInterface
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public void Run()
        {
            //Problème du OU Exclusif
            Console.WriteLine("Problème du OU Exclusif :");
            IExempleDataParserRepository parser = new TextExempleDataParser();
            if (parser.CreateExempleData("E:\\Projets codage\\C#\\IA\\xor.txt", 2, 1))
            {
                NeuralSystem system = new NeuralSystem(2, 4, 1, parser, 1.0, this, false);
                system.LearningRate = 0.7;
                system.Run();

                Console.WriteLine("Réseau entrainé ! Test avec les entrées 0 et 1 :");
                double[] inputs = new double[] { 0, 1 };
                double[] result = system.ComputeOutput(inputs);
                Console.WriteLine("Résultat : " + result[0]);
                Console.WriteLine("Test avec les entrées 0 et 0 :");
                inputs = new double[] { 0, 0 };
                result = system.ComputeOutput(inputs);
                Console.WriteLine("Résultat : " + result[0]);
                Console.WriteLine("Test avec les entrées 1 et 0 :");
                inputs = new double[] { 1, 0 };
                result = system.ComputeOutput(inputs);
                Console.WriteLine("Résultat : " + result[0]);
                Console.WriteLine("Test avec les entrées 1 et 1 :");
                inputs = new double[] { 1, 1 };
                result = system.ComputeOutput(inputs);
                Console.WriteLine("Résultat : " + result[0]);

                Console.WriteLine("Sauvegarde du réseau de neurones entrainé...");
                if (system.SaveTrainedNeuralNetwork("E:\\Neural2.neu"))
                {
                    Console.WriteLine("Réseau sauvegardé avec succès");
                }
                else
                {
                    Console.WriteLine("Erreur dans la sauvegarde");
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Echec du parsing...");
            }

            //// Problème du OU Exclusif avec réseau de neurones déjà entrainé
            //Console.WriteLine("Problème du OU Exclusif :");
            //NeuralSystem system = new NeuralSystem("E:\\neural.neu", this);
            //Console.WriteLine("Utilisation d'un réseau de neurones entrainé ! Test avec les entrées 0 et 1 :");
            //double[] inputs = new double[] { 0, 1 };
            //double[] result = system.ComputeOutput(inputs);
            //Console.WriteLine("Résultat : " + result[0]);
            //Console.WriteLine("Test avec les entrées 0 et 0 :");
            //inputs = new double[] { 0, 0 };
            //result = system.ComputeOutput(inputs);
            //Console.WriteLine("Résultat : " + result[0]);
            //Console.WriteLine("Test avec les entrées 1 et 0 :");
            //inputs = new double[] { 1, 0 };
            //result = system.ComputeOutput(inputs);
            //Console.WriteLine("Résultat : " + result[0]);
            //Console.WriteLine("Test avec les entrées 1 et 1 :");
            //inputs = new double[] { 1, 1 };
            //result = system.ComputeOutput(inputs);
            //Console.WriteLine("Résultat : " + result[0]);
            //Console.ReadKey();


            //// Problème Abalone
            //Console.WriteLine("Problème ABALONE :");
            //IExempleDataParserRepository parser = new TextExempleDataParser();
            //if (parser.CreateExempleData("E:\\Projets codage\\C#\\IA\\abalone_norm.txt", 10, 1))
            //{
            //    NeuralSystem system = new NeuralSystem(10, 50, 1, parser, 0.8, this, true);
            //    system.LearningRate = 0.3;
            //    system.Run();

            //    Console.WriteLine("Réseau entrainé ! Test avec les entrées1	0	0	0,575	0,47	0,165	0,853	0,292	0,179	0,35	sortie(0,516666667) :");
            //    double[] inputs = new double[] { 1, 0, 0, 0.575, 0.47, 0.165, 0.853, 0.292, 0.179, 0.35 };
            //    double[] result = system.ComputeOutput(inputs);
            //    Console.WriteLine("Résultat : " + result[0]);

            //    Console.WriteLine("Sauvegarde du réseau de neurones entrainé...");
            //    system.SaveTrainedNeuralNetwork("E:\\Abalone.neu");

            //    Console.ReadKey();
            //}
            //else
            //{
            //    Console.WriteLine("Echec du parsing...");
            //}

            //// Problème Abalone avec réseau de neurones déjà entrainé
            //Console.WriteLine("Problème ABALONE :");
            //NeuralSystem system = new NeuralSystem("E:\\Abalone.neu", this);
            //double[] inputs = new double[] { 1, 0, 0, 0.575, 0.47, 0.165, 0.853, 0.292, 0.179, 0.35 };
            //double[] result = system.ComputeOutput(inputs);
            //Console.WriteLine("Résultat : " + result[0]);
            //Console.ReadKey();

        }

        public void PrintMessage(string a_message)
        {
            Console.WriteLine(a_message);
        }
    }
}
