namespace Game.Models;

public class Players
{
    internal int Hp { get; set; }
    internal int Score { get; set; }
    internal string Name { get; set; }

    Players(string _name)
    {
        Score = 0;
        Hp = 3;
        Name = _name;
    } 
    
    
}