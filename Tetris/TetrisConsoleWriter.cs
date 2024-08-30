using System.Text;

using Tetris.Contracts;

namespace Tetris
{
    public class TetrisConsoleWriter : ITetrisWriter
    {
        private int tetrisRows;
        private int tetrisColumns;
        private int infoColumns;
        private int consoleRows;
        private int consoleCols;
        private char tetrisCharacter;

        public TetrisConsoleWriter(int tetrisRows, int tetrisColumns, int infoColumns, char tetrisCharacter = '*')
        {
            this.tetrisRows = tetrisRows;
            this.tetrisColumns = tetrisColumns;
            this.infoColumns = infoColumns;
            this.consoleRows = this.tetrisRows + 2;
            this.consoleCols = this.tetrisColumns + this.infoColumns + 3;
            this.tetrisCharacter = tetrisCharacter;
            this.Frame = 0;
            this.FramesToMoveFigure = 16;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = "Tetris v1.0";
            Console.WindowHeight = this.consoleRows + 1;
            Console.WindowWidth = this.consoleCols;
            Console.BufferHeight = this.consoleRows + 1;
            Console.BufferWidth = this.consoleCols;
            Console.CursorVisible = false;
        }

        public int TetrisRows => this.tetrisRows;

        public int TetrisColumns => this.tetrisColumns;

        public int InfoColumns => this.infoColumns;

        public int Frame { get; set; }

        public int FramesToMoveFigure { get; private set; }

        public void DrawGameState(int tetrisColumns, ITetrisGame state, IScoreManager scoreManager)
        {
            this.Write("Level:", 1, tetrisColumns + 3);
            this.Write(state.Level.ToString(), 2, tetrisColumns + 3);
            this.Write("Score:", 4, tetrisColumns + 3);
            this.Write(scoreManager.Score.ToString(), 5, tetrisColumns + 3);
            this.Write("High Score:", 7, tetrisColumns + 3);
            this.Write(scoreManager.HighScore.ToString(), 8, tetrisColumns + 3);
            this.Write("Frame:", 10, tetrisColumns + 3);
            this.Write($"{this.Frame.ToString()} / {(this.FramesToMoveFigure - state.Level).ToString()}", 11, tetrisColumns + 3);
            this.Write("Position:", 13, tetrisColumns + 3);
            this.Write($"{state.CurrentFigureRow}, {state.CurrentFigureCol}", 14, tetrisColumns + 3);
            this.Write("Keys:", 16, tetrisColumns + 3);
            this.Write("  W      ^", 18, tetrisColumns + 3);
            this.Write("A S D  < v >", 19, tetrisColumns + 3);
        }

        public void DrawBorder(int tetrisRows, int tetrisColumns, int infoColumns)
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', tetrisColumns);
            line += "╦";
            line += new string('═', infoColumns);
            line += "╗";
            Console.WriteLine(line);
            string middleLine = string.Empty;
            for (int i = 0; i < tetrisRows; i++)
            {
                middleLine = "║";
                middleLine += new string(' ', tetrisColumns);
                middleLine += "║";
                middleLine += new string(' ', infoColumns);
                middleLine += "║";
                Console.Write(middleLine);
            }

            string lastLine = "╚";
            lastLine += new string('═', tetrisColumns);
            lastLine += "╩";
            lastLine += new string('═', infoColumns);
            lastLine += "╝";
            Console.WriteLine(lastLine);
        }

        public void GameOver(int score)
        {
            int row = this.tetrisRows / 2 - 3;
            int col = this.tetrisColumns / 2;

            var scoreAsString = score.ToString();

            Write("╔═════════╗", row, col);
            Write("║  Game   ║", row + 1, col);
            Write("║   over! ║", row + 2, col);

            var spacesToInsert = 5;
            if (scoreAsString.Length == 1)
            {
                Write($"║{scoreAsString} p.{new string(' ', spacesToInsert)}║", row + 3, col);

            }
            else if (scoreAsString.Length == 2)
            {
                Write($"║{scoreAsString} p.{new string(' ', spacesToInsert - 1)}║", row + 3, col);

            }
            else if (scoreAsString.Length == 3)
            {
                Write($"║{scoreAsString} p.{new string(' ', spacesToInsert - 2)}║", row + 3, col);

            }
            else if (scoreAsString.Length == 4)
            {
                Write($"║{scoreAsString} p.{new string(' ', spacesToInsert - 3)}║", row + 3, col);

            }
            else if (scoreAsString.Length == 5)
            {
                Write($"║{scoreAsString} p.{new string(' ', spacesToInsert - 4)}║", row + 3, col);

            }
            else if (scoreAsString.Length == 6)
            {
                Write($"║{scoreAsString} p.║", row + 3, col);

            }

            Write("╚═════════╝", row + 4, col);

            Environment.Exit(0);
        }

        public void DrawCurrentFigure(Tetromino currentFigure, int currentFigureRow, int currentFigureCol)
        {
            for (int row = 0; row < currentFigure.Width; row++)
            {
                for (int col = 0; col < currentFigure.Height; col++)
                {
                    if (currentFigure.Shape[row, col])
                    {
                        Write(this.tetrisCharacter.ToString(), row + 1 + currentFigureRow, col + 1 + currentFigureCol);
                    }
                }
            }
        }

        public void DrawTetrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                var line = new StringBuilder();
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    if (tetrisField[row, col])
                    {
                        line.Append(this.tetrisCharacter);
                    }
                    else
                    {
                        line.Append(' ');
                    }
                }

                Write(line.ToString(), row + 1, 1);
            }
        }

        private void Write(string text, int row, int col)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(text);
        }
    }
}