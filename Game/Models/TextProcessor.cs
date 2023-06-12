using System;
using System.Collections.Generic;
using System.Globalization;
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
            string pattern = @"[^a-zA-Z0-9*ě*š*č*ř*ž*ý*á*í*é*-]";
            string cleanText = Regex.Replace(text, pattern, "");
            return cleanText;
        }
    }
}