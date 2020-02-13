/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe KnapSackSolution implémentant l'interface ISolution pour exemple du problème d'optimisation du sac à dos
 */


using System.Collections.Generic;
using System.Linq;
using System.Text;
using IA.Metaheuristic.Interfaces;

namespace IA.Metaheuristic.ExempleKnapSack
{
    public class KnapSackSolution : ISolution
    {
        // PROPRIETES
        private List<Box> loadedContent;
        /// <summary>
        /// Content of the knap sack : collection of boxes
        /// </summary>
        public List<Box> LoadedContent
        {
            get { return loadedContent; }
            set { loadedContent = value; }
        }

        /// <summary>
        /// Weight of a knap sack
        /// </summary>
        public double Weight
        {
            get
            {
                return loadedContent.Sum(x => x.Weight);
            }
        }

        /// <summary>
        /// Value of a knap sack
        /// </summary>
        public double Value
        {
            get
            {
                return loadedContent.Sum(x => x.Value);
            }
        }

        // CONSTRUCTEURS
        /// <summary>
        /// Creates an empty instance of the KnapSackSolution class 
        /// </summary>
        public KnapSackSolution()
        {
            loadedContent = new List<Box>();
        }

        /// <summary>
        /// Creates an instance of the KnapSackSolution class with an existing KnapSackSolution instance
        /// </summary>
        /// <param name="a_solution">KnapSackSolution instance</param>
        public KnapSackSolution(KnapSackSolution a_solution)
        {
            loadedContent = new List<Box>();
            loadedContent.AddRange(a_solution.LoadedContent);
        }

        // METHODES
        /// <summary>
        /// Returns Weight and Value of the KnapSack and box details
        /// </summary>
        /// <returns>Returns Weight and Value of the KnapSack and box details</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Value : " + Value + " - Weight : " + Weight);
            sb.Append("Loaded : " + string.Join(" - ", loadedContent));
            return sb.ToString();
        }

        /// <summary>
        /// Compares 2 KnapSackSolution objects, they are equals if they have same content and thus same weight and value
        /// </summary>
        /// <param name="obj">KnapSackSolution object to compare</param>
        /// <returns>Comparaison result</returns>
        public override bool Equals(object obj)
        {
            KnapSackSolution solution = (KnapSackSolution)obj;
            if(solution.LoadedContent.Count != loadedContent.Count || solution.Value != Value || solution.Weight != Weight)
            {
                return false;
            }
            else
            {
                foreach(Box box in loadedContent)
                {
                    if (!solution.LoadedContent.Contains(box))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return (int)(Value * Weight);
        }
    }
}