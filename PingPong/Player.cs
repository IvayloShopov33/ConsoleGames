namespace PingPong
{
    public class Player
    {
        protected char playerCharacter = '|';

        public Player(string name, int positionX, int positionY, int padSize = 6)
        {
            Name = name;
            PositionX = positionX;
            PositionY = positionY;
            PadSize = padSize;
        }

        public string Name { get; }

        public int PositionY { get; protected set; }

        public int PositionX { get; }

        public int PadSize { get; }

        public int Score { get; set; }

        public void MoveUp()
        {
            if (PositionY > 0)
            {
                PositionY--;
            }
        }

        public void MoveDown()
        {
            if (PositionY + PadSize < Console.WindowHeight)
            {
                PositionY++;
            }
        }

        public void Draw()
        {
            for (int y = PositionY; y < PositionY + PadSize; y++)
            {
                PrintAtPosition(PositionX, y, playerCharacter);
            }
        }

        protected void PrintAtPosition(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}