namespace SnakeGame
{
    public class Snake
    {
        private Queue<Position> snakeElements;
        private Position[] directions;
        private int direction;

        public Snake()
        {
            snakeElements = new Queue<Position>();
            directions = new Position[]
            {
                new Position(0, 1),  // right
                new Position(0, -1), // left
                new Position(1, 0),  // down
                new Position(-1, 0)  // up
            };
            direction = 0;

            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            DrawSnake();
        }

        public void DrawSnake()
        {
            // Draw the body
            foreach (var position in snakeElements.Take(snakeElements.Count - 1))
            {
                Console.SetCursorPosition(position.Column, position.Row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('*');
            }

            // Draw the head
            Position head = GetHead();
            Console.SetCursorPosition(head.Column, head.Row);
            Console.ForegroundColor = ConsoleColor.Green;

            // Draw the head according to the current direction
            char headSymbol = direction switch
            {
                0 => '>', // right
                1 => '<', // left
                2 => 'v', // down
                3 => '^', // up
                _ => '>'  // default to right if something goes wrong
            };
            Console.Write(headSymbol);
        }

        public Position GetHead()
        {
            return snakeElements.Last();
        }

        public bool Move()
        {
            Position head = GetHead();
            Position newHead = new Position(head.Row + directions[direction].Row, head.Column + directions[direction].Column);

            if (IsCollision(newHead))
            {
                return false;
            }

            snakeElements.Enqueue(newHead);
            DrawSnake();

            return true;
        }

        public void Shrink()
        {
            // moving snake forward
            Position tail = snakeElements.Dequeue();
            Console.SetCursorPosition(tail.Column, tail.Row);
            Console.Write(' ');
        }

        public void ChangeDirection(ConsoleKey key)
        {
            if ((key == ConsoleKey.D || key == ConsoleKey.RightArrow) && direction != 1) direction = 0;
            else if ((key == ConsoleKey.A || key == ConsoleKey.LeftArrow) && direction != 0) direction = 1;
            else if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) && direction != 3) direction = 2;
            else if ((key == ConsoleKey.W || key == ConsoleKey.UpArrow) && direction != 2) direction = 3;
        }

        public bool IsCollision(Position newHead, List<Position> obstacles = null)
        {
            if (obstacles != null)
            {
                return obstacles.Any(pos => pos.Equals(newHead));
            }

            return snakeElements.Take(snakeElements.Count - 1).Any(pos => pos.Equals(newHead)) ||
                newHead.Row < 0 || newHead.Row >= Console.WindowHeight ||
                newHead.Column < 0 || newHead.Column >= Console.WindowWidth;
        }

        public bool Contains(Position position)
        {
            return snakeElements.Any(pos => pos.Equals(position));
        }

        public int Length => snakeElements.Count;
    }
}