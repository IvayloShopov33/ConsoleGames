namespace Cars
{
    public class PlayerCar : GameObject
    {
        public PlayerCar(int x, int y, char symbol = '@', ConsoleColor color = ConsoleColor.DarkYellow)
            : base(x, y, symbol, color)
        {
        }

        public void MoveLeft()
        {
            if (X > 0) X--;
        }

        public void MoveRight(int playfieldWidth)
        {
            if (X < playfieldWidth - 1) X++;
        }
    }
}