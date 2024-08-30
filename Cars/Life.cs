namespace Cars
{
    public class Life : GameObject
    {
        public Life(int x, int y, char symbol = '\u2764', ConsoleColor color = ConsoleColor.Red)
            : base(x, y, symbol, color)
        {
        }
    }
}