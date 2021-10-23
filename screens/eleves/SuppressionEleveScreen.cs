using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class SuppressionEleveScreen : NavigableScreen
    {
        ICollection<Eleve> eleves;

        public SuppressionEleveScreen(ICollection<Eleve> eleves)
        {
            this.eleves = eleves;

            this.screenTitle = "SUPPRESSION D'UN ELEVE";
            this.actions = eleves.Select(e => e.Nom + " " + e.Prenom).ToList();
            this.actions.Add("Retour au menu des élèves");
        }

        private void RetourMenuPrecedent()
        {
            FileUtils.Log("Retour au menu des élèves");
            exitScreen = true;
        }

        protected override void ChangeScreen()
        {
            if (choix != actions.Count - 1)
            {
                Eleve eleveASupprimer = eleves.ElementAt(choix);
                string eleve = eleveASupprimer.Nom + " " + eleveASupprimer.Prenom;

                Console.Write("Confirmez vous la suppression de l'élève \"" + eleve + "\" ? (Y/N) : ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    FileUtils.Log("Suppression de l'élève " + eleve);
                    eleves.Remove(eleveASupprimer);
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
