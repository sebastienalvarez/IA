/*
 * PROJET : Bibliothèque C# Intelligence Artificielle
 * SUJET : Implémentation des techniques d'Intelligence Artificielle du livre "L'Intelligence Artificielle pour les développeurs Concepts et implémentations en C#"
 * AUTEUR : Sébastien ALVAREZ
 * VERSION : 1.0
 * DATE : Février 2019
 * REVISION : NA
 * 
 * Description : Codes erreur du parser de base de règles fourni en XML
 */


namespace IA.ExpertSystem.Repositories
{
    public enum XmlRulesBaseParserErrorCode
    {
        SUCCESS,
        ERROR_NOT_XML_FILE,
        ERROR_INCOMPLETE_XML_FILE,
        ERROR_FILE_NOT_FOUND,
        ERROR_UNKNOWN
    }
}