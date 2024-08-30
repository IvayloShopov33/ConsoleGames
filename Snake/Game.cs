namespace SnakeGame
{
    public class Game
    {
        private Snake snake;
        private Food food;
        private Obstacle obstacles;
        private int lastFoodTime;
        private int foodDisappearTime = 13000;
        private int negativePoints;
        private double sleepTime = 120;

        public void Start()
        {
            ConsoleSetup();
            snake = new Snake();
            obstacles = new Obstacle(snake, 3);
            food = new Food(snake, obstacles.GetObstacles());

            lastFoodTime = Environment.TickCount;

            while (true)
            {
                Update();
                Thread.Sleep((int)sleepTime);
            }
        }

        private void ConsoleSetup()
        {
            // Settings
            Console.CursorVisible = false;
            Console.WindowHeight = 30;
            Console.WindowWidth = 50;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        private void Update()
        {
            if (Console.KeyAvailable)
            {
                snake.ChangeDirection(Console.ReadKey(true).Key);
            }

            Position head = snake.GetHead();

            if (!snake.Move() || snake.IsCollision(head, obstacles.GetObstacles()))
            {
                GameOver();
            }

            if (head.Equals(food.Position))
            {
                food.GenerateNewPosition(snake, obstacles.GetObstacles());
                food.Draw();
                obstacles.AddNewObstacle(snake, food.Position);
                sleepTime--; // Increase speed
            }
            else
            {
                snake.Shrink();
            }

            if (Environment.TickCount - lastFoodTime >= foodDisappearTime)
            {
                negativePoints += 5; // penalty for not taking the food
                food.GenerateNewPosition(snake, obstacles.GetObstacles());
                food.Draw();
                lastFoodTime = Environment.TickCount;
            }

            sleepTime = Math.Max(1, sleepTime - 0.01);
        }

        private void GameOver()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over!");

            int points = (snake.Length - 6) * 10 - negativePoints;
            Console.WriteLine($"You have {Math.Max(0, points)} points!");
            Environment.Exit(0); // game finished
        }
    }
}