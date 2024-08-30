namespace PingPong
{
    public class Ball
    {
        private char ballCharacter = '*';

        public Ball()
        {
            SetAtCenterPosition();
        }

        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public bool DirectionUp { get; private set; } = true;

        public bool DirectionRight { get; private set; } = true;

        public void Move(Player firstPlayer, Player secondPlayer)
        {
            if (PositionY == 0)
            {
                DirectionUp = false;
            }
            else if (PositionY == Console.WindowHeight - 1)
            {
                DirectionUp = true;
            }

            if (PositionX == Console.WindowWidth - 1)
            {
                firstPlayer.Score++;
                SetAtCenterPosition();
                DirectionRight = false;
            }
            else if (PositionX == 0)
            {
                secondPlayer.Score++;
                SetAtCenterPosition();
                DirectionRight = true;
            }

            if (PositionX < 2 && PositionY >= firstPlayer.PositionY && PositionY < firstPlayer.PositionY + firstPlayer.PadSize)
            {
                DirectionRight = true;
            }
            else if (PositionX > Console.WindowWidth - 3 && PositionY >= secondPlayer.PositionY && PositionY < secondPlayer.PositionY + secondPlayer.PadSize)
            {
                DirectionRight = false;
            }

            if (DirectionUp)
            {
                PositionY--;
            }
            else
            {
                PositionY++;
            }

            if (DirectionRight)
            {
                PositionX++;
            }
            else
            {
                PositionX--;
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(ballCharacter);
        }

        private void SetAtCenterPosition()
        {
            PositionX = Console.WindowWidth / 2;
            PositionY = Console.WindowHeight / 2;
        }
    }
}