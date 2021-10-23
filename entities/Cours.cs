using System.Collections.Generic;

namespace NotEdu.entities
{
    public class Cours
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public Cours(int id, string nom)
        {
            Id = id;
            Nom = nom;
        }

        public IList<string> DisplayFormated()
        {
            List<string> resultat = new List<string>();

            resultat.Add(Nom);

            return resultat;
        }

    }
}
