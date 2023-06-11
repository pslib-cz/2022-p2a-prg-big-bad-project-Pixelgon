using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game.Models
{
    public static class TextProcessor
    {
        public static string RemoveSpecialCharacters(string text)
        {
            string pattern = @"[^a-zA-Z0-9]";
            string cleanText = Regex.Replace(text, pattern, "");
            Console.WriteLine(cleanText);
            return cleanText;
        }
    }
}