namespace NotEdu.utils
{
    public static class StringUtils
    {
        public static string PascalCaseString(string s)
        {
            string lower = s.ToLower();
            return lower[0].ToString().ToUpper() + lower.Substring(1);
        }
    }
}
