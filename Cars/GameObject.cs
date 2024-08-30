namespace Cars
{
    public class GameObject
    {
        public GameObject(int x, int y, char symbol, ConsoleColor color)
        {
            X = x;
            Y = y;
            Symbol = symbol;
            Color = color;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public char Symbol { get; set; }

        public ConsoleColor Color { get; set; }

        public virtual void MoveDown()
        {
            Y++;
        }

        public virtual void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}