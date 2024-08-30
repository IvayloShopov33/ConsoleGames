using Tetris.Enums;

namespace Tetris.Contracts
{
    public interface IInputHandler
    {
        TetrisGameInput GetInput();
    }
}