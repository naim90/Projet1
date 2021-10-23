using System.Collections.Generic;
using NotEdu.entities;
using NotEdu.screens.cours;
using NotEdu.screens.eleves;
using NotEdu.screens.generique;
using NotEdu.screens.promotion;
using NotEdu.utils;

namespace NotEdu.screens
{
    class MainScreen : NavigableScreen
    {
        private readonly ICollection<Eleve> eleves;
        private readonly ICollection<Cours> cours;
        
        public MainScreen(ICollection<Eleve> eleves, ICollection<Cours> cours)
        {
            this.eleves = eleves;
            this.cours = cours;

            screenTitle = "MENU PRINCIPAL";
            actions = new List<string> {
                "Gestion des élèves",
                "Gestion des cours",
                "Afficher les informations des promotions",
                "Quitter le programme"
            };
        }

        override protected void ChangeScreen()
        {
            switch (choix)
            {
                case 0:
                    FileUtils.Log("Accès au menu des élèves");
                    new EleveScreen(eleves, cours).Display();
                    break;
                case 1:
                    FileUtils.Log("Accès au menu des cours");
                    new CoursScreen(eleves, cours).Display();
                    break;
                case 2:
                    FileUtils.Log("Accès à la liste des promos disponible");
                    new PromotionScreen(eleves, cours).Display();
                    break;
                case 3:
                    FileUtils.Log("Fermeture du programme");
                    exitScreen = true;
                    break;
            }
        }
    }
}
