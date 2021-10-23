using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class SaisirNotePourUnEleveScreen : NavigableScreen
    {
        ICollection<Cours> cours;
        Eleve eleve;
        public SaisirNotePourUnEleveScreen(ICollection<Cours> cours, Eleve eleve)
        {
            this.cours = cours;
            this.eleve = eleve;

            this.screenTitle = "AJOUT D'UN RESULTAT POUR L'ELEVE " + eleve.Nom.ToUpper() + " " + eleve.Prenom.ToUpper();
            this.actions = cours.Select(c => c.Nom).ToList();
            actions.Add("Revenir au menu précédent");
        }

        private void RetourMenuPrecedent()
        {
            FileUtils.Log("Retour au menu des élèves");
            exitScreen = true;
        }

        private string Ask(string question)
        {
            Console.Write(" " + question);
            return Console.ReadLine();
        }

        private void AjouterResultat(Cours cours, double note, string appreciation)
        {
            FileUtils.Log("Enregistrement du résultat pour le cours " + cours.Nom + " : " + note + "/20 - " + appreciation);
            eleve.Resultats.Add(new Resultat(cours.Id, note, appreciation));
        }

        private void SaisirResultatCours(Cours cours)
        {
            do
            {
                Console.WriteLine();
                string noteString = Ask("Note : ");
                double note;
                bool noteEstValide = double.TryParse(noteString, out note);
                if (!noteEstValide || note < 0 || note > 20)
                {
                    Console.WriteLine(" La note doit être comprise entre 0 et 20");
                }
                else
                {
                    string appreciation = Ask("Remarque : ");
                    AjouterResultat(cours, note, appreciation);
                    break;
                }
            } while (true);
        }

        protected override void ChangeScreen()
        {
            if (choix != actions.Count - 1)
            {
                Cours coursANoter = cours.ElementAt(choix);

                if (eleve.Resultats.FirstOrDefault(r => r.CoursId == coursANoter.Id) != null)
                {
                    Console.WriteLine(" Une note a déjà été saisie pour ce cours. Merci d'en sélectionner un autre.");
                    Console.WriteLine(" Appuyez sur une touche pour changer votre saisie");
                    Console.ReadKey();
                }
                else
                {
                    SaisirResultatCours(coursANoter);
                    RetourMenuPrecedent();
                }                
            }
            else
            {
                RetourMenuPrecedent();
            }
        }
    }
}
