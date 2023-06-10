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
    }
}
