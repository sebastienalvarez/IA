/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Classe FuzzySystem représentant un système de contrôle flou
 */


using System.Collections.Generic;

namespace IA.FuzzyLogic.Engine
{
    public class FuzzySystem
    {
        // PROPRIETES
        public string Name { get; set; }
        public List<LinguisticVariable> Inputs { get; }
        private LinguisticVariable output;

        public LinguisticVariable Output
        {
            get { return output;  }
        }
        public List<FuzzyRule> Rules { get; }
        public List<FuzzyValue> Problem { get; set; }

        // CONSTRUCTEUR
        public FuzzySystem(string a_name)
        {
            Name = a_name;
            Inputs = new List<LinguisticVariable>();
            Rules = new List<FuzzyRule>();
            Problem = new List<FuzzyValue>();
        }

        // METHODES PUBLIQUES
        public FuzzySystem(string a_name, List<LinguisticVariable> a_inputs, LinguisticVariable a_output, List<FuzzyRule> a_rules, List<FuzzyValue> a_problem)
        {
            Name = a_name;
            Inputs = a_inputs;
            output = a_output;
            Rules = a_rules;
            Problem = a_problem;
        }

        public FuzzySystem(string a_name, List<LinguisticVariable> a_inputs, LinguisticVariable a_output, List<FuzzyRule> a_rules)
        {
            Name = a_name;
            Inputs = a_inputs;
            output = a_output;
            Rules = a_rules;
            Problem = new List<FuzzyValue>();
        }

        public void AddInputVariable(LinguisticVariable a_variable)
        {
            Inputs.Add(a_variable);
        }

        public void AddOutputVariable(LinguisticVariable a_variable)
        {
            output = a_variable;
        }

        public void AddRule(FuzzyRule a_rule)
        {
            Rules.Add(a_rule);
        }

        public void SetInputVariable(LinguisticVariable a_inputVariable, double a_value)
        {
            Problem.Add(new FuzzyValue(a_inputVariable, a_value));
        }

        public void ResetCase()
        {
            Problem.Clear();
        }

        public LinguisticVariable LinguisticVariableByName(string a_name)
        {
            foreach(var input in Inputs)
            {
                if(input.Name.ToUpper() == a_name.ToUpper())
                {
                    return input;
                }
            }
            if(Output.Name.ToUpper() == a_name.ToUpper())
            {
                return output;
            }
            return null;
        }


        public double Solve()
        {
            FuzzySet result = new FuzzySet();
            result.AddPoint(output.MinValue, 0);
            result.AddPoint(output.MaxValue, 0);

            foreach (var rule in Rules)
            {
                FuzzySet resultingFuzzySet = rule.Apply(Problem);
                if(resultingFuzzySet != null)
                {
                    result = result | resultingFuzzySet;
                }
            }
            return result.Barycenter;
        }
    }
}
