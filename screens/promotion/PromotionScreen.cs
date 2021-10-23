using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.screens.generique;
using NotEdu.utils;

namespace NotEdu.screens.promotion
{
    class PromotionScreen : NavigableScreen
    {
        ICollection<Eleve> eleves;
        ICollection<Cours> cours;

        public PromotionScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            this.screenTitle = "PROMOTION";
            this.actions = new List<string>
            {
                "Afficher promotion existante",
                "Afficher les élèves d'une promotion",
                "Afficher la moyenne de tous les élèves d'une promotion",
                "Revenir au menu principal"
            };
        }

        private void ListerLesPromotions()
        {

            screenTitle = "PROMOTION";

            if (cours.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des promotions");
                foreach (Eleve c in eleves)
                {
                    screenDatas.Add(c.DisplayBasicInformations()[3]);
                }
               // screenDatas.RemoveAt(screenDatas.Count - 1);
            }
            else
            {
                FileUtils.Log("Affichage de la liste des promotions vide");
                screenDatas.Add("Il n'existe encore aucune promotion dans le système");
            }
        }

        public void ListerLesElevesDunePromotion()
        {

            Console.WriteLine("Entrer la promotion souhaité :");
            string promotionEntree = Console.ReadLine();

            screenTitle = screenTitle+" "+promotionEntree;

            var filteredProjects = eleves.Where(p => p.AnneeDePromotion==promotionEntree);

            if (eleves.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des élèves de la promotion entrée");
                foreach (Eleve e in filteredProjects)
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
                FileUtils.Log($"Affichage de la liste des élèves de la promotion {promotionEntree} est vide");
                screenDatas.Add($"Il n'existe encore aucun élève dans le système de la promotion {promotionEntree}");
            }

        }

        private void AfficherMoyenneGenPromotion()
        {
            Console.WriteLine("Entrer la promotion souhaité :");
            string promotionEntree = Console.ReadLine();

            screenTitle = screenTitle + " " + promotionEntree;

            var filteredProjects = eleves.Where(p => p.AnneeDePromotion == promotionEntree);
            List<string> listMoyenneEleves = new List<string>();
            int nombreLigneDansListe = 0;
            double moyenneGeneralePromotion = 0;

            if (eleves.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des élèves de la promotion entrée");
                foreach (Eleve e in filteredProjects)
                {
                    nombreLigneDansListe = 0;

                    foreach (string line2 in e.DisplayFormated(cours.ToList()))
                    {
                        nombreLigneDansListe = nombreLigneDansListe + 1;

                        if (nombreLigneDansListe == e.DisplayFormated(cours.ToList()).Count)
                        {
                            listMoyenneEleves.Add(line2);
                        }
                    }

                    screenDatas.Add("");
                }

                screenDatas.RemoveAt(screenDatas.Count - 1);

                List<double> listMoyenneElevesInt = listMoyenneEleves.Select(s => double.Parse(s)).ToList();
                moyenneGeneralePromotion = listMoyenneElevesInt.Average();

                screenDatas.Add($"La moyenne générale de tous les élèves de la promotion {promotionEntree} est : {moyenneGeneralePromotion}");
            }

            else
            {
                FileUtils.Log($"Affichage de moyenne des élèves de la promotion {promotionEntree} est vide");
                screenDatas.Add($"Il n'existe encore aucun élève dans le système de la promotion {promotionEntree}");
            }
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
                    ListerLesPromotions();
                    break;
                case 1:
                    ListerLesElevesDunePromotion();
                    break;
                case 2:
                    AfficherMoyenneGenPromotion();
                    break;
            }
        }
    }
}
