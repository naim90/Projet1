using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.cours
{
    class SuppressionCoursScreen : NavigableScreen
    {
        ICollection<Eleve> eleves;
        ICollection<Cours> cours;

        public SuppressionCoursScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            this.screenTitle = "SUPPRESSION D'UN COURS";
            this.actions = cours.Select(c => c.Nom).ToList();
            this.actions.Add("Retour au menu précédent");
        }

        private void RetourMenuPrecedent()
        {
            FileUtils.Log("Retour au menu des cours");
            exitScreen = true;
        }

        protected override void ChangeScreen()
        {
            if (choix != actions.Count - 1)
            {
                Cours coursASupprimer = cours.ElementAt(choix);

                Console.Write("Confirmez vous la suppression du cours \"" + coursASupprimer.Nom + "\" ? (Y/N) : ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    FileUtils.Log("Suppression du cours " + coursASupprimer.Nom);
                    cours.Remove(coursASupprimer);
                    foreach (Eleve eleve in eleves)
                    {
                        eleve.Resultats.RemoveAll(r => r.CoursId == coursASupprimer.Id);
                    }
                    RetourMenuPrecedent();
                }
            } else
            {
                RetourMenuPrecedent();
            }
        }
    }
}
