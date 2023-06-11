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
                    player.AddScore(100);
                }
                else
                {
                    player.AddScore((_tries * 2));
                }
                if (player._score % 1000 == 0 && player._hp < 4)
                {
                    player.AddHp();
                }
                _tries = 0;
                RemoveWordFromWordList(_currentWord);
                return true;
            }
            player.ReduceHp();
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
            RemoveWordFromWordList(vysledek);
            return vysledek;
        }

        public string GetRandomWord()
        {
            if(_wordList.Length == 0)
            {
                return null;
            }
            return _wordList[_random.Next(0, _wordList.Length-1)];;
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

        public void RemoveWordFromWordList(string wrd)
        {
            _wordList = _wordList.Where(word => word != wrd).ToArray();
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

        public bool Round(Players player)
        {
            if(_wordList.Length == 0)
            {
                return false;
            }
            if(player._hp == 0)
            {
                return false;
            }
            if(_round == 0)
            {
                _currentWord = GetRandomWord();
            }
            else
            {
                _currentWord = GetWord();
            }
            _round++;
            return true;
        }
    }
}