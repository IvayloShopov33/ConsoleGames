namespace Cars
{
    public class Car : GameObject
    {
        public Car(int x, int y, char symbol = '#', ConsoleColor color = ConsoleColor.Cyan)
            : base(x, y, symbol, color)
        {
        }
    }
}