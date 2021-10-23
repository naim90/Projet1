using System;
using System.Collections.Generic;
using System.Linq;
using NotEdu.entities;
using NotEdu.utils;

namespace NotEdu.screens.eleves
{
    class AjoutEleveScreen
    {
        ICollection<Eleve> eleves;
        public AjoutEleveScreen(ICollection<Eleve> eleves)
        {
            this.eleves = eleves;
        }

        private string Ask(string question)
        {
            Console.Write(" " + question);
            return Console.ReadLine();
        } 

        private void AjouterEleve(string nom, string prenom, DateTime dateNaissance, string anneeDePromotion)
        {
            int id = eleves.Count == 0 ? 1 : eleves.Max(e => e.Id) + 1;
            string nomPascalCase = StringUtils.PascalCaseString(nom);
            string prenomPascalCase = StringUtils.PascalCaseString(prenom);
            Eleve eleve = new Eleve(id, nomPascalCase, prenom, dateNaissance, null, anneeDePromotion);

            FileUtils.Log("Ajout de l'élève N°" + id + " " + nomPascalCase + " " + prenomPascalCase);

            eleves.Add(eleve);
        }

        private bool VerifierDonnees(string nom, string prenom, string dateNaissance, bool isDateValide, string anneeDePromotion)
        {
            bool erreur = false;
            if (nom == "")
            {
                Console.WriteLine(" Attention, le nom ne doit pas être vide !");
                erreur = true;
            }
            if (prenom == "")
            {
                Console.WriteLine(" Attention, le prénom ne doit pas être vide !");
                erreur = true;
            }
            if (dateNaissance == "")
            {
                Console.WriteLine(" Attention, la date de naissance ne doit pas être vide !");
                erreur = true;
            }
            if (!isDateValide && dateNaissance != "")
            {
                Console.WriteLine(" Attention, le format de la date de naissace n'est pas valide !");
                erreur = true;
            }
            if(anneeDePromotion=="")
            {
                Console.WriteLine(" Attention, le format de l'année de promomotion n'est pas valide !");
                erreur = true;
            }
            return erreur;
        }

        public void Display()
        {
            Console.Clear();

            ConsoleUtils.DisplayNotEdu();

            Console.WriteLine();
            Console.WriteLine(" AJOUT D'UN ELEVE");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Pour annuler l'opération, laissez tous les champs vides");
            Console.WriteLine();

            bool exitScreen = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine();

                string nom =           Ask("Nom de l'élève                                      : ");
                string prenom =        Ask("Prénom de l'élève                                   : ");
                string dateNaissance = Ask("Date de naissance de l'élève (au format YYYY/MM/DD) : ");
                DateTime dn;
                string anneeDePromotion =      Ask("Annee de Promotion de l'élève               : ");
                bool isDateValide = DateTime.TryParse(dateNaissance, out dn);

                if (nom == "" && prenom == "" && dateNaissance == "" && anneeDePromotion == "")
                {
                    exitScreen = true;
                } 
                else
                {
                    Console.WriteLine();
                    bool erreur = VerifierDonnees(nom, prenom, dateNaissance, isDateValide, anneeDePromotion);

                    if (erreur)
                    {
                        Console.WriteLine(" Veuillez recommencer votre saisie.");
                    }
                    else
                    {
                        AjouterEleve(nom, prenom, dn, anneeDePromotion);
                        exitScreen = true;
                    }
                }
            } while (exitScreen == false);
        }
    }
}
