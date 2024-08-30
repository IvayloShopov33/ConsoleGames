using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;

namespace Tic_Tac_Toe.GameLogic
{
    public class GameWinnerLogic
    {
        public Symbol GetWinner(IBoard board)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                Symbol winner = board.GetRowSymbol(row);

                if (winner != Symbol.None)
                {
                    return winner;
                }
            }

            for (int col = 0; col < board.Columns; col++)
            {
                Symbol winner = board.GetColumnSymbol(col);

                if (winner != Symbol.None)
                {
                    return winner;
                }
            }

            Symbol firstDiagonalWinner = board.GetDiagonalRightToLeftSymbol();
            if (firstDiagonalWinner != Symbol.None)
            {
                return firstDiagonalWinner;
            }

            Symbol secondDiagonalWinner = board.GetDiagonalLeftToRightSymbol();
            if (secondDiagonalWinner != Symbol.None)
            {
                return secondDiagonalWinner;
            }

            return Symbol.None;
        }

        public bool IsGameOver(IBoard board)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                if (board.GetRowSymbol(row) != Symbol.None)
                {
                    return true;
                }
            }

            for (int col = 0; col < board.Columns; col++)
            {
                if (board.GetColumnSymbol(col) != Symbol.None)
                {
                    return true;
                }
            }

            if (board.GetDiagonalRightToLeftSymbol() != Symbol.None)
            {
                return true;
            }

            if (board.GetDiagonalLeftToRightSymbol() != Symbol.None)
            {
                return true;
            }

            return board.IsFull();
        }
    }
}