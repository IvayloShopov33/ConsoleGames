namespace PingPong
{
    public class Game
    {
        private Player firstPlayer;
        private Player secondPlayer;
        private Ball ball;
        private string gameType;

        public void Setup()
        {
            ConsoleSetUp();
            gameType = ChooseGameType();

            Console.Write("First Player's Name is: ");
            string firstPlayerName = Console.ReadLine();
            firstPlayer = new Player(firstPlayerName, 0, Console.WindowHeight / 2 - 3);

            if (gameType == "Multiplayer")
            {
                Console.Write("Second Player's Name is: ");
                string secondPlayerName = Console.ReadLine();
                secondPlayer = new Player(secondPlayerName, Console.WindowWidth - 1, Console.WindowHeight / 2 - 3);
            }
            else
            {
                secondPlayer = new AIPlayer("Robot Master", Console.WindowWidth - 1, Console.WindowHeight / 2 - 3);
            }

            ball = new Ball();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();

                HandleInput();

                if (gameType == "Singleplayer")
                {
                    (secondPlayer as AIPlayer)?.Move(ball);
                }

                firstPlayer.Draw();
                secondPlayer.Draw();

                ball.Move(firstPlayer, secondPlayer);
                ball.Draw();

                PrintResult();

                Thread.Sleep(50);
            }
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                if (key == ConsoleKey.W)
                {
                    firstPlayer.MoveUp();
                }
                else if (key == ConsoleKey.S)
                {
                    firstPlayer.MoveDown();
                }
                else if (key == ConsoleKey.Escape)
                {
                    GameOver();
                }

                if (gameType == "Multiplayer")
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        secondPlayer.MoveUp();
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        secondPlayer.MoveDown();
                    }
                }
            }
        }

        private void ConsoleSetUp()
        {
            Console.WindowWidth = 80;
            Console.WindowHeight = 20;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        private string ChooseGameType()
        {
            var gameTypes = new Dictionary<int, string>
            {
                {1, "Singleplayer"},
                {2, "Multiplayer"}
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose Game Type:");
                Console.WriteLine("1. One player against the computer");
                Console.WriteLine("2. Two players against each other");
                Console.Write("Your choice is: ");

                if (int.TryParse(Console.ReadLine(), out int gameType) && gameTypes.ContainsKey(gameType))
                {
                    return gameTypes[gameType];
                }
            }
        }

        private void PrintResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write($"{firstPlayer.Score}-{secondPlayer.Score}");
        }

        private void GameOver()
        {
            string winner = firstPlayer.Score > secondPlayer.Score
                ? $"The winner is: {firstPlayer.Name}"
                : $"The winner is: {secondPlayer.Name}";

            if (firstPlayer.Score == secondPlayer.Score)
            {
                winner = "There is no winner";
            }

            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 5);
            Console.WriteLine($"{winner}. Final Score: {firstPlayer.Score}-{secondPlayer.Score}");
            Environment.Exit(0);
        }
    }
}