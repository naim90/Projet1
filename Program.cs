using System.Collections.Generic;
using NotEdu.entities;
using NotEdu.screens;
using NotEdu.utils;

namespace NotEdu
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<Cours> cours;
            ICollection<Eleve> eleves;

            string jsonFile = (args.Length > 0 && !string.IsNullOrEmpty(args[0])) ? args[0] : "./NotEdu.json";

            FileUtils.ConfigureJsonFile(jsonFile);
            FileUtils.ChargerJson(out cours, out eleves);
            new MainScreen(
                eleves, 
                cours
            ).Display();
        }
    }
}
