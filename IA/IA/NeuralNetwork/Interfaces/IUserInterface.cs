/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Interface qu'une application utilisant la technique des réseaux de neurones de la bibliothèque doit implémenter pour présenter à l'utilisateur le résultat
 */


namespace IA.NeuralNetwork.Interfaces
{
    public interface IUserInterface
    {
        /// <summary>
        /// Print message to display information on the training process
        /// </summary>
        /// <param name="a_message">Message to print</param>
        void PrintMessage(string a_message);
    }
}