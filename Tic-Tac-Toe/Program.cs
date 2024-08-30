using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameLogic;
using Tic_Tac_Toe.Players;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tic-Tac-Toe v1.0";
            Console.WindowHeight = 40;
            Console.WindowWidth = 60;

            while (true)
            {
                ConsoleSetUp();

                while (true)
                {
                    Console.Write("Please enter a number [1-12]: ");
                    string line = Console.ReadLine();

                    if (line == "1")
                    {
                        PlayTicTacToeGame(new ConsolePlayer(), new ConsolePlayer());
                        break;
                    }
                    else if (line == "2")
                    {
                        PlayTicTacToeGame(new ConsolePlayer(), new RandomPlayer());
                        break;
                    }
                    else if (line == "3")
                    {
                        PlayTicTacToeGame(new RandomPlayer(), new ConsolePlayer());
                        break;
                    }
                    else if (line == "4")
                    {
                        PlayTicTacToeGame(new RandomPlayer(), new RandomPlayer());
                        break;
                    }
                    else if (line == "5")
                    {
                        PlayTicTacToeGame(new ConsolePlayer(), new AiPlayer());
                        break;
                    }
                    else if (line == "6")
                    {
                        PlayTicTacToeGame(new AiPlayer(), new ConsolePlayer());
                        break;
                    }
                    else if (line == "7")
                    {
                        PlayTicTacToeGame(new RandomPlayer(), new AiPlayer());
                        break;
                    }
                    else if (line == "8")
                    {
                        Simulate(new RandomPlayer(), new RandomPlayer(), 1000);
                        break;
                    }
                    else if (line == "9")
                    {
                        Simulate(new AiPlayer(), new RandomPlayer(), 10);
                        break;
                    }
                    else if (line == "10")
                    {
                        Simulate(new RandomPlayer(), new AiPlayer(), 10);
                        break;
                    }
                    else if (line == "11")
                    {
                        Simulate(new AiPlayer(), new AiPlayer(), 10);
                        break;
                    }
                    else if (line == "12")
                    {
                        return;
                    }
                }

                Console.WriteLine("Press [enter] to continue or [esc] to exit the game...");
                ConsoleKeyInfo consoleKey = Console.ReadKey();

                if (consoleKey.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        public static void Simulate(IPlayer firstPlayer, IPlayer secondPlayer, int gamesCount)
        {
            int x = 0, o = 0, draw = 0, firstWinner = 0, secondWinner = 0;
            IPlayer first = firstPlayer;
            IPlayer second = secondPlayer;
            for (int i = 0; i < gamesCount; i++)
            {
                TicTacToeGame game = new TicTacToeGame(first, second);
                GameResult gameResult = game.Play();

                if (gameResult.Winner == Symbol.X && first == firstPlayer)
                {
                    firstWinner++;
                }
                else if (gameResult.Winner == Symbol.X && first == secondPlayer)
                {
                    secondWinner++;
                }
                else if (gameResult.Winner == Symbol.O && first == firstPlayer)
                {
                    secondWinner++;
                }
                else if (gameResult.Winner == Symbol.O && first == secondPlayer)
                {
                    firstWinner++;
                }

                if (gameResult.Winner == Symbol.X)
                {
                    x++;
                }
                else if (gameResult.Winner == Symbol.O)
                {
                    o++;
                }
                else
                {
                    draw++;
                }

                (first, second) = (second, first);
            }

            Console.WriteLine($"Games played: {gamesCount}");
            Console.WriteLine($"Games won by X: {x}");
            Console.WriteLine($"Games won by O: {o}");
            Console.WriteLine($"Draw games: {draw}");
            Console.WriteLine($"{firstPlayer.GetType().Name} won {firstWinner} games");
            Console.WriteLine($"{secondPlayer.GetType().Name} won {secondWinner} games");
        }

        public static void PlayTicTacToeGame(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            TicTacToeGame game = new TicTacToeGame(firstPlayer, secondPlayer);
            GameResult gameResult = game.Play();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Game over!");
            Console.WriteLine($"Winner: {gameResult.Winner}");
            Console.WriteLine(gameResult.Board.ToString());
            Console.ResetColor();
        }

        private static void ConsoleSetUp()
        {
            Console.Clear();
            Console.WriteLine("=== Tic-Tac-Toe v1.0 ===");
            Console.WriteLine("1. Player vs Player");
            Console.WriteLine("2. Player vs Random");
            Console.WriteLine("3. Random vs Player");
            Console.WriteLine("4. Random vs Random");
            Console.WriteLine("5. Player vs AI");
            Console.WriteLine("6. AI vs Player");
            Console.WriteLine("7. Random vs AI");
            Console.WriteLine("8. Simulate Random vs Random");
            Console.WriteLine("9. Simulate AI vs Random");
            Console.WriteLine("10. Simulate Random vs AI");
            Console.WriteLine("11. Simulate AI vs AI");
            Console.WriteLine("12. Exit the game");
        }
    }
}