using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    internal class FileHandlerer
    {
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return wordList.ToArray();
        }

        internal static void SaveStatistic(Games game, Players player)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = Path.Combine(currentDirectory, "Data/statistic.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(absolutePath, true))
                {
                    sw.WriteLine($"{player._name} | {player._score} | {game._round}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }   
    }
}
