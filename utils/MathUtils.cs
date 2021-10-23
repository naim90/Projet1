using System;

namespace NotEdu.utils
{
    public static class MathUtils
    {
        public static double ArrondirNote(double note)
        {
            double noteArrondie = Math.Round(note*100);
            return Math.Round((noteArrondie/100),3);
        }

        public static string FormatNote(double note)
        {
            return "" + ArrondirNote(note);
        }
    }
}
