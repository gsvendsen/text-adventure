using System;
namespace console_game
{
    public class SafeSpace : LevelSpace
    {

        public SafeSpace(string prompt) : base(prompt)
        {
        }

        public void AddFriendly(Entity entity) {
            this.Friendlies.Add(entity);
        }

        override public bool HasEnemies()
        {
            return false;
        }

        public override bool HasFriendlies()
        {
            return this.Friendlies.Count > 0;
        }
    }
}
