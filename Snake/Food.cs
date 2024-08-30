namespace SnakeGame
{
    public class Food
    {
        public Position Position { get; private set; }

        public Food(Snake snake, List<Position> obstacles)
        {
            GenerateNewPosition(snake, obstacles);
            Draw();
        }

        public void GenerateNewPosition(Snake snake, List<Position> obstacles)
        {
            Random random = new Random();
            do
            {
                // snake is taking food
                Position = new Position(random.Next(0, Console.WindowHeight),
                                        random.Next(0, Console.WindowWidth));
            } while (snake.Contains(Position) || obstacles.Any(o => o.Equals(Position)));
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.Column, Position.Row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write('@'); // adding food
        }
    }
}