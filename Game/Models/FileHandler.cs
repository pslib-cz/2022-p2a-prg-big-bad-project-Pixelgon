using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    internal class FileHandler
    {
        private static string _dataRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static string _statsPath = Path.Combine(_dataRoot, "statistic.csv");
        private static string _wordlistPath = Path.Combine(_dataRoot, "wordlistPath.txt");

        internal static string[] LoadWordlist(string wordlist)
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
            catch (FileNotFoundException)
            {
                Console.WriteLine("Soubor slovníku nebyl nalezen na adrese: " + wordlist);
            }
            catch (IOException)
            {
                Console.WriteLine("Chyba při čtení souboru slovníku: " + wordlist);
            }
            catch (Exception e)
            {
                Console.WriteLine("Nastala neznámá chyba: " + e.Message);
            }

            return wordList.ToArray();
        }

        internal static void SaveStatistic(Games game, Players player)
        {
            if (!Directory.Exists(_dataRoot))
            {
                Directory.CreateDirectory(_dataRoot);
            }

            if (!File.Exists(_statsPath))
            {
                File.Create(_statsPath).Close();
                using (StreamWriter sw = new StreamWriter(_statsPath, true))
                {
                    sw.WriteLine("Hráč;Počet kol;Score");
                }
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(_statsPath, true, Encoding.UTF8))
                {
                    string csvLine = $"{player._name};{player._score};{game._round}";
                    sw.WriteLine(csvLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        internal static List<string[]> LoadStatistic()
        {
            List<string[]> statistic = new List<string[]>();

            try
            {
                using (StreamReader sr = new StreamReader(_statsPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        statistic.Add(line.Split(';'));
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(_statsPath).Close();
            }
            catch (IOException)
            {
                Console.WriteLine("Chyba při čtení souboru statistiky: " + _statsPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Nastala neznámá chyba: " + e.Message);
            }

            return statistic;
        }

        internal static void SaveWordlistPath(string path)
        {
            if (!Directory.Exists(_dataRoot))
            {
                Directory.CreateDirectory(_dataRoot);
            }

            if (!File.Exists(_wordlistPath))
            {
                File.Create(_wordlistPath).Close();
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(_wordlistPath, false, Encoding.UTF8))
                {
                    sw.WriteLine(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }   

        internal static string ReadWordlistPath()
        {
            if (!Directory.Exists(_dataRoot))
            {
                Directory.CreateDirectory(_dataRoot);
            }

            if (!File.Exists(_wordlistPath))
            {
                File.Create(_wordlistPath).Close();
            }
            try
            {
                using (StreamReader sr = new StreamReader(_wordlistPath))
                {
                    return sr.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Soubor s cestou k slovníku nexistuje: " + _wordlistPath);
            }
            catch (IOException)
            {
                Console.WriteLine("Chyba při čtení souboru cesty k slovníku: " + _wordlistPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Nastala neznámá chyba: " + e.Message);
            }

            return null;
        }
    }
}
