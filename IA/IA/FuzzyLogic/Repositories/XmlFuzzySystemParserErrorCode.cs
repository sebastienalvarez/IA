/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Codes erreur du parser de système à base d elogique floue fourni en XML
 */


namespace IA.FuzzyLogic.Repositories
{
    public enum XmlFuzzySystemParserErrorCode
    {
        SUCCESS,
        ERROR_NOT_XML_FILE,
        ERROR_INCOMPLETE_XML_FILE,
        ERROR_FILE_NOT_FOUND,
        ERROR_INCORRECT_NUMBER_OF_INPUTS,
        ERROR_INPUTS,
        ERROR_INCORRECT_NUMBER_OF_OUTPUT,
        ERROR_OUTPUT,
        ERROR_INCORRECT_NUMBER_OF_RULES,
        ERROR_RULES,
        ERROR_UNKNOWN
    }
}