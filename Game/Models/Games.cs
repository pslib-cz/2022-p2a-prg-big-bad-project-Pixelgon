using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Game.Models
{
    public class Games
    {
        internal string[] _wordList {get; set;}
        internal static Random _random = new Random();
        internal string _currentWord { get; set; }
        internal int _round { get; set; } = 0;
        internal int _tries { get; set; } = 0;

        public Games(string wordlist = "Data/wordlist.txt")
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.Combine(currentDirectory, wordlist);
            _wordList = FileHandlerer.LoadWordlist(absolutePath);
        }

        public bool Check(string word, Players player)
        {
            if (_currentWord.EndsWith(word, StringComparison.OrdinalIgnoreCase) && _wordList.Contains(word))
            {
                _currentWord = word;
                _round++;

                if (_tries == 0)
                {
                    player.Score += 100;
                }
                else
                {
                    player.Score /= (_tries * 2);
                }

                _tries = 0;
                this.RemoveWordFromWordList();
                return true;
            }

            player.Hp--;
            _tries++;
            return false;
        }

        public string GetWord()
        {
            if (_wordList.Length == 0)
            {
                return null;
            }
            var vysledek = _wordList.FirstOrDefault(s => s.StartsWith(_currentWord[_currentWord.Length - 1].ToString(), StringComparison.OrdinalIgnoreCase));
            if (vysledek == null)
            {
                vysledek = _wordList.FirstOrDefault(s => RemoveDiacritics(s[0]) == RemoveDiacritics(_currentWord[_currentWord.Length - 1]));
            }
            _currentWord = vysledek;
            this.RemoveWordFromWordList();
            return _currentWord;
        }

        public string GetRandomWord()
        {
            if(_wordList.Length == 0)
            {
                return null;
            }
            _currentWord = _wordList[_random.Next(0, _wordList.Length-1)];
            return _currentWord;
        }


        public bool ChangeWordlist(string adr)
        {
            Console.Clear();
            string [] newWordlist = FileHandlerer.LoadWordlist(adr);
            if(newWordlist.Length > 0)
            {
                _wordList = newWordlist;
                return true;
            }
            return false;
        }

        public void RemoveWordFromWordList()
        {
            _wordList = _wordList.Where(word => word != _currentWord).ToArray();
        }

        public char RemoveDiacritics(char c)
        {
            string s = c.ToString().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (var item in s)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(item) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(item);
                }
            }

            return sb.ToString()[0];
        }
    }
}