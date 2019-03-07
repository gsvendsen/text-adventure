using System;
namespace console_game
{
    public class Player : Entity
    {

        public int xPosition = 0;

        public int yPosition = 0;

        public Player(string Name, int Health, int Strength): base(Name,Health, Strength)
        {
        }
    }
}
