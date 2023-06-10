using System;
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@" ___       __   ________  ________  ________          ________  ___  ___  ________  ___  ________      
|\  \     |\  \|\   __  \|\   __  \|\   ___ \        |\   ____\|\  \|\  \|\   __  \|\  \|\   ___  \    
\ \  \    \ \  \ \  \|\  \ \  \|\  \ \  \_|\ \       \ \  \___|\ \  \\\  \ \  \|\  \ \  \ \  \\ \  \   
 \ \  \  __\ \  \ \  \\\  \ \   _  _\ \  \ \\ \       \ \  \    \ \   __  \ \   __  \ \  \ \  \\ \  \  
  \ \  \|\__\_\  \ \  \\\  \ \  \\  \\ \  \_\\ \       \ \  \____\ \  \ \  \ \  \ \  \ \  \ \  \\ \  \ 
   \ \____________\ \_______\ \__\\ _\\ \_______\       \ \_______\ \__\ \__\ \__\ \__\ \__\ \__\\ \__\
    \|____________|\|_______|\|__|\|__|\|_______|        \|_______|\|__|\|__|\|__|\|__|\|__|\|__| \|__|");
                Console.WriteLine("Vytvořil Pixelgon, pixelgon.cz");
                Console.ResetColor();
                Console.WriteLine($"{(option == 1 ? "\u001b[33m" : "\u001b[0m")}1. Hrát\u001b[0m");
                Console.WriteLine($"{(option == 2 ? "\u001b[33m" : "\u001b[0m")}2. Statistiky\u001b[0m");
                Console.WriteLine($"{(option == 3 ? "\u001b[33m" : "\u001b[0m")}3. Změnit slovník\u001b[0m");
                Console.WriteLine($"{(option == 4 ? "\u001b[33m" : "\u001b[0m")}4. Ukončit\u001b[0m");
                Console.WriteLine($"Vybral sis možnost číslo \u001b[33m{option}.\u001b[0m");
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

        public static void Round(Games game)
        {
            Console.WriteLine(game.GetRandomWord());
            Console.WriteLine(game.GetWord());
        }

        public static void Stats()
        {

        }

        public static void ChangeWordlist(Games game)
        {
            Console.WriteLine("Zadejte cestu k souboru se slovníkem.");
            string filePath = Console.ReadLine();
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

        public static void GameOver()
        {

        }

        public static void Win()
        {
            Console.WriteLine("");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
