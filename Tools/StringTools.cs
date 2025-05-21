using System.Text.RegularExpressions;

namespace OdontoPrevAPI.Tools
{
    public class StringTools
    {
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^0-9a-zA-Z]+", "");
        }
        public static string OnlyNumbers(string str)
        {
            return Regex.Replace(str, "[^0-9]", "");
        }
    }
}
