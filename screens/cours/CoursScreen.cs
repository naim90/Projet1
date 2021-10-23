using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.cours
{
    class CoursScreen : NavigableScreen
    {
        ICollection<Eleve> eleves;
        ICollection<Cours> cours;

        public CoursScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            this.screenTitle = "COURS";
            this.actions = new List<string>
            {
                "Lister les cours existants",
                "Ajouter un cours au programme",
                "Supprimer un cours du programme",
                "Revenir au menu principal"
            };
        }

        private void ListerLesCours()
        {
            if (cours.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des cours");
                foreach (Cours c in cours)
                {
                    foreach (string line in c.DisplayFormated())
                    {
                        screenDatas.Add(line);
                    }
                    screenDatas.Add("");
                }
                screenDatas.RemoveAt(screenDatas.Count - 1);
            }
            else
            {
                FileUtils.Log("Affichage de la liste des cours vide");
                screenDatas.Add("Il n'existe encore aucun cours dans le système");
            }
        }

        private void AjouterUnCours()
        {
            Console.Write("Saisissez le nom du cours : ");
            string nom = Console.ReadLine();

            if (!string.IsNullOrEmpty(nom))
            {
                string nomCours = StringUtils.PascalCaseString(nom);
                FileUtils.Log("Ajout du cours " + nomCours);
                cours.Add(
                    new Cours(
                        cours.Count == 0 ? 1 : cours.Max(c => c.Id) + 1,
                        nomCours
                    )
                );                
                FileUtils.SauvegarderJson(cours, eleves);
            }
        }

        private void SupprimerUnCours()
        {
            if (cours.Count == 0)
            {
                FileUtils.Log("Erreur lors de la suppression d'un cours alors qu'il n'en existe aucun");
                screenDatas.Add("Il n'existe aucun cours à supprimer dans le système");
                return;
            }

            FileUtils.Log("Affichage de l'écran de suppression d'un cours");
            new SuppressionCoursScreen(eleves, cours).Display();
            FileUtils.SauvegarderJson(cours, eleves);
        }

        override
        protected void ChangeScreen()
        {
            if (choix == actions.Count - 1)
            {
                FileUtils.Log("Retour au menu principal");
                exitScreen = true;
                return;
            }

            switch (choix)
            {
                case 0:
                    ListerLesCours();
                    break;
                case 1:
                    AjouterUnCours();
                    break;
                case 2:
                    SupprimerUnCours();
                    break;
            }
        }
    }
}
