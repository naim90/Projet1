using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class DetailsEleveScreen : NavigableScreen
    {
        ICollection<Eleve> eleves;
        ICollection<Cours> cours;

        public DetailsEleveScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            this.screenTitle = "DETAILS D'UN ELEVE";
            this.actions = eleves.Select(e => e.Nom + " " + e.Prenom).ToList();
            this.actions.Add("Retour au menu précédent");
        }

        protected override void ChangeScreen()
        {
            if (choix == actions.Count - 1)
            {
                FileUtils.Log("Retour au menu des élèves");
                exitScreen = true;
                return;
            }

            Eleve eleve = eleves.ElementAt(choix);

            FileUtils.Log("Affichage des détails de l'élève " + eleve.Nom + " " + eleve.Prenom);

            foreach (string line in eleve.DisplayFormated(cours.ToList()))
            {
                screenDatas.Add(line);
            }
        }
    }
}
