using Game.Models;

namespace Game;

class Program
{
    // main method
    static void Main(string[] args)
    {
        // create a new instance of the game
        Games game = new Games();
        // start the game
        game.Menu();
    }
}

