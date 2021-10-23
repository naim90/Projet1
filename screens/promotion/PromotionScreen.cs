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
                "Revenir au menu principal"
            };
        }

        private void ListerLesPromotions()
        {

            if (cours.Count > 0)
            {
                FileUtils.Log("Affichage de la liste des cours");
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

        private void ListerLesElevesDunePromotion()
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
                FileUtils.Log("Affichage de la liste des élèves vide");
                screenDatas.Add("Il n'existe encore aucun élève dans le système");
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
            }
        }
    }
}
