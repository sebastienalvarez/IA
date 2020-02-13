/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Août 2018
 * REVISION : Version 2.0 de Février 2019 => modifications legères pour implémenter l'interface IRulesBaseParserRepository
 * 
 * Description : Parser de base de règles fourni en XML
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using IA.ExpertSystem.Interfaces;
using IA.ExpertSystem.Engine;

namespace IA.ExpertSystem.Repositories
{
    public class XmlRulesBaseParser : IRulesBaseParserRepository
    {
        // PROPRIETES
        private RulesBase rulesBase;
        /// <summary>
        /// RulesBase instance generated from parsing
        /// </summary>
        public RulesBase RulesBase
        {
            get { return rulesBase; }
        }

        private string name;
        /// <summary>
        /// Name of the rules base generated from parsing
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private float version;
        /// <summary>
        /// version of the rules name generated from parsing
        /// </summary>
        public float Version
        {
            get { return version; }
        }

        private DateTime date;
        /// <summary>
        /// Date of creation of the rules base generated from parsing
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }

        private XmlRulesBaseParserErrorCode errorCode;
        /// <summary>
        /// Error code generated from parsing
        /// </summary>
        public XmlRulesBaseParserErrorCode ErrorCode
        {
            get { return errorCode; }
        }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the XmlRulesBaseParser
        /// </summary>
        public XmlRulesBaseParser()
        {
            name = null;
            version = 0.0f;
            date = DateTime.Today;
            rulesBase = new RulesBase();
            errorCode = XmlRulesBaseParserErrorCode.SUCCESS;
        }

        /// <summary>
        /// Creates and returns RulesBase object from the given XML file, argument must be a valid xml file path, otherwise null is returned
        /// </summary>
        /// <param name="a_source">XML Source file path</param>
        /// <returns>RulesBase object generated from an XML file, null is returned if parsing is unsuccessful</returns>
        public RulesBase CreateRulesBase(object a_source)
        {
            if (a_source != null)
            {
                try
                {
                    string fileName = (string)a_source;
                    XDocument xmlDoc = XDocument.Load(fileName);
                   
                    var ruleElements = from XElement element in xmlDoc.Descendants("Rule")
                                       select element;

                    foreach (XElement element in ruleElements)
                    {
                        Rule newRule = ParseRule(element);
                        if (newRule == null)
                        {
                            throw new NullReferenceException(); // Pour affecter errorCode => voir catch
                        }
                        rulesBase.Rules.Add(newRule);
                    }

                    name = xmlDoc.Descendants("RulesBaseInfo").FirstOrDefault().Descendants("Name").FirstOrDefault().Value;

                    float version = 0.0f;
                    if (float.TryParse(xmlDoc.Descendants("RulesBaseInfo").FirstOrDefault().Descendants("Issue").FirstOrDefault().Value, out version))
                    {
                        this.version = version;
                    }
                    int day = 1;
                    bool isDayCorrect = int.TryParse(xmlDoc.Descendants("RulesBaseInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("day").Value, out day);
                    int month = 1;
                    bool isMonthCorrect = int.TryParse(xmlDoc.Descendants("RulesBaseInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("month").Value, out month);
                    int year = 0;
                    bool isYearCorrect = int.TryParse(xmlDoc.Descendants("RulesBaseInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("year").Value, out year);
                    if (isDayCorrect && isMonthCorrect && isYearCorrect)
                    {
                        try
                        {
                            date = new DateTime(year, month, day);
                        }
                        catch (Exception)
                        {
                            // Format de la date incorrect
                            // Pas d'action, donnée non essentielle
                        }
                    }
                }
                catch (XmlException)
                {
                    errorCode = XmlRulesBaseParserErrorCode.ERROR_NOT_XML_FILE;
                    rulesBase = null;
                }
                catch (NullReferenceException)
                {
                    errorCode = XmlRulesBaseParserErrorCode.ERROR_INCOMPLETE_XML_FILE;
                    rulesBase = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType() + "   " + ex.Message + "    " + ex.Source.ToString());
                    errorCode = XmlRulesBaseParserErrorCode.ERROR_UNKNOWN;
                    rulesBase = null;
                }
            }
            return rulesBase;
        }

        /// <summary>
        /// Parses an XML Fact element
        /// </summary>
        /// <param name="a_fact">XML Fact element</param>
        /// <returns>Corresponding Fact object or null if parsing is unsuccessful</returns>
        protected IFact ParseFact(XElement a_fact)
        {
            // Variables pour le parsing
            string name;
            string valueType;
            string valueString;
            bool valueBool = true;
            int valueInt = 0;
            string question;

            // Parsing de la balise <Fact>
            name = a_fact.Descendants("Name").FirstOrDefault().Value;
            if (name == null || name == string.Empty)
            {
                return null;
            }

            valueType = a_fact.Descendants("Value").FirstOrDefault().Attribute("type").Value;
            if (valueType.ToLower() != "boolean" && valueType.ToLower() != "integer")
            {
                return null;
            }

            valueString = a_fact.Descendants("Value").FirstOrDefault().Value;
            if (valueString == null || valueString == string.Empty)
            {
                return null;
            }

            if (valueType.ToLower() == "boolean")
            {
                bool isParsing = bool.TryParse(a_fact.Descendants("Value").FirstOrDefault().Value, out valueBool);
                if (!isParsing)
                {
                    return null;
                }
            }
            else
            {
                bool isParsing = int.TryParse(a_fact.Descendants("Value").FirstOrDefault().Value, out valueInt);
                if (!isParsing)
                {
                    return null;
                }
            }

            question = a_fact.Descendants("Question").FirstOrDefault().Value;
            if (question == null)
            {
                return null;
            }

            // Si on arrive ici, la balise <Fact> est conforme
            IFact newFact;
            if (question == string.Empty)
            {
                question = null;
            }
            if (valueType.ToLower() == "boolean")
            {
                newFact = new BoolFact(name, valueBool, 0, question);
            }
            else
            {
                newFact = new IntFact(name, valueInt, 0, question);
            }

            return newFact;
        }

        /// <summary>
        /// Parses an XML Rule element
        /// </summary>
        /// <param name="a_rule">XML Rule element</param>
        /// <returns>Corresponding Rule object or null if parsing is unsuccessful</returns>
        protected Rule ParseRule(XElement a_rule)
        {
            // Variables pour le parsing
            string name;
            XElement premisesElement;
            List<XElement> factListElements = new List<XElement>();
            XElement factElement;
            List<IFact> premises = new List<IFact>();
            IFact conclusion;

            // Parsing de la balise <Rule>
            name = a_rule.Descendants("Name").FirstOrDefault().Value;
            if (name == null || name == string.Empty)
            {
                return null;
            }

            premisesElement = a_rule.Descendants("Premises").FirstOrDefault();
            if (premisesElement == null)
            {
                return null;
            }

            factListElements = premisesElement.Descendants("Fact").ToList();
            if (factListElements.Count == 0)
            {
                return null;
            }

            factElement = a_rule.Descendants("Conclusion").FirstOrDefault().Descendants("Fact").FirstOrDefault();
            if (factElement == null)
            {
                return null;
            }

            // Si on arrive ici, la balise <Rule> est conforme (parsing des balises <Fact>)
            // Parsing des balises <Fact>
            foreach (XElement elementFact in factListElements)
            {
                IFact newFact = ParseFact(elementFact);
                if (newFact == null)
                {
                    return null;
                }
                premises.Add(newFact);
            }

            conclusion = ParseFact(factElement);
            if (conclusion == null)
            {
                return null;
            }

            // Si on arrive ici, la balise <Rule> est conforme
            Rule newRule = new Rule(name, premises, conclusion);
            return newRule;
        }
    }
}