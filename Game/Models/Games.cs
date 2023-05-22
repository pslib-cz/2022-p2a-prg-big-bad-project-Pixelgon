using System.ComponentModel.Design;
using System.Diagnostics.Tracing;

namespace Game.Models;

public class Games
{
    public static String[] _wordList { get; set; }


    Games(string wordlist = "/home/pixel/RiderProjects/2022-p2a-prg-big-bad-project-Pixelgon/Game/Data/wordlist.txt")
    {
        _wordList = Load(wordlist);
    }

    public bool Check(string word)
    {
        return true;
    }

    private static String[] LoadWord(string word, String[] wordlist)
    {
        word.Trim();
        if (!(wordlist.Contains(word)))
        {
            wordlist.Append(word);
            return wordlist;
        }
        else
        {
            return wordlist;
        }
    }
    
    private static string[] Load(string wordlist)
    {
        String[] words = new String[File.ReadAllLines(wordlist).Length]; 
        using (StreamReader sr = new StreamReader(wordlist) )
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                LoadWord(line, words);
            }
        }
        return words;
    }

    public static string GetRandomWord()
    {
        return _wordList[Random.Shared.Next(0, _wordList.Length)];
    }
}