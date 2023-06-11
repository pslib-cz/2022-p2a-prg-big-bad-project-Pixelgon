namespace Game.Models;

public class Players
{
    internal int _hp { get; set; }
    internal int _score { get; set; }
    internal string _name { get; set; }

    public Players()
    {
        _score = 0;
        _hp = 3;
    }
    
    public void Reset()
    {
        _score = 0;
        _hp = 3;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void ReduceHp()
    {
        _hp--;
    }
    
    public void AddHp()
    {
        _hp++;
    }   
}