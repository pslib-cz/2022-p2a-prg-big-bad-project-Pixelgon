﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public static class Render
    {
        public static int Menu()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            int option = 1;
            bool isSelected = false;
            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@" ___       __   ________  ________  ________          ________  ___  ___  ________  ___  ________      
|\  \     |\  \|\   __  \|\   __  \|\   ___ \        |\   ____\|\  \|\  \|\   __  \|\  \|\   ___  \    
\ \  \    \ \  \ \  \|\  \ \  \|\  \ \  \_|\ \       \ \  \___|\ \  \\\  \ \  \|\  \ \  \ \  \\ \  \   
 \ \  \  __\ \  \ \  \\\  \ \   _  _\ \  \ \\ \       \ \  \    \ \   __  \ \   __  \ \  \ \  \\ \  \  
  \ \  \|\__\_\  \ \  \\\  \ \  \\  \\ \  \_\\ \       \ \  \____\ \  \ \  \ \  \ \  \ \  \ \  \\ \  \ 
   \ \____________\ \_______\ \__\\ _\\ \_______\       \ \_______\ \__\ \__\ \__\ \__\ \__\ \__\\ \__\
    \|____________|\|_______|\|__|\|__|\|_______|        \|_______|\|__|\|__|\|__|\|__|\|__|\|__| \|__|");
                Console.WriteLine("\nVytvořil Pixelgon, pixelgon.cz\n");
                Console.ResetColor();
                Console.WriteLine($"{(option == 1 ? "\x1b[93m" : "\u001b[0m")}1. Hrát\u001b[0m");
                Console.WriteLine($"{(option == 2 ? "\x1b[93m" : "\u001b[0m")}2. Statistika\u001b[0m");
                Console.WriteLine($"{(option == 3 ? "\x1b[93m" : "\u001b[0m")}3. Změnit slovník\u001b[0m");
                Console.WriteLine($"{(option == 4 ? "\x1b[91m" : "\u001b[0m")}4. Ukončit\u001b[0m");
                Console.WriteLine($"\nVybral sis možnost \u001b[93m{option}.\u001b[0m");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 4 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 4 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        Console.Clear();
                        break;
                }
                Console.Clear();
            }
            return option;
        }

        public static void Play(Games game, Players player)
        {
            while (game.Round(player))
            {
                Console.Clear();
                Console.WriteLine($"Kolo: \u001b[96m{game._round}\u001b[0m Score: \u001b[96m{player._score}\u001b[0m HP: \u001b[91m{player._hp}\u001b[0m");
                Console.WriteLine($"Aktuální slovo je \u001b[96m{game._currentWord}.\u001b[0m");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string word = Console.ReadLine();
                Console.ResetColor();
                while (!game.RoundChecking(word, player))
                {
                    if(player._hp == 0 || game._wordList.Length == 0)
                    {
                        Render.GameOver(game, player);
                        return;
                    }
                    Console.WriteLine($"\u001b[91mŠpatně! slovo {word} slovník neobsahuje nebo jsi ho už použil.\u001b[0m");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    word = Console.ReadLine();
                    Console.ResetColor();
                }
            }
        }


        public static void Stats()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ________  _________  ________  _________  ___  ________  _________  ___  ___  __    ________     \r\n|\\   ____\\|\\___   ___\\\\   __  \\|\\___   ___\\\\  \\|\\   ____\\|\\___   ___\\\\  \\|\\  \\|\\  \\ |\\   __  \\    \r\n\\ \\  \\___|\\|___ \\  \\_\\ \\  \\|\\  \\|___ \\  \\_\\ \\  \\ \\  \\___|\\|___ \\  \\_\\ \\  \\ \\  \\/  /|\\ \\  \\|\\  \\   \r\n \\ \\_____  \\   \\ \\  \\ \\ \\   __  \\   \\ \\  \\ \\ \\  \\ \\_____  \\   \\ \\  \\ \\ \\  \\ \\   ___  \\ \\   __  \\  \r\n  \\|____|\\  \\   \\ \\  \\ \\ \\  \\ \\  \\   \\ \\  \\ \\ \\  \\|____|\\  \\   \\ \\  \\ \\ \\  \\ \\  \\\\ \\  \\ \\  \\ \\  \\ \r\n    ____\\_\\  \\   \\ \\__\\ \\ \\__\\ \\__\\   \\ \\__\\ \\ \\__\\____\\_\\  \\   \\ \\__\\ \\ \\__\\ \\__\\\\ \\__\\ \\__\\ \\__\\\r\n   |\\_________\\   \\|__|  \\|__|\\|__|    \\|__|  \\|__|\\_________\\   \\|__|  \\|__|\\|__| \\|__|\\|__|\\|__|\r\n   \\|_________|                                   \\|_________|");
            Console.WriteLine("\nPořadí | Hráč | Score | Kolo");
            Console.ResetColor();
            int n = 0;
            foreach (string[] row in Games.GetStatistic())
            {
                n++;
                Console.Write(n + ". | ");
                foreach (string field in row)
                {
                    Console.Write(field + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nStiskněte libovolnou klávesu pro návrat do menu.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ChangeWordlist(Games game)
        {
            Console.WriteLine("Zadejte cestu k souboru se slovníkem.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string filePath = Console.ReadLine();
            Console.ResetColor();
            bool success = game.ChangeWordlist(filePath);
            Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(success ? "Slovník byl úspěšně změněn." : "Slovník se nepodařilo změnit.");
            Console.ResetColor();
        }


        public static void Exit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@" ________      ___    ___ _______   ___       
|\   __  \    |\  \  /  /|\  ___ \ |\  \      
\ \  \|\ /_   \ \  \/  / | \   __/|\ \  \     
 \ \   __  \   \ \    / / \ \  \_|/_\ \  \    
  \ \  \|\  \   \/  /  /   \ \  \_|\ \ \__\   
   \ \_______\__/  / /      \ \_______\|__|   
    \|_______|\___/ /        \|_______|   ___ 
             \|___|/                     |\__\
                                         \|__|");
            Console.ResetColor();
            Environment.Exit(0);
        }

        public static void GameOver(Games game, Players player)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (game._wordList.Length == 0 || game._currentWord == null)
            {
                Console.WriteLine(@"  ___    ___ ________  ___  ___          ___       __   ___  ________   ________      
 |\  \  /  /|\   __  \|\  \|\  \        |\  \     |\  \|\  \|\   ___  \|\_____  \     
 \ \  \/  / | \  \|\  \ \  \\\  \       \ \  \    \ \  \ \  \ \  \\ \  \|____|\  \    
  \ \    / / \ \  \\\  \ \  \\\  \       \ \  \  __\ \  \ \  \ \  \\ \  \    \ \__\   
   \/  /  /   \ \  \\\  \ \  \\\  \       \ \  \|\__\_\  \ \  \ \  \\ \  \    \|__|   
 __/  / /      \ \_______\ \_______\       \ \____________\ \__\ \__\\ \__\       ___ 
|\___/ /        \|_______|\|_______|        \|____________|\|__|\|__| \|__|      |\__\
\|___|/                                                                          \|__|");
                if(game._wordList.Length == 0)
                {
                    Console.WriteLine("Vyhrál jsi!");

                }
                else
                {
                    Console.WriteLine($"Měl jsi štěstí, jelikož nebylo nalezeno slovo, které by navazovalo, stále zbylo ");
                }
            }
            else
            {
                Console.WriteLine(@" ________  ________  _____ ______   _______           ________  ___      ___ _______   ________  ___       
|\   ____\|\   __  \|\   _ \  _   \|\  ___ \         |\   __  \|\  \    /  /|\  ___ \ |\   __  \|\  \      
\ \  \___|\ \  \|\  \ \  \\\__\ \  \ \   __/|        \ \  \|\  \ \  \  /  / | \   __/|\ \  \|\  \ \  \     
 \ \  \  __\ \   __  \ \  \\|__| \  \ \  \_|/__       \ \  \\\  \ \  \/  / / \ \  \_|/_\ \   _  _\ \  \    
  \ \  \|\  \ \  \ \  \ \  \    \ \  \ \  \_|\ \       \ \  \\\  \ \    / /   \ \  \_|\ \ \  \\  \\ \__\   
   \ \_______\ \__\ \__\ \__\    \ \__\ \_______\       \ \_______\ \__/ /     \ \_______\ \__\\ _\\|__|   
    \|_______|\|__|\|__|\|__|     \|__|\|_______|        \|_______|\|__|/       \|_______|\|__|\|__|   ___ 
                                                                                                      |\__\
                                                                                                      \|__|");
            }
            Console.ResetColor();
            Console.WriteLine($"Získal jsi {player._score} score.");
            Console.WriteLine($"Ve slovníku zbylo {game._wordList.Length} slov.");
            Console.WriteLine("Přeješ si uložit své score? (a/n)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            ConsoleKeyInfo key = Console.ReadKey();
            Console.ResetColor();
            if (key.Key == ConsoleKey.A)
            {
                Console.WriteLine("\nZadejte své jméno:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                game.GenerateStat(Console.ReadLine(), game, player);
                Console.ResetColor();
            }
            Console.Clear();
            game.Reset(player);
        }
    }
}
