using System;
using System.Timers;
using System.Linq;


namespace console_game
{
    public class Game
    {

        public int Score;
        private Player player;

        public Game()
        {
        }

        public void Start()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();


            // Starting Sequence
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("A ray of light hits your eyelids as you wake");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("...");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("You hear birds chirping as you realise you are in a foreign, dense forest.");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("An elderly man approaches you and asks...");
            Console.WriteLine("");
            Console.WriteLine("");

            // Takes in player name
            Console.Write("Lost traveller, what is your name?: ");
            var PlayerName = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine($"{PlayerName}, was it? Good luck!");
            Console.WriteLine("");

            // Creates new player and gamefield
            this.player = new Player(PlayerName, 50, 20);
            GameField gamefield = new GameField(20);
            gamefield.Generate();

            // Runs game while player is alive
            while (player.isAlive)
            {

                LevelSpace currentLevelSpace = gamefield.LevelArea[player.yPosition][player.xPosition];

                currentLevelSpace.isActive = true;

                while (currentLevelSpace.isActive)
                {

                    // Sequence for levelspace with enemies
                    while (currentLevelSpace.HasEnemies())
                    {

                        currentLevelSpace.Enemies.ForEach(enemy => {
                            Console.WriteLine($"A dangerous {enemy.Name} approaches you!");
                            Console.WriteLine("");

                            if (UserYesOrNo($"Will you fight the {enemy.Name}?"))
                            {
                                while (enemy.isAlive)
                                {
                                    Console.WriteLine($"{player.Name} swings their huge fist at {enemy.Name}!");
                                    Console.WriteLine("");
                                    Console.WriteLine($"You deal {player.Attack(enemy)} damage to {enemy.Name}!");
                                    Console.WriteLine("");
                                    Console.WriteLine($"{enemy.Name} bites back and deals {enemy.Attack(player)} damage!");
                                    Console.WriteLine("");
                                }

                                if (!enemy.isAlive) {

                                    this.Score += enemy.Score;

                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine($"   {enemy.Name} has died! You gained {enemy.Score} score!");
                                    Console.WriteLine($"   Health: {(player.Health < 0 ? "0" : player.Health.ToString())}  ---- Score: {this.Score}    ");
                                    Console.WriteLine("***********************************************************");
                                    Console.WriteLine("");
                                }

                            } else {

                                Console.WriteLine($"You run away from {enemy.Name}, it manages to hit you for {enemy.Attack(player)} damage!");
                                Console.WriteLine("");

                            }

                        });

                        currentLevelSpace.Enemies.Clear();
                    }

                    // Sequence for levelspace with friendlies
                    while (currentLevelSpace.HasFriendlies())
                    {
                        currentLevelSpace.Friendlies.ForEach(friendly =>
                        {
                            Console.WriteLine($"{friendly.Name} heals you for {friendly.Heal(player)} health!");
                            Console.WriteLine("");

                            if(player.Health > 100)
                            {
                                player.Health = 100;
                            }
                        });

                        currentLevelSpace.Friendlies.Clear();
                    }

                    // Breaks game loop if player died after fight
                    if (!player.isAlive)
                    {
                        break;
                    }

                    // Get Directions for next move
                    var direction = GetUserDirection(currentLevelSpace.prompt);

                    if (direction == UserDirections.North)
                    {
                        if (gamefield.CanMoveTo(player.yPosition - 1, player.xPosition))
                        {
                            Console.WriteLine("You travel North...");
                            Console.WriteLine("");
                            player.yPosition -= 1;
                            currentLevelSpace.isActive = false;
                        }
                        else
                        {
                            Console.WriteLine("The road leading north is blocked, you go back...");
                            Console.WriteLine("");
                        }
                    }

                    if (direction == UserDirections.East)
                    {
                        if (gamefield.CanMoveTo(player.yPosition, player.xPosition + 1))
                        {
                            Console.WriteLine("You travel East...");
                            Console.WriteLine("");
                            player.xPosition += 1;
                            currentLevelSpace.isActive = false;
                        }
                        else
                        {
                            Console.WriteLine("The road leading east is blocked, you go back...");
                            Console.WriteLine("");
                        }
                    }

                    if (direction == UserDirections.West)
                    {
                        if (gamefield.CanMoveTo(player.yPosition, player.xPosition - 1))
                        {
                            Console.WriteLine("You travel West...");
                            Console.WriteLine("");
                            player.xPosition -= 1;
                            currentLevelSpace.isActive = false;
                        }
                        else
                        {
                            Console.WriteLine("The road leading west is blocked, you go back...");
                            Console.WriteLine("");
                        }
                    }

                    if (direction == UserDirections.South)
                    {
                        if (gamefield.CanMoveTo(player.yPosition + 1, player.xPosition))
                        {
                            Console.WriteLine("You travel South...");
                            Console.WriteLine("");
                            player.yPosition += 1;
                            currentLevelSpace.isActive = false;
                        }
                        else
                        {
                            Console.WriteLine("The road leading south is blocked, you go back...");
                            Console.WriteLine("");
                        }
                    }
                }
            }

            // When player dies
            if(UserYesOrNo($"The lost traveller {player.Name} has fallen... Score: {this.Score} Try again?"))
            {
                // Starts new game
                Start();
            }
            else
            {
                Console.WriteLine($"Your adventure now ends.");
            }

        }

        // Method that prompts user with question, return true if Yes, return false if No
        private bool UserYesOrNo(string prompt)
        {
            var HasCorrectInput = false;

            while (!HasCorrectInput)
            {
                Console.WriteLine($"{prompt} (y/n)");
                Console.WriteLine("");

                var userInput = Console.ReadLine().ToLower();
                Console.WriteLine("");

                if (userInput == "y")
                {
                    return true;
                }

                if (userInput == "n")
                {
                    HasCorrectInput = true;
                    return false;
                }
            }

            return true;
        }

        private UserDirections GetUserDirection(string prompt)
        {
            var HasCorrectInput = false;
            var IsPrompted = false;

            while (!HasCorrectInput)
            {

                if (!IsPrompted)
                {
                    Console.WriteLine(prompt);
                    Console.WriteLine("");
                    Console.WriteLine("");

                    IsPrompted = true;
                }

                var userInput = Console.ReadLine().ToLower();
                Console.WriteLine("");

                if (userInput == "north")
                {
                    return UserDirections.North;
                }

                if (userInput == "south")
                {
                    HasCorrectInput = true;
                    return UserDirections.South;
                }

                if (userInput == "west")
                {
                    HasCorrectInput = true;
                    return UserDirections.West;
                }

                if (userInput == "east")
                {
                    HasCorrectInput = true;
                    return UserDirections.East;
                }

                // Hidden user input case for test purposes
                if (userInput == "kms")
                {
                    HasCorrectInput = true;
                    player.TakeDamage(100);
                }
                else
                {

                    Console.WriteLine($"Command '{userInput}' not recognized.");
                    Console.WriteLine("");

                }


            }

            return UserDirections.West;

        }
    }
}
