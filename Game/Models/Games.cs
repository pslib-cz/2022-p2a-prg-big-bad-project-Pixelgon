using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Game.Models
{
    public class Games
    {
        internal string _wordlistPath { get; set; }
        internal string[] _wordList { get; set; }
        internal static Random _random = new Random();
        internal string _currentWord { get; set; }
        internal int _round { get; set; } = 0;
        internal int _tries { get; set; } = 0;

        public Games()
        {
            _wordlistPath = FileHandler.ReadWordlistPath();
            if (_wordlistPath == null)
            {
                _wordlistPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/wordlist.txt");
            }
            _wordList = FileHandler.LoadWordlist(_wordlistPath);
        }

        public bool Check(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }

            if (_currentWord.EndsWith(word[0].ToString(), StringComparison.OrdinalIgnoreCase) && Array.Exists(_wordList, w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        public string GetWord(string word)
        {
            if (_wordList.Length == 0)
            {
                return null;
            }
            return _wordList.Where(s => s.StartsWith(word?[word.Length - 1].ToString(), StringComparison.OrdinalIgnoreCase)).OrderBy(x => _random.Next()).FirstOrDefault();
        }

        public string GetRandomWord()
        {
            if (_wordList.Length == 0)
            {
                return null;
            }
            return _wordList[_random.Next(0, _wordList.Length - 1)];
        }

        public bool ChangeWordlist(string adr)
        {
            Console.Clear();
            string[] newWordlist = FileHandler.LoadWordlist(adr);
            if (newWordlist.Length > 0)
            {
                _wordList = newWordlist;
                FileHandler.SaveWordlistPath(adr);
                return true;
            }
            return false;
        }

        public void RemoveWordFromWordList(string wrd)
        {
            _wordList = Array.FindAll(_wordList, w => !string.Equals(w, wrd, StringComparison.OrdinalIgnoreCase));
        }

        public bool Round(Players player)
        {
            _round++;
            if (player._score % 1500 == 0 && player._hp > 0 && player._hp < 3)
            {
                player.AddHp();
            }
            if (_wordList.Length == 0)
            {
                return false;
            }
            if (player._hp == 0)
            {
                return false;
            }
            if (_round == 1)
            {
                _currentWord = GetRandomWord();
            }
            else
            {
                _currentWord = GetWord(_currentWord);
            }
            if (_currentWord == null)
            {
                return false;
            }
            RemoveWordFromWordList(_currentWord);
            return true;
        }

        public bool RoundChecking(string word, Players player)
        {
            word = TextProcessor.PrepareWord(word);
            if (Check(word))
            {
                _currentWord = word;
                RemoveWordFromWordList(_currentWord);
                player.AddScore(300 / (_tries == 0 ? 1 : _tries + 1));
                _tries = 0;
                return true;
            }
            else
            {
                _tries++;
                player.ReduceHp();
                return false;
            }
        }

        public void Reset(Players player)
        {
            _round = 0;
            _tries = 0;
            _wordList = FileHandler.LoadWordlist(_wordlistPath);
            player.Reset();
        }

        public void GenerateStat(string name, Games game, Players player)
        {
            player.SetName(TextProcessor.PrepareWord(name));
            FileHandler.SaveStatistic(game, player);
        }

        public static List<string[]> GetStatistic()
        {
            List<string[]> stats = FileHandler.LoadStatistic();
            stats.Sort((row1, row2) =>
            {
                int score1 = int.Parse(row1[2]);
                int score2 = int.Parse(row2[2]);
                return score2.CompareTo(score1);
            });

            return stats;
        }
    }
}
