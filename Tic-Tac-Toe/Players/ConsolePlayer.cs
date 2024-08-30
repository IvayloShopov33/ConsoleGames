using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe.Players
{
    public class ConsolePlayer : IPlayer
    {
        public Position Play(IBoard board, Symbol symbol)
        {
            Console.WriteLine(board.ToString());
            Position position;

            while (true)
            {
                Console.Write($"{symbol} please enter position (x, y): ");
                string line = Console.ReadLine();

                try
                {
                    position = new Position(line);
                }
                catch
                {
                    Console.WriteLine("Invalid position format.");

                    continue;
                }

                if (!board.GetEmptyPositions().Any(x => x.Equals(position)))
                {
                    Console.WriteLine("Unavailable position.");

                    continue;
                }

                break;
            }

            return position;
        }
    }
}