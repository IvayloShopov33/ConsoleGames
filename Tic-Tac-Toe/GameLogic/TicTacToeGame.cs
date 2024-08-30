using Tic_Tac_Toe.Players.Contracts;
using Tic_Tac_Toe.GameBoard;

namespace Tic_Tac_Toe.GameLogic
{
    public class TicTacToeGame
    {
        public TicTacToeGame(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            this.WinnerLogic = new GameWinnerLogic();
        }

        public IPlayer FirstPlayer { get; }

        public IPlayer SecondPlayer { get; }

        public GameWinnerLogic WinnerLogic { get; }

        public GameResult Play()
        {
            Board board = new Board();
            IPlayer currentPlayer = FirstPlayer;
            Symbol currentSymbol = Symbol.X;

            while (!this.WinnerLogic.IsGameOver(board))
            {
                Position move = currentPlayer.Play(board, currentSymbol);
                board.PlaceSymbol(move, currentSymbol);

                if (currentPlayer == FirstPlayer)
                {
                    currentPlayer = SecondPlayer;
                    currentSymbol = Symbol.O;
                }
                else
                {
                    currentPlayer = FirstPlayer;
                    currentSymbol = Symbol.X;
                }
            }

            Symbol winner = this.WinnerLogic.GetWinner(board);

            return new GameResult(winner, board);
        }
    }
}