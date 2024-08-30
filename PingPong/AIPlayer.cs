namespace PingPong
{
    public class AIPlayer : Player
    {
        private Random random = new Random();

        public AIPlayer(string name, int positionX, int positionY, int padSize = 6)
            : base(name, positionX, positionY, padSize)
        {
        }

        public void Move(Ball ball)
        {
            int randomNumber = random.Next(1, 101);

            if (randomNumber > 75)
            {
                if (ball.DirectionUp)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }
}