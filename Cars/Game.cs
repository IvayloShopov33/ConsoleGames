using System.Text;

namespace Cars
{
    public class Game
    {
        private int speed = 0;
        private int playfieldWidth = 9;
        private int livesCount = 5;
        private int score = 0;
        private int acceleration = 1;
        private Random random = new Random();

        private PlayerCar playerCar;
        private List<Car> cars;
        private List<Price> prices;
        private List<Life> lives;

        public Game()
        {
            ConsoleSetUp();
            playerCar = new PlayerCar(playfieldWidth / 2, Console.WindowHeight - 1);
            cars = new List<Car>();
            prices = new List<Price>();
            lives = new List<Life>();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                speed += acceleration;
                if (speed > 500) speed = 500;

                bool hitted = false;

                SpawnObjects();
                HandleInput();

                UpdateObjects(ref hitted);

                DrawObjects();

                if (hitted)
                {
                    playerCar.Draw();
                }

                DrawUI();

                Thread.Sleep(600 - speed);
            }
        }

        private void ConsoleSetUp()
        {
            Console.WindowWidth = 30;
            Console.WindowHeight = 20;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.OutputEncoding = Encoding.UTF8;
        }

        private void SpawnObjects()
        {
            var newCar = new Car(random.Next(0, playfieldWidth), 0);
            cars.Add(newCar);

            int chance = random.Next(0, 101);
            if (chance <= 3)
            {
                var life = new Life(random.Next(0, playfieldWidth), 0);
                lives.Add(life);
            }
            else if (chance < 20)
            {
                var price = new Price(random.Next(0, playfieldWidth), 0);
                prices.Add(price);
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

                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
                {
                    playerCar.MoveLeft();
                }
                else if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
                {
                    playerCar.MoveRight(playfieldWidth);
                }
                else if (key == ConsoleKey.Escape)
                {
                    GameOver();
                }
            }
        }

        private void UpdateObjects(ref bool hitted)
        {
            UpdateListOfObjects(cars, ref hitted, true);
            UpdateListOfObjects(prices, ref hitted, false, true);
            UpdateListOfObjects(lives, ref hitted, false, false, true);
        }

        private void UpdateListOfObjects<T>(List<T> objects, ref bool hitted, bool isCar = false, bool isPrice = false, bool isLife = false) where T : GameObject
        {
            List<T> newList = new List<T>();

            foreach (var obj in objects)
            {
                obj.MoveDown();

                if (obj.X == playerCar.X && obj.Y == playerCar.Y)
                {
                    if (isCar)
                    {
                        livesCount--;
                        hitted = true;
                        if (livesCount <= 0) GameOver();
                    }
                    else if (isPrice)
                    {
                        score += 5;
                        speed = Math.Max(0, speed - 20);
                    }
                    else if (isLife)
                    {
                        livesCount++;
                    }
                }

                if (obj.Y < Console.WindowHeight)
                {
                    newList.Add(obj);
                }
                else if (!hitted && isCar)
                {
                    score++;
                    speed = Math.Min(500, speed + 2);
                }
            }

            objects.Clear();
            objects.AddRange(newList);
        }

        private void DrawObjects()
        {
            playerCar.Draw();

            foreach (var car in cars)
            {
                car.Draw();
            }

            foreach (var price in prices)
            {
                price.Draw();
            }

            foreach (var life in lives)
            {
                life.Draw();
            }

            DrawBorder();
        }

        private void DrawUI()
        {
            PrintStringAtPosition(13, 4, $"Lives: {livesCount}", ConsoleColor.White);
            PrintStringAtPosition(13, 6, $"Score: {score}", ConsoleColor.White);
            PrintStringAtPosition(13, 8, $"Speed: {speed}", ConsoleColor.White);
            PrintStringAtPosition(13, 10, $"Acceleration: {acceleration}", ConsoleColor.White);
        }

        private void DrawBorder()
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                PrintAtPosition(playfieldWidth, y, '|', ConsoleColor.Yellow);
            }
        }

        private void PrintAtPosition(int x, int y, char symbol, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
            Console.ResetColor();
        }

        private void PrintStringAtPosition(int x, int y, string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        private void GameOver()
        {
            PrintStringAtPosition(6, 8, $"Game over! Score: {score}", ConsoleColor.Red);
            Environment.Exit(0);
        }
    }
}