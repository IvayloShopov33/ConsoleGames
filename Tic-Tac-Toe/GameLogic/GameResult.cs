using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;

namespace Tic_Tac_Toe.GameLogic
{
    public class GameResult
    {
        public GameResult(Symbol winner, IBoard board)
        {
            this.Winner = winner;
            this.Board = board;
        }

        public Symbol Winner { get; }
        public IBoard Board { get; }
    }
}