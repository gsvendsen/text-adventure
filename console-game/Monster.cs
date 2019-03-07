using System;
namespace console_game
{
    public class Monster : Entity
    {

        public int Score {
            get { return 20; }
        }

        public Monster(string Name, int Health, int Strength) : base(Name,Health,Strength)
        {
        }
    }
}
