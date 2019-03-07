using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace console_game
{
    public class GameField
    {

        public int Size;

        public List<List<LevelSpace>> LevelArea = new List<List<LevelSpace>>();

        public GameField(int Size)
        {
            this.Size = Size;
        }

        public void Generate()
        {

            string[] dangerousPrompts = File.ReadAllLines(Directory.GetCurrentDirectory() + "/LevelData/DangerousPrompts.txt");
            string[] monsterNames = File.ReadAllLines(Directory.GetCurrentDirectory() + "/LevelData/MonsterNames.txt");
            string[] neutralPrompts = File.ReadAllLines(Directory.GetCurrentDirectory() + "/LevelData/NeutralPrompts.txt");

            for (var i = 0; i <= this.Size; i++)
            {
                List<LevelSpace> level = new List<LevelSpace>();
                for (var x = 0; x <= this.Size; x++)
                {
                    Random rnd = new Random();
                    int levelValue = rnd.Next(1, 100); // creates a number between 1 and 100
                    if (levelValue <= 50 || i == 0)
                    {
                        Random rndPrompt = new Random();
                        int promptValue = rnd.Next(0, neutralPrompts.Length);

                        var levelSpace = new NeutralSpace($"You come across {neutralPrompts[promptValue]} (North/East/South/West)");
                        level.Add(levelSpace);
                        continue;

                    }

                    if (levelValue > 50 && levelValue <= 75)
                    {
                        Random rndPrompt = new Random();
                        int promptValue = rnd.Next(0, dangerousPrompts.Length);

                        Random rndMonster = new Random();
                        int monsterValue = rnd.Next(0, monsterNames.Length);

                        var levelSpace = new DangerousSpace("You discover "+dangerousPrompts[promptValue] + " (North/East/South/West)");
                        levelSpace.AddEnemy(new Monster($"{monsterNames[monsterValue]}", 15, 15));
                        level.Add(levelSpace);
                        continue;


                    }

                    if (levelValue > 75)
                    {
                        var levelSpace = new SafeSpace("You come across the priest's camp." + " (North/East/South/West)");
                        levelSpace.AddFriendly(new Monster("Priest", 10, 10));
                        level.Add(levelSpace);
                        continue;

                    }

                }

                LevelArea.Add(level);
            }
        }

        public bool CanMoveTo(int y, int x)
        {
            if (y >= 0 && y<= this.Size)
            {
                if(x >= 0 && x <= this.Size)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
