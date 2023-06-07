using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Models
{
    internal class Games
    {
        private string[] _wordList {get; set;}
        private static Random _random = new Random();
        private string _word { get; set; }
        private int _round { get; set; } = 0;

        public Games(string wordlist = "Data/wordlist.txt")
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.Combine(currentDirectory, wordlist);
            _wordList = Load(absolutePath);
            _word = GetRandomWord();
        }

        public bool Check(string word)
        {
            return Array.Exists(_wordList, w => w.Trim().Equals(word.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        private static string[] LoadWord(string word, string[] wordlist)
        {
            if (!Array.Exists(wordlist, w => w.Trim().Equals(word.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                List<string> tempList = new List<string>(wordlist);
                tempList.Add(word);
                return tempList.ToArray();
            }
            else
            {
                return wordlist;
            }
        }

        private static string[] Load(string wordlist)
        {
            List<string> wordList = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(wordlist))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        wordList.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return wordList.ToArray();
        }

        public string GetRandomWord()
        {
            return _wordList[_random.Next(0, _wordList.Length-1)];
        }

        public void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Vítej v hře Slovní fotbal!");
            Console.ResetColor();
            Console.WriteLine("1. Hrát");
            Console.WriteLine("2. Statistiky");
            Console.WriteLine("3. Změnit slovník");
            Console.WriteLine("4. Ukončit");
            Console.Write("Vyber možnost: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Začíná hra!");
                    Round();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Statistiky");
                    break;
                case "3":
                    Console.Clear();
                    _wordList = Load(Console.ReadLine());
                    return;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Ukončuji hru...");
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Neplatná možnost!");
                    Console.ResetColor();
                    Menu();
                    break;
            }
        }
        
        public void Round()
        {
            
        }
    }
}