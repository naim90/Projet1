using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.utils;

namespace NotEdu.screens.generique
{
    public abstract class NavigableScreen
    {
        protected int choix = 0;
        protected IList<string> actions;
        protected string screenTitle;
        protected bool exitScreen;
        protected IList<string> screenDatas;

        public NavigableScreen()
        {
            exitScreen = false;
            screenDatas = new List<string>();
        }

        protected abstract void ChangeScreen();
        protected void DisplayScreenData()
        {
            HashSet<string> supprimerDoublons = new HashSet<string>(screenDatas);//***********************************
            List<string> listSansDoublons = supprimerDoublons.ToList();//*********************************************

            if (screenDatas.Count > 0 && screenTitle != "PROMOTION") 
            {
                Console.WriteLine(" ---------------------------------------------------");
                foreach (string line in screenDatas)
                {
                    Console.WriteLine(" " + line);
                }
                Console.WriteLine(" ---------------------------------------------------");
            }   
            else if (screenDatas.Count > 0 && screenTitle=="PROMOTION")//*********************************************
            {
                Console.WriteLine(" ---------------------------------------------------");
                foreach (string line in listSansDoublons)
                {
                    Console.WriteLine(" " + line);
                }
                Console.WriteLine(" ---------------------------------------------------");
            }//*******************************************************************************************************
        }

        public virtual void Display()
        {
            Console.Clear();
            Console.WriteLine();

            ConsoleUtils.DisplayNotEdu();

            Console.WriteLine();
            Console.WriteLine(" " + screenTitle);
            Console.WriteLine();


            DisplayScreenData();
            Console.WriteLine();

            Console.WriteLine(" Choisissez une action : ");
            Console.WriteLine();

            for (int i = 0; i < actions.Count; i++)
            {
                string action = actions[i];

                Console.WriteLine(string.Format(" [{0}] {1}", (choix == i ? "X" : " "), action));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Navigation avec ↑ et ↓ puis Enter pour valider");

            Console.WriteLine();
            Console.WriteLine();

            ConsoleKeyInfo cki = Console.ReadKey();

            if (cki.Key == ConsoleKey.DownArrow && choix < actions.Count - 1) choix++;
            else if (cki.Key == ConsoleKey.UpArrow && choix > 0) choix--;
            else if (cki.Key == ConsoleKey.Enter) {
                screenDatas.Clear();
                ChangeScreen();
            };
            if (!exitScreen) Display();
        }
    }
}
