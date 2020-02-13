/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe IndividualFactory avec design pattern Singleton, cette classe permet la fabrique des individus
 */


using IA.GeneticAlgorithm;

namespace IA.GeneticAlgorithm.Engine
{
    public class IndividualFactory
    {
        /// <summary>
        /// Single instance of the IndivisualFactory class (singleton)
        /// </summary>
        static private IndividualFactory instance;

        /// <summary>
        /// Creates the instance of the IndividualFactory class
        /// </summary>
        private IndividualFactory() { }

        /// <summary>
        /// Returns single instance
        /// </summary>
        /// <returns>Single instance</returns>
        static public IndividualFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new IndividualFactory();
            }
            return instance;
        }

        /// <summary>
        /// Inits a problem by creating initial individual population
        /// </summary>
        /// <param name="a_type">Problem type</param>
        public void Init(string a_type)
        {
            switch (a_type)
            {
                case "TSP":
                    ExempleTSP.TSP.Init();
                    break;
            }
        }

        /// <summary>
        /// Gets a random individual for a given problem
        /// </summary>
        /// <param name="a_type">Problem type</param>
        /// <returns>Random Individual inherited object</returns>
        public Individual GetIndividual(string a_type)
        {
            Individual ind = null;
            switch (a_type)
            {
                case "TSP":
                    ind = new ExempleTSP.TSPIndividual();
                    break;
            }
            return ind;
        }

        /// <summary>
        /// Gets an individual from a single parent
        /// </summary>
        /// <param name="a_type">Problem type</param>
        /// <param name="a_father">Parent</param>
        /// <returns>Resulting child Individual inherited object</returns>
        public Individual GetIndividual(string a_type, Individual a_father)
        {
            Individual ind = null;
            switch (a_type)
            {
                case "TSP":
                    ind = new ExempleTSP.TSPIndividual((ExempleTSP.TSPIndividual)a_father);
                    break;
            }
            return ind;
        }

        /// <summary>
        /// Gets an individual from two parents
        /// </summary>
        /// <param name="a_type">Problem type</param>
        /// <param name="a_father">Parent 1</param>
        /// <param name="a_mother">Parent 2</param>
        /// <returns>Resulting child Individual inherited object</returns>
        public Individual GetIndividual(string a_type, Individual a_father, Individual a_mother)
        {
            Individual ind = null;
            switch (a_type)
            {
                case "TSP":
                    ind = new ExempleTSP.TSPIndividual((ExempleTSP.TSPIndividual)a_father, (ExempleTSP.TSPIndividual)a_mother);
                    break;
            }
            return ind;
        }
    }
}