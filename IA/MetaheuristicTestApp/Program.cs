using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA.Metaheuristic.ExempleKnapSack;
using IA.Metaheuristic.Interfaces;
using IA.Metaheuristic.Algorithms;

namespace MetaheuristicTestApp
{
    class Program : IUserInterface
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void PrintMessage(string a_message)
        {
            Console.WriteLine(a_message);
        }

        private void RunAlgorithms(IProblem a_problem)
        {
            Algorithm algorithm;

            Console.WriteLine("Algorithme glouton :");
            algorithm = new GreedyAlgorithmForKnapSack();
            algorithm.Solve(a_problem, this);
            Console.WriteLine("");

            Console.WriteLine("Descente de gradient :");
            algorithm = new GradientDescentAlgorithmForKnapSack();
            algorithm.Solve(a_problem, this);
            Console.WriteLine("");

            Console.WriteLine("Recherche tabou :");
            algorithm = new TabuSearchAlgorithmForKnapSack();
            algorithm.Solve(a_problem, this);
            Console.WriteLine("");

            Console.WriteLine("Recuit simulé :");
            algorithm = new SimulatedAnnealingAlgorithmForKnapSack();
            algorithm.Solve(a_problem, this);
            Console.WriteLine("");

            Console.WriteLine("Optimisation par essaim particulaire :");
            algorithm = new ParticleSwarmOptimizationAlgorithmForKnapSack();
            algorithm.Solve(a_problem, this);
            Console.WriteLine("");
        }

        private void Run()
        {
            Console.WriteLine("Métaheuristique d'optimisation :\n");
            IProblem kspb = new KnapSackProblem();
            RunAlgorithms(kspb);

            Console.WriteLine("***************************************************\n");
            IProblem kspbRandom = new KnapSackProblem(100, 30, 20);
            RunAlgorithms(kspbRandom);

            Console.WriteLine("FIN");

            Console.ReadKey();
        }
    }
}
