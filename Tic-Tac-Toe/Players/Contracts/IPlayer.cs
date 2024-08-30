using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;

namespace Tic_Tac_Toe.Players.Contracts
{
    public interface IPlayer
    {
        Position Play(IBoard board, Symbol symbol);
    }
}