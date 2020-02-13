/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Parser de système à base logique fourni en XML
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using IA.FuzzyLogic.Engine;
using IA.FuzzyLogic.Interfaces;

namespace IA.FuzzyLogic.Repositories
{
    public class XmlFuzzySystemParser : IFuzzySystemParserRepository
    {
        // PROPRIETES
        private List<LinguisticVariable> inputs;
        /// <summary>
        /// LinguisticVariable collection generated from parsing and constituting inputs
        /// </summary>
        public List<LinguisticVariable> Inputs
        {
            get { return inputs; }
        }

        private LinguisticVariable output;
        /// <summary>
        /// LinguisticVariable instance generated from parsing and constituting output
        /// </summary>
        public LinguisticVariable Output
        {
            get { return output; }
        }

        private List<FuzzyRule> rules;
        /// <summary>
        /// LinguisticRule collection generated from parsing and constituting fuzzy rules
        /// </summary>
        public List<FuzzyRule> Rules
        {
            get { return rules; }
        }

        private string name;
        /// <summary>
        /// Name of the fuzzy system generated from parsing
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        private float version;
        /// <summary>
        /// version of the fuzzy system generated from parsing
        /// </summary>
        public float Version
        {
            get { return version; }
        }

        private DateTime date;
        /// <summary>
        /// Date of creation of the fuzzy system generated from parsing
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }

        private XmlFuzzySystemParserErrorCode errorCode;
        /// <summary>
        /// Error code generated from parsing
        /// </summary>
        public XmlFuzzySystemParserErrorCode ErrorCode
        {
            get { return errorCode; }
        }

        // CONSTRUCTEUR
        /// <summary>
        /// Creates an instance of the XmlFuzzySystemParser
        /// </summary>
        public XmlFuzzySystemParser()
        {
            inputs = new List<LinguisticVariable>();
            rules = new List<FuzzyRule>();
        }

        // METHODES PUBLIQUES
        /// <summary>
        /// Creates a Fuzzy System from the given XML file, argument must be a valid xml file path, otherwise false value is returned
        /// </summary>
        /// <param name="a_source">XML Source file path</param>
        /// <returns>True is returned if parsing is successful (false otherwise)</returns>
        public bool CreateFuzzySystem(object a_source)
        {
            if (a_source != null)
            {
                try
                {
                    string fileName = (string)a_source;
                    XDocument xmlDoc = XDocument.Load(fileName);

                    // Parsing de la balise <FuzzySystemInfo>
                    name = xmlDoc.Descendants("FuzzySystemInfo").FirstOrDefault().Descendants("Name").FirstOrDefault().Value;

                    float version = 0.0f;
                    if (float.TryParse(xmlDoc.Descendants("FuzzySystemInfo").FirstOrDefault().Descendants("Issue").FirstOrDefault().Value, out version))
                    {
                        this.version = version;
                    }
                    int day = 1;
                    bool isDayCorrect = int.TryParse(xmlDoc.Descendants("FuzzySystemInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("day").Value, out day);
                    int month = 1;
                    bool isMonthCorrect = int.TryParse(xmlDoc.Descendants("FuzzySystemInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("month").Value, out month);
                    int year = 0;
                    bool isYearCorrect = int.TryParse(xmlDoc.Descendants("FuzzySystemInfo").FirstOrDefault().Descendants("Date").FirstOrDefault().Attribute("year").Value, out year);
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

                    // Parsing de la balise <InputLinguisticVariableDefinitions>
                    var inputElements = xmlDoc.Descendants("InputLinguisticVariable");
                    if (inputElements.Count() < 1)
                    {
                        errorCode = XmlFuzzySystemParserErrorCode.ERROR_INCORRECT_NUMBER_OF_INPUTS;
                        inputs = null;
                        output = null;
                        rules = null;
                        return false;
                    }
                    foreach (var element in inputElements)
                    {
                        LinguisticVariable variable = ParseLinguisticVariableElement(element);
                        if (variable == null)
                        {
                            errorCode = XmlFuzzySystemParserErrorCode.ERROR_INPUTS;
                            inputs = null;
                            output = null;
                            rules = null;
                            return false;
                        }
                        inputs.Add(variable);
                    }

                    // Parsing de la balise <OutputLinguisticVariableDefinition>
                    var outputElements = xmlDoc.Descendants("OutputLinguisticVariableDefinition");

                    if (outputElements.Count() != 1 || outputElements.Descendants("OutputLinguisticVariable") == null)
                    {
                        errorCode = XmlFuzzySystemParserErrorCode.ERROR_INCORRECT_NUMBER_OF_OUTPUT;
                        inputs = null;
                        output = null;
                        rules = null;
                        return false;
                    }
                    else
                    {
                        output = ParseLinguisticVariableElement(outputElements.Descendants("OutputLinguisticVariable").FirstOrDefault());
                    }

                    // Parsing de la balise <FuzzyRuleDefinitions>
                    var ruleElements = xmlDoc.Descendants("FuzzyRule");
                    if (ruleElements.Count() < 1)
                    {
                        errorCode = XmlFuzzySystemParserErrorCode.ERROR_INCORRECT_NUMBER_OF_RULES;
                        inputs = null;
                        output = null;
                        rules = null;
                        return false;
                    }
                    else
                    {
                        foreach(var element in ruleElements)
                        {
                            FuzzyRule rule = ParseFuzzyRuleElement(element);
                            if (rule == null)
                            {
                                errorCode = XmlFuzzySystemParserErrorCode.ERROR_RULES;
                                inputs = null;
                                output = null;
                                rules = null;
                                return false;
                            }
                            rules.Add(rule);
                        }
                    }
                }
                catch (XmlException)
                {
                    errorCode = XmlFuzzySystemParserErrorCode.ERROR_NOT_XML_FILE;
                    inputs = null;
                    output = null;
                    rules = null;
                    return false;
                }
                catch (NullReferenceException)
                {
                    errorCode = XmlFuzzySystemParserErrorCode.ERROR_INCOMPLETE_XML_FILE;
                    inputs = null;
                    output = null;
                    rules = null;
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType() + "   " + ex.Message + "    " + ex.Source.ToString());
                    errorCode = XmlFuzzySystemParserErrorCode.ERROR_UNKNOWN;
                    inputs = null;
                    output = null;
                    rules = null;
                    return false;
                }

                // If we get there, parsing is successful
                return true;
            }
            errorCode = XmlFuzzySystemParserErrorCode.ERROR_NOT_XML_FILE;
            return false;
        }

        /// <summary>
        /// Gets collection of LinguisticVariable objects generated from parsing and constituting inputs
        /// </summary>
        /// <returns>Collection of LinguisticVariable objects generated from parsing and constituting inputs or null id parsing is unsuccessful</returns>
        public List<LinguisticVariable> GetInputs()
        {
            if(inputs != null)
            {
                return inputs;
            }
            return null;
        }

        /// <summary>
        /// Gets LinguisticVariable object generated from parsing and constituting output
        /// </summary>
        /// <returns>LinguisticVariable object generated from parsing and constituting output or null id parsing is unsuccessful</returns>
        public LinguisticVariable GetOutput()
        {
            if(output != null)
            {
                return output;
            }
            return null;
        }

        /// <summary>
        /// Gets collection of FuzzyRule objects generated from parsing and constituting fuzzy rules 
        /// </summary>
        /// <returns>Collection of FuzzyRule objects generated from parsing and constituting fuzzy rules</returns>
        public List<FuzzyRule> GetRules()
        {
            if(rules != null)
            {
                return rules;
            }
            return null;
        }

        // METHODES PRIVEES
        /// <summary>
        /// Parses an XML LinguisticVariable element
        /// </summary>
        /// <param name="a_linguisticVariable">XML LinguisticVariable element</param>
        /// <returns>Corresponding LinguisticVariable object or null if parsing is unsuccessful</returns>
        protected LinguisticVariable ParseLinguisticVariableElement(XElement a_linguisticVariable)
        {
            // Variables pour le parsing
            string name;
            List<XElement> linguisticValueElements = new List<XElement>();
            LinguisticValue linguisticValue;
            List<LinguisticValue> linguisticValues = new List<LinguisticValue>();

            // Parsing de la balise <OutputLinguisticVariable>
            name = a_linguisticVariable.Descendants("Name").FirstOrDefault().Value;
            if (name == null || name == string.Empty)
            {
                return null;
            }

            linguisticValueElements = a_linguisticVariable.Descendants("LinguisticValueDefinitions").FirstOrDefault().Descendants("LinguisticValue").ToList();
            if (linguisticValueElements == null)
            {
                return null;
            }
            foreach(var element in linguisticValueElements)
            {
                linguisticValue = ParseLinguisticValueElement(element);
                if(linguisticValue == null)
                {
                    return null;
                }
                linguisticValues.Add(linguisticValue);
            }

            // Si on arrive ici, la balise <InputLinguisticVariable> est conforme
            return new LinguisticVariable(name, linguisticValues);
        }

        /// <summary>
        /// Parses an XML LinguisticValue element
        /// </summary>
        /// <param name="a_linguisticValue">XML LinguisticValue element</param>
        /// <returns>Corresponding LinguisticValue object or null if parsing is unsuccessful</returns>
        protected LinguisticValue ParseLinguisticValueElement(XElement a_linguisticValue)
        {
            string name = null;
            FuzzySet fs = null;

            foreach (var element in a_linguisticValue.Descendants())
            {
                if (element.Name.LocalName == "Name")
                {
                    name = element.Value;
                }
                if (element.Name.LocalName == "RampFuzzySet")
                {
                    bool isParsingCorrect = true;
                    double zero;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Zero").FirstOrDefault().Value, out zero)))
                    {
                        return null;
                    }
                    double one;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("One").FirstOrDefault().Value, out one)))
                    {
                        return null;
                    }
                    double min;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Min").FirstOrDefault().Value, out min)))
                    {
                        return null;
                    }
                    double max;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Max").FirstOrDefault().Value, out max)))
                    {
                        return null;
                    }
                    // If we get here, parsing is successful
                    try
                    {
                        fs = new RampFuzzySet(one, zero, min, max);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (element.Name.LocalName == "TrapezoidalFuzzySet")
                {
                    bool isParsingCorrect = true;
                    double leftZero;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("LeftZero").FirstOrDefault().Value, out leftZero)))
                    {
                        return null;
                    }
                    double leftOne;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("LeftOne").FirstOrDefault().Value, out leftOne)))
                    {
                        return null;
                    }
                    double rightZero;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("RightZero").FirstOrDefault().Value, out rightZero)))
                    {
                        return null;
                    }
                    double rightOne;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("RightOne").FirstOrDefault().Value, out rightOne)))
                    {
                        return null;
                    }
                    double min;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Min").FirstOrDefault().Value, out min)))
                    {
                        return null;
                    }
                    double max;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Max").FirstOrDefault().Value, out max)))
                    {
                        return null;
                    }
                    // If we get here, parsing is successful
                    try
                    {
                        fs = new TrapezoidalFuzzySet(leftZero, leftOne, rightOne, rightZero, min, max);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }
                if (element.Name.LocalName == "TriangularFuzzySet")
                {
                    bool isParsingCorrect = true;
                    double left;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Left").FirstOrDefault().Value, out left)))
                    {
                        return null;
                    }
                    double center;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Center").FirstOrDefault().Value, out center)))
                    {
                        return null;
                    }
                    double right;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Right").FirstOrDefault().Value, out right)))
                    {
                        return null;
                    }
                    double min;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Min").FirstOrDefault().Value, out min)))
                    {
                        return null;
                    }
                    double max;
                    if (!(isParsingCorrect = double.TryParse(element.Descendants("Max").FirstOrDefault().Value, out max)))
                    {
                        return null;
                    }
                    // If we get here, parsing is successful
                    try
                    {
                        fs = new TriangularFuzzySet(left, center, right, min, max);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            if (name != null && name != string.Empty && fs != null)
            {
                return new LinguisticValue(name, fs);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses an XML FuzzyRule element
        /// </summary>
        /// <param name="a_fuzzyRule">XML FuzzyRule element</param>
        /// <returns>Corresponding FuzzyRule object or null if parsing is unsuccessful</returns>
        protected FuzzyRule ParseFuzzyRuleElement(XElement a_fuzzyRule)
        {
            List<FuzzyExpression> premises = new List<FuzzyExpression>();
            FuzzyExpression conclusion = null;

            if(a_fuzzyRule.Descendants().Count() != 0)
            {
                foreach (var element in a_fuzzyRule.Elements())
                {
                    if(element.Name.LocalName == "Premises")
                    {
                        foreach(var premise in element.Descendants("FuzzyExpression"))
                        {
                            FuzzyExpression expression = ParseFuzzyExpression(premise, true);
                            if(expression == null)
                            {
                                return null;
                            }
                            premises.Add(expression);
                        }
                    }
                    int count = element.Descendants("FuzzyExpression").Count();
                    if (element.Name.LocalName == "Conclusion" && element.Descendants("FuzzyExpression").Count() == 1)
                    {
                        conclusion = ParseFuzzyExpression(element.Descendants().FirstOrDefault(), false);
                        if(conclusion == null)
                        {
                            return null;
                        }
                    }
                }
                if (premises.Count > 0 && conclusion != null)
                {
                    return new FuzzyRule(premises, conclusion);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses an XML FuzzyExpression element
        /// </summary>
        /// <param name="a_expression">XML FuzzyExpression element</param>
        /// <param name="a_isPremise">Corresponding FuzzyExpression object or null if parsing is unsuccessful</param>
        /// <returns></returns>
        private FuzzyExpression ParseFuzzyExpression(XElement a_expression, bool a_isPremise)
        {
            LinguisticVariable linguisticVariable = null;
            string variableName = a_expression.Descendants("LinguisticVariableName").FirstOrDefault().Value;
            string valueName = a_expression.Descendants("LinguisticValueName").FirstOrDefault().Value;
            if (variableName != null && variableName != string.Empty && valueName != null && valueName != string.Empty)
            {
                if (a_isPremise)
                {
                    foreach (var variable in inputs)
                    {
                        if(variable.Name == variableName)
                        {
                            linguisticVariable = variable;
                        }
                    }
                }
                else
                {
                    if(output.Name == variableName)
                    {
                        linguisticVariable = output;
                    }
                }
            }
            if(linguisticVariable != null && linguisticVariable.Values.Where(x => x.Name == valueName).Count() != 0)
            {
                return new FuzzyExpression(linguisticVariable, valueName);
            }
            else
            {
                return null;
            }
        }
    }
}