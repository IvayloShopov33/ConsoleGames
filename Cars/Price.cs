namespace Cars
{
    public class Price : GameObject
    {
        public Price(int x, int y, char symbol = '$', ConsoleColor color = ConsoleColor.Green)
            : base(x, y, symbol, color)
        {
        }
    }
}