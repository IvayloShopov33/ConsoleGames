using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe.Players
{
    public class RandomPlayer : IPlayer
    {
        private Random random;

        public RandomPlayer()
        {
            this.random = new Random();
        }

        public Position Play(IBoard board, Symbol symbol)
        {
            Position[] availablePositions = board.GetEmptyPositions().ToArray();

            return availablePositions[random.Next(0, availablePositions.Length)];
        }
    }
}