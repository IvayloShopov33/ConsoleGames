using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;
using Tic_Tac_Toe.GameLogic;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe.Players
{
    public class AiPlayer : IPlayer
    {
        public AiPlayer()
        {
            this.WinnerLogic = new GameWinnerLogic();
        }

        public GameWinnerLogic WinnerLogic { get; }

        public Position Play(IBoard board, Symbol symbol)
        {
            Position[] availablePositions = board.GetEmptyPositions().ToArray();
            Position bestMove = null;
            int bestMoveValue = -1000;

            foreach (Position position in availablePositions)
            {
                board.PlaceSymbol(position, symbol);
                int value = MiniMax(board, symbol, symbol == Symbol.X ? Symbol.O : Symbol.X);

                if (value > bestMoveValue)
                {
                    bestMove = position;
                    bestMoveValue = value;
                }

                board.PlaceSymbol(position, Symbol.None);
            }

            return bestMove;
        }

        private int MiniMax(IBoard board, Symbol player, Symbol currentPlayer)
        {
            if (this.WinnerLogic.IsGameOver(board))
            {
                Symbol winner = this.WinnerLogic.GetWinner(board);

                if (player == winner)
                {
                    return 1;
                }
                else if (winner == Symbol.None)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

            Position[] availablePositions = board.GetEmptyPositions().ToArray();
            int bestMoveValue = player == currentPlayer ? -100 : 100;

            foreach (Position position in availablePositions)
            {
                board.PlaceSymbol(position, currentPlayer);
                int value = MiniMax(board, player, currentPlayer == Symbol.O ? Symbol.X : Symbol.O);
                board.PlaceSymbol(position, Symbol.None);
                bestMoveValue = currentPlayer == player ? Math.Max(bestMoveValue, value) : Math.Min(bestMoveValue, value);
            }

            return bestMoveValue;
        }
    }
}