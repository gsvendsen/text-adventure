using System;
using System.Collections.Generic;
namespace console_game
{
    abstract public class LevelSpace
    {
        public string prompt;

        public List<Monster> Enemies = new List<Monster>();

        public List<Entity> Friendlies = new List<Entity>();


        public bool isActive = true;

        public LevelSpace(String prompt)
        {
            this.prompt = prompt;
        }

        abstract public bool HasEnemies();

        abstract public bool HasFriendlies();

    }
}
