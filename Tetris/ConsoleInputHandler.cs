using Tetris.Contracts;
using Tetris.Enums;

namespace Tetris
{
    public class ConsoleInputHandler : IInputHandler
    {
        public TetrisGameInput GetInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }

                if (key == ConsoleKey.Escape)
                {
                    return TetrisGameInput.Exit;
                }
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    return TetrisGameInput.Left;
                }
                else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    return TetrisGameInput.Right;
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    return TetrisGameInput.Down;
                }
                else if (key == ConsoleKey.Spacebar || key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    return TetrisGameInput.Rotate;
                }
            }

            return TetrisGameInput.None;
        }
    }
}