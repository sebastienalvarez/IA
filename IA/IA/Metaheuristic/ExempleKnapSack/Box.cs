/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Mars 2019
 * REVISION : NA
 * 
 * Description : Classe Box pour exemple du problème d'optimisation du sac à dos
 */


namespace IA.Metaheuristic.ExempleKnapSack
{
    public class Box
    {
        // PROPRIETES
        /// <summary>
        /// Weight of the box
        /// </summary>
        public double Weight { get; }
        /// <summary>
        /// Value of the box
        /// </summary>
        public double Value { get; }
        /// <summary>
        /// Name of the box
        /// </summary>
        public string Name { get; }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the Box class
        /// </summary>
        /// <param name="a_name">Name of the box</param>
        /// <param name="a_weight">Weight of the box</param>
        /// <param name="a_value">Value of the box</param>
        public Box(string a_name, double a_weight, double a_value)
        {
            Name = a_name;
            Weight = a_weight;
            Value = a_value;
        }

        /// <summary>
        /// Returns Name box with its weight and value
        /// </summary>
        /// <returns>Name box with its weight and value</returns>
        public override string ToString()
        {
            return Name + " (" + Weight.ToString() + ", " + Value.ToString() + ")";
        }

    }
}