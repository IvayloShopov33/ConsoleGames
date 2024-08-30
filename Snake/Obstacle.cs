namespace SnakeGame
{
    public class Obstacle
    {
        private List<Position> obstacles;

        public Obstacle(Snake snake, int count)
        {
            obstacles = new List<Position>();
            GenerateObstacles(snake, count);
        }

        public void GenerateObstacles(Snake snake, int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Position obstacle;
                do
                {
                    // adding an obstacle
                    obstacle = new Position(random.Next(0, Console.WindowHeight),
                                            random.Next(0, Console.WindowWidth));
                } while (obstacles.Any(o => o.Equals(obstacle)) || snake.Contains(obstacle));

                obstacles.Add(obstacle);
                Draw(obstacle);
            }
        }

        public void AddNewObstacle(Snake snake, Position food)
        {
            Position obstacle;
            Random random = new Random();
            do
            {
                obstacle = new Position(random.Next(0, Console.WindowHeight),
                                        random.Next(0, Console.WindowWidth));
            } while (obstacles.Any(o => o.Equals(obstacle)) || snake.Contains(obstacle) ||
                     (food.Row == obstacle.Row && food.Column == obstacle.Column));

            obstacles.Add(obstacle);
            Draw(obstacle);
        }

        public List<Position> GetObstacles()
        {
            return obstacles;
        }

        private void Draw(Position obstacle)
        {
            Console.SetCursorPosition(obstacle.Column, obstacle.Row);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write('='); // creating obstacles
        }
    }
}