using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA.GeneticAlgorithm.Engine;
using IA.GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithmTestApp
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
            // Init
            Parameters.CrossoverRate = 0.0;
            Parameters.MutationsRate = 0.6;
            Parameters.MutationAddRate = 0.0;
            Parameters.MutationDeleteRate = 0.0;
            Parameters.MinFitness = 0;
            Parameters.GenerationMaxNb = 15;

            // Lancement
            while (true)
            {
                EvolutionaryProcess geneticAlgoTSP = new EvolutionaryProcess(this, "TSP");
                geneticAlgoTSP.Run();
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        public void PrintBestIndividual(Individual a_individual, int a_generation)
        {
            Console.WriteLine(a_generation + " -> " + a_individual);
        }
    }
}
