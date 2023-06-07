namespace Game.Models;

internal class Players
{
    public int Hp { get; set; }
    public int Score { get; set; }
    public string Name { get; set; }

    Players(string _name)
    {
        Score = 0;
        Hp = 3;
        Name = _name;
    }
    
    
    public int GetNumberOfPlayers()
    {
        return 5;
    }
}