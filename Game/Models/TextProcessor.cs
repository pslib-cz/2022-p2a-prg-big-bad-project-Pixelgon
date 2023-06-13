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
        internal static string _wordPattern = @"[^a-zA-Z0-9ěščřžýáíé-]";

        public static string PrepareWord(string text)
        {
            return Regex.Replace(text, _wordPattern, "");
        }

    }
}