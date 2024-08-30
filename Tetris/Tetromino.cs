namespace Tetris
{
    public class Tetromino
    {
        public Tetromino(bool[,] shape)
        {
            this.Shape = shape;
        }

        public bool[,] Shape { get; private set; }

        public int Width => this.Shape.GetLength(0);

        public int Height => this.Shape.GetLength(1);

        public Tetromino Rotate()
        {
            var newFigure = new bool[this.Height, this.Width];

            for (int row = 0; row < this.Width; row++)
            {
                for (int col = 0; col < this.Height; col++)
                {
                    newFigure[col, this.Width - row - 1] = this.Shape[row, col];
                }
            }

            return new Tetromino(newFigure);
        }
    }
}