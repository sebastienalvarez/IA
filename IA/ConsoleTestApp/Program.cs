
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IA.ExpertSystem.Engine;
using IA.ExpertSystem.Interfaces;
using IA.ExpertSystem.Repositories;

namespace ConsoleTestApp
{

    class Program : IUserInterface
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public int AskIntValue(string a_question)
        {
            int value;
            Console.Write(a_question + " ? ");
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Saisie incorrecte" + Environment.NewLine + a_question + " ? ");
            }
            return value;
        }

        public bool AskBoolValue(string a_question)
        {
            String answer = String.Empty;
            bool isAnswerCorrect = false;
            do
            {
                Console.Write(a_question + " (oui/non) ? ");
                answer = Console.ReadLine();
                isAnswerCorrect = (answer.ToLower() == "oui" || answer.ToLower() == "non");
                if (!isAnswerCorrect)
                {
                    Console.WriteLine("Saisie incorrecte");
                }
            } while (!isAnswerCorrect);
            return answer.ToLower() == "oui";
        }

        public void PrintFacts(List<IFact> a_facts)
        {
            StringBuilder result = new StringBuilder();
            result.Append("\nSolutions trouvées par l'IA :" + Environment.NewLine);
            result.Append(String.Join(Environment.NewLine, from fact in a_facts
                                                           where fact.Level > 0
                                                           orderby fact.Level descending
                                                           select fact.Name));
            Console.WriteLine(result.ToString());
        }

        public void PrintRules(List<Rule> a_rules)
        {
            foreach (Rule rule in a_rules)
            {
                Console.WriteLine(rule.ToString());
            }
        }

        private void Run()
        {
            Console.WriteLine("Demonstration Intelligence Artificielle Système Expert" + Environment.NewLine +
                              "Sébastien ALVAREZ - Septembre 2018" + Environment.NewLine);

            Console.WriteLine("=> Création de la base de règles à partir du fichier xml...");
            XmlRulesBaseParser parser = new XmlRulesBaseParser();
            RulesBase rulesBase = parser.CreateRulesBase("RulesBase - polygones.xml");
            if (parser.ErrorCode == XmlRulesBaseParserErrorCode.SUCCESS)
            {
                Console.WriteLine("=> Base de règles chargée pour : " + parser.Name + "...");

                // Résolution du problème d'identification
                Console.WriteLine("=> Création du moteur d'inférence de l'IA...");
                FactsBase factsBase = new FactsBase();
                factsBase.Facts.Add(new IntFact("Ordre", 3, 0, null));
                factsBase.Facts.Add(new BoolFact("Angle droit", true, 0, null));

                Engine ia = new Engine(this, rulesBase, factsBase);

                //Engine ia = new Engine(this, rulesBase);
                Console.WriteLine("=> Résolution du problème d'identification...");
                ia.Solve();

                Console.WriteLine("\n=> Résolution du problème d'identification...");
                List<IFact> newFactsList = new List<IFact>();
                newFactsList.Add(new IntFact("Ordre", 3, 0, null));
                newFactsList.Add(new IntFact("Côtés égaux", 3, 0, null));
                ia.SetFactsBase(newFactsList);
                ia.Solve();

                Console.WriteLine("\n=> Résolution du problème d'identification...");
                ia = new Engine(this, rulesBase);
                ia.Solve();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Erreur de parsing du fichier xml..." + Environment.NewLine + "Erreur : " + parser.ErrorCode.ToString());
                Console.ReadLine();
            }
        }

        private List<Rule> CreateTestRulesBase()
        {
            List<Rule> rulesBase = new List<Rule>();
            List<IFact> currentFactList = new List<IFact>();
            IFact currentFact = null;
            Rule currentRule = null;
            // R1
            currentFact = new IntFact("Ordre", 3, 0, "Quel est l'ordre");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle", true, 0, null);
            currentRule = new Rule("R1", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R2
            currentFact = new BoolFact("Triangle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Angle droit", true, 0, "La figure a t'elle au moins un angle droit");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle rectangle", true, 0, null);
            currentRule = new Rule("R2", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R3
            currentFact = new BoolFact("Triangle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new IntFact("Côtés égaux", 2, 0, "Combien la figure a-t-elle de côtés égaux");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle isocèle", true, 0, null);
            currentRule = new Rule("R3", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R4
            currentFact = new BoolFact("Triangle rectangle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle isocèle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle rectangle isocèle", true, 0, null);
            currentRule = new Rule("R4", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R5
            currentFact = new BoolFact("Triangle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new IntFact("Côtés égaux", 3, 0, "Combien la figure a-t-elle de côtés égaux");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Triangle équilatéral", true, 0, null);
            currentRule = new Rule("R5", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R6
            currentFact = new IntFact("Ordre", 4, 0, "Quel est l'ordre");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Quadrilatère", true, 0, null);
            currentRule = new Rule("R6", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R7
            currentFact = new BoolFact("Quadrilatère", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new IntFact("Côtés parallèles", 2, 0, "Combien y a-t-il de côtés parallèles entre eux");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Trapèze", true, 0, null);
            currentRule = new Rule("R7", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R8
            currentFact = new BoolFact("Quadrilatère", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new IntFact("Côtés parallèles", 4, 0, "Combien y a-t-il de côtés parallèles entre eux");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Parallélogramme", true, 0, null);
            currentRule = new Rule("R8", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R9
            currentFact = new BoolFact("Parallélogramme", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Angle droit", true, 0, "La figure a t'elle au moins un angle droit");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Rectangle", true, 0, null);
            currentRule = new Rule("R9", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R10
            currentFact = new BoolFact("Parallélogramme", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new IntFact("Côtés égaux", 4, 0, "Combien la figure a-t-elle de côtés égaux");
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Losange", true, 0, null);
            currentRule = new Rule("R10", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();
            // R11
            currentFact = new BoolFact("Rectangle", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Losange", true, 0, null);
            currentFactList.Add(currentFact);
            currentFact = new BoolFact("Carré", true, 0, null);
            currentRule = new Rule("R11", currentFactList, currentFact);
            rulesBase.Add(currentRule);
            currentFactList.Clear();

            return rulesBase;
        }

    }
}