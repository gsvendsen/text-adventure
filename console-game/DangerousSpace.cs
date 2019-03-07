using System;
using System.Collections.Generic;

namespace console_game
{
    public class DangerousSpace : LevelSpace
    {

        public DangerousSpace(string prompt) : base(prompt)
        {
        }

        public void AddEnemy(Monster enemy)
        {
            this.Enemies.Add(enemy);
        }

        override public bool HasEnemies()
        {
            return this.Enemies.Count > 0;
        }

        public override bool HasFriendlies()
        {
            return false;
        }


    }
}
