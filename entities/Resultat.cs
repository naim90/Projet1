using System.Collections.Generic;
using System.Linq;
using NotEdu.utils;

namespace NotEdu.entities
{
    public class Resultat
    {
        public int CoursId { get; set; }
        public double Note { get; set; }
        public string Appreciation { get; set; }

        public Resultat(int coursId, double note, string appreciation)
        {
            this.CoursId = coursId;
            this.Note = note;
            this.Appreciation = appreciation;
        }

        public IList<string> DisplayFormated(IList<Cours> cours)
        {
            IList<string> lines = new List<string>();

            lines.Add("Cours        : " + cours.First(c => c.Id == CoursId).Nom);
            lines.Add("Note         : " + MathUtils.FormatNote(Note) + "/20");
            lines.Add("Appréciation : " + Appreciation);

            return lines;
        }
    }
}
