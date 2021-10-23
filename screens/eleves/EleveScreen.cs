using System.Collections.Generic;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class EleveScreen : NavigableScreen
    {
        private ICollection<Eleve> eleves;
        private ICollection<Cours> cours;
        
        public EleveScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            this.screenTitle = "ELEVES";
            this.actions = new List<string>
            {
                "Lister les élèves",
                "Ajouter un élève",
                "Consulter les détails d'un élève",
                "Ajouter une note pour un élève",
                "Supprimer un élève",
                "Revenir au menu principal"
            };
        }


        private void ListerLesEleves()
        {
            if (eleves.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des élèves");
                foreach (Eleve e in eleves)
                {
                    foreach (string line in e.DisplayBasicInformations())
                    {
                        screenDatas.Add(line);
                    }
                    screenDatas.Add("");
                }
                screenDatas.RemoveAt(screenDatas.Count - 1);
            }
            else
            {
                FileUtils.Log("Affichage de la liste des élèves vide");
                screenDatas.Add("Il n'existe encore aucun élève dans le système");
            }
        }

        private void AjouterUnEleve()
        {
            FileUtils.Log("Affichage de l'écran d'ajout d'un élève");
            new AjoutEleveScreen(eleves).Display();
            FileUtils.SauvegarderJson(cours, eleves);
        }

        private void AfficherDetailsEleve()
        {
            if (eleves.Count == 0)
            {
                FileUtils.Log("Erreur lors de l'affichage des détails d'un élève alors qu'il n'en existe aucun dans le système");
                screenDatas.Add("Il n'existe aucun élève à afficher dans le système");
                return;
            }

            FileUtils.Log("Affichage de l'écran des détails élèves");
            new DetailsEleveScreen(eleves, cours).Display();
        }

        private void SaisirNoteEleve()
        {
            if (eleves.Count == 0)
            {
                FileUtils.Log("Erreur lors de la saisie d'une note alors qu'il n'existe aucun élève dans le système");
                screenDatas.Add("Il n'existe aucun élève à noter dans le système");
                return;
            }
            if (cours.Count == 0)
            {
                FileUtils.Log("Erreur lors de la saisie d'une note alors qu'il n'existe aucun cours dans le système");
                screenDatas.Add("Il n'existe aucun cours à noter dans le système");
                return;
            }
            
            FileUtils.Log("Affichage de l'écran de choix d'un élève pour l'ajout d'une note");
            new SaisirNoteListeElevesScreen(cours, eleves).Display();
        }

        private void SupprimerUnEleve()
        {
            if (eleves.Count == 0)
            {
                FileUtils.Log("Erreur lors de la suppression d'un élève alors qu'il n'en existe aucun");
                screenDatas.Add("Il n'existe aucun élève à supprimer dans le système");
                return;
            }

            FileUtils.Log("Affichage de l'écran de suppression d'un élève");
            new SuppressionEleveScreen(eleves).Display();
            FileUtils.SauvegarderJson(cours, eleves);
        }

        override protected void ChangeScreen()
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
                    ListerLesEleves();
                    break;
                case 1:
                    AjouterUnEleve();
                    break;
                case 2:
                    AfficherDetailsEleve();
                    break;
                case 3:
                    SaisirNoteEleve();
                    break;
                case 4:
                    SupprimerUnEleve();
                    break;
            }
        }
    }
}
