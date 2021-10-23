using System;
using System.Collections.Generic;
using NotEdu.utils;

namespace NotEdu.entities
{
    public class Eleve
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string AnneeDePromotion{ get; set; }

        public List<Resultat> Resultats;


        public Eleve(int id, string nom, string prenom, DateTime dateNaissance, List<Resultat> resultats, string anneeDePromotion)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            Resultats = resultats ?? new List<Resultat>();
            //string toto = AnneeDePromotion.Year.ToString();
            AnneeDePromotion= anneeDePromotion;
        }

        public IList<string> DisplayBasicInformations()
        {
            IList<string> lines = new List<string>();

            lines.Add("Nom               : " + Nom);
            lines.Add("Prénom            : " + Prenom);
            lines.Add("Date de naissance : " + DateNaissance.ToString("yyyy/MM/dd"));
            lines.Add("Annee de promotion: " + AnneeDePromotion);

            return lines;
        }

        public IList<string> DisplayFormated(IList<Cours> cours)
        {
            IList<string> lines = DisplayBasicInformations();
            lines.Add("");

            if (Resultats.Count > 0)
            {
                lines.Add("Résultats : ");

                double somme = 0;

                foreach (Resultat r in Resultats)
                {
                    somme += r.Note;
                    foreach (string l in r.DisplayFormated(cours))
                    {
                        lines.Add("    " + l);
                    }
                    lines.Add("");
                }

                lines.Add("    Moyenne de l'élève : " + MathUtils.FormatNote(MathUtils.ArrondirNote(somme / Resultats.Count)));
            }
            else
            {
                lines.Add("Pas encore de résultats pour cet élève.");
            }
            
            return lines;
        }
    }
}
