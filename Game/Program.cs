using Game.Models;

namespace Game;

class Program
{
    // main method
    static void Main(string[] args)
    {
        Games game = new Games();
        Players P = new Players();
        int option;
        do
        {
            option = Render.Menu();
            switch (option)
            {
                case 1:
                    Render.Play(game, P);
                    break;
                case 2:
                    Render.Stats();
                    break;
                case 3:
                    Render.ChangeWordlist(game);
                    break;
                case 4:
                    Render.Exit();
                    break;
            }
        } while (option != 4);
    }
}

