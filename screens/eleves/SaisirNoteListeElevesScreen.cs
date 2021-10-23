using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class SaisirNoteListeElevesScreen : NavigableScreen
    {
        ICollection<Cours> cours;
        ICollection<Eleve> eleves;
        public SaisirNoteListeElevesScreen(ICollection<Cours> cours, ICollection<Eleve> eleves)
        {
            this.cours = cours;
            this.eleves = eleves;

            this.screenTitle = "AJOUTER UN RESULTAT POUR UN ELEVE";
            this.actions = eleves.Select(e => e.Nom + " " + e.Prenom).ToList();
            this.actions.Add("Revenir au menu précédent");
        }

        protected override void ChangeScreen()
        {
            if (choix == actions.Count - 1)
            {
                FileUtils.Log("Retour au menu des élèves");
                exitScreen = true;
                return;
            }

            Eleve eleveANoter = eleves.ElementAt(choix);

            FileUtils.Log("Affichage de l'écran de saisie d'une note pour l'élève \"" + eleveANoter.Nom + " " + eleveANoter.Prenom + "\"");
            new SaisirNotePourUnEleveScreen(cours, eleveANoter).Display();
            FileUtils.SauvegarderJson(cours, eleves);
        }
    }
}
