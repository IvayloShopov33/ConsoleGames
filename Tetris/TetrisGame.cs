using Tetris.Contracts;

namespace Tetris
{
    public class TetrisGame : ITetrisGame
    {
        private readonly List<Tetromino> TetrisFigures = new List<Tetromino>()
        {
            //I
            new Tetromino(new bool[,]
            {
                {true, true, true, true}
            }),

            //O
            new Tetromino(new bool[,]
            {
                {true, true},
                {true, true}
            }),

            //T
            new Tetromino(new bool[,]
            {
                {false, true, false},
                {true, true, true}
            }),

            //S
            new Tetromino(new bool[,]
            {
                {false, true, true},
                {true, true, false}
            }),

            //Z
            new Tetromino(new bool[,]
            {
                {true, true, false},
                {false, true, true}
            }),

            //J
            new Tetromino(new bool[,]
            {
                {true, false, false},
                {true, true, true}
            }),

            //L
            new Tetromino(new bool[,]
            {
                {false, false, true},
                {true, true, true}
            })
        };
        private Random Random = new Random();

        public TetrisGame(int tetrisRows, int tetrisColumns)
        {
            this.Level = 1;
            this.CurrentFigureRow = 0;
            this.CurrentFigureCol = 0;
            this.TetrisField = new bool[tetrisRows, tetrisColumns];
            this.TetrisRows = tetrisRows;
            this.TetrisColumns = tetrisColumns;

            this.NewRandomFigure();
        }

        public int Level { get; private set; }

        public int CurrentFigureRow { get; set; }

        public int CurrentFigureCol { get; set; }

        public bool[,] TetrisField { get; private set; }

        public Tetromino CurrentFigure { get; set; }

        public int TetrisRows { get; }

        public int TetrisColumns { get; }

        public void NewRandomFigure()
        {
            this.CurrentFigure = this.TetrisFigures[this.Random.Next(0, TetrisFigures.Count)];
            this.CurrentFigureRow = 0;
            this.CurrentFigureCol = 0;
        }

        public void UpdateLevel(int score)
        {
            this.Level = (int)Math.Log10(score) - 1;
            if (this.Level < 1 || score == 0)
            {
                this.Level = 1;
            }
            else if (this.Level > 10)
            {
                this.Level = 10;
            }
        }

        public void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < this.CurrentFigure.Width; row++)
            {
                for (int col = 0; col < this.CurrentFigure.Height; col++)
                {
                    if (this.CurrentFigure.Shape[row, col])
                    {
                        this.TetrisField[this.CurrentFigureRow + row, this.CurrentFigureCol + col] = true;
                    }
                }
            }
        }

        public int CheckForFullLines()
        {
            int lines = 0;

            for (int row = 0; row < this.TetrisField.GetLength(0); row++)
            {
                bool isRowFull = true;
                for (int col = 0; col < this.TetrisField.GetLength(1); col++)
                {
                    if (this.TetrisField[row, col] == false)
                    {
                        isRowFull = false;
                        break;
                    }
                }

                if (isRowFull)
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int colToMove = 0; colToMove < this.TetrisField.GetLength(1); colToMove++)
                        {
                            this.TetrisField[rowToMove, colToMove] = this.TetrisField[rowToMove - 1, colToMove];
                        }
                    }

                    lines++;
                }
            }

            return lines;
        }

        public bool Collision(Tetromino figure)
        {
            if (this.CurrentFigureCol > this.TetrisColumns - figure.Height)
            {
                return true;
            }

            if (this.CurrentFigureRow + figure.Width >= this.TetrisRows)
            {
                return true;
            }

            for (int row = 0; row < figure.Width; row++)
            {
                for (int col = 0; col < figure.Height; col++)
                {
                    if (figure.Shape[row, col] && this.TetrisField[this.CurrentFigureRow + row + 1, this.CurrentFigureCol + col])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveToLeft()
        {
            return (this.CurrentFigureCol >= 1 && !CheckForCollision(-1));
        }

        public bool CanMoveToRight()
        {
            return (this.CurrentFigureCol < this.TetrisColumns - this.CurrentFigure.Height)
                && !CheckForCollision(1);
        }

        private bool CheckForCollision(int direction) //direction = -1 left, = 1 right
        {
            for (int row = 0; row < CurrentFigure.Width; row++)
            {
                for (int col = 0; col < CurrentFigure.Height; col++)
                {
                    if (CurrentFigure.Shape[row, col] &&
                        this.TetrisField[this.CurrentFigureRow + row, this.CurrentFigureCol + col + direction])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}