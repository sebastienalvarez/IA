/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe GreedyAlgorithmForKnapSack dérivée de la classe GreedyAlgorithm pour exemple du problème d'optimisation du sac à dos
 */


using System.Collections.Generic;
using System.Linq;
using IA.Metaheuristic.Algorithms;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class GreedyAlgorithmForKnapSack : GreedyAlgorithm
    {
        // PROPRIETES
        KnapSackSolution solution;

        // METHODES
        /// <summary>
        /// Constructs the solution element by element
        /// </summary>
        protected override void ConstructSolution()
        {
            KnapSackProblem pb = (KnapSackProblem)problem;
            List<Box> boxes = pb.Boxes;

            solution = new KnapSackSolution();

            foreach(Box currentBox in boxes.OrderByDescending(x => x.Value / x.Weight))
            {
                double spaceLeft = pb.MaxWeight - solution.Weight;
                if(currentBox.Weight < spaceLeft)
                {
                    solution.LoadedContent.Add(currentBox);
                }
            }
        }

        /// <summary>
        /// Creates the result for the given optimization problem
        /// </summary>
        protected override void SendResult()
        {
            gui.PrintMessage(solution.ToString());
        }
    }
}