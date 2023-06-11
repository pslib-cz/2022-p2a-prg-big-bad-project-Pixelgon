using System;

namespace Game.Models
{
    public interface IReadable
    {
        string RemoveSpecialCharacters(string text);
    }
}