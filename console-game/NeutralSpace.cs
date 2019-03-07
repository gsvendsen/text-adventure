using System;
namespace console_game
{
    public class NeutralSpace : LevelSpace
    {
    
    
        public NeutralSpace(string prompt) : base(prompt)
        {
        }

        override public bool HasEnemies()
        {
            return false;
        }

        public override bool HasFriendlies()
        {
            return false;
        }
    }
}
