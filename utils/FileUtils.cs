using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NotEdu.entities;

namespace NotEdu.utils
{
    public static class FileUtils
    {
        private static string jsonFile;

        public static void ConfigureJsonFile(string filePath)
        {
            jsonFile = filePath;
        }

        public static void ChargerJson(out ICollection<Cours> cours, out ICollection<Eleve> eleves)
        {
            if (File.Exists(jsonFile))
            {
                Log("Chargement du fichier JSON \"" + jsonFile + "\"");
                string json = File.ReadAllText(jsonFile);

                (ICollection<Cours>, ICollection<Eleve>) contenu = JsonConvert.DeserializeObject<(ICollection<Cours>, ICollection<Eleve>)>(json);

                cours = contenu.Item1;
                Log("Nombre de cours : " + cours.Count);
                eleves = contenu.Item2;
                Log("Nombre d'élèves : " + eleves.Count);
            }
            else
            {
                Log("Le fichier \"" + jsonFile + "\" n'existe pas et n'a pas pu être chargé. Initialisation des données vide.");
                cours = new List<Cours>();
                eleves = new List<Eleve>();
            }            
        }

        public static void SauvegarderJson(ICollection<Cours> cours, ICollection<Eleve> eleves)
        {
            (ICollection<Cours>, ICollection<Eleve>) contenu = (cours, eleves);

            string json = JsonConvert.SerializeObject(contenu);

            Log("Enregistrement des données dans le fichier \"" + jsonFile + "\"");
            Log("Nombre de cours : " + cours.Count);
            Log("Nombre d'élèves : " + eleves.Count);
            File.WriteAllText(jsonFile, json);
        }

        public static void Log(string log)
        {
            string logFile = Path.Combine(Path.GetDirectoryName(jsonFile), "NotEdu.log");

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string line = date + " - " + log;

            File.AppendAllLines(logFile, new List<string> { line });
        }
    }
}
