using System;
namespace console_game
{
    public abstract class Entity
    {

        public string Name { get; }
        public int Health { get; set; }
        public int Strength { get; set; }

        public bool isAlive = true;

        public Entity(string Name, int Health, int Strength)
        {
            this.Name = Name;
            this.Health = Health;
            this.Strength = Strength;
        }

        public void TakeDamage(int Damage)
        {
            this.Health = this.Health - Damage;
            if (this.Health <= 0)
            {
                this.isAlive = false;
            }
        }

        public void GainHealth(int Heal)
        {
            this.Health = this.Health + Heal;
        }

        public int Attack(Entity Target)
        {
            Random rnd = new Random();
            int damageValue = rnd.Next(1, this.Strength); // creates a number between 1 and strength

            Target.TakeDamage(damageValue);

            return damageValue;
        }

        public int Heal(Entity Target)
        {
            Random rnd = new Random();
            int healValue = rnd.Next(1, this.Strength); // creates a number between 1 and strength

            Target.GainHealth(healValue);

            return healValue;
        }
    }
}
