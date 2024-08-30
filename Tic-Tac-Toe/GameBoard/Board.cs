using System.Text;
using Tic_Tac_Toe.GameBoard.Contracts;

namespace Tic_Tac_Toe.GameBoard
{
    public class Board : IBoard
    {
        private Symbol[,] symbolsOnBoard;

        public Board()
            : this(3, 3)
        {

        }

        public Board(int rows, int columns)
        {
            if (rows != columns)
            {
                throw new ArgumentException("Rows and columns should be equal.");
            }

            this.Rows = rows;
            this.Columns = columns;
            this.symbolsOnBoard = new Symbol[this.Rows, this.Columns];
        }

        public int Rows { get; }

        public int Columns { get; }

        public Symbol[,] SymbolsOnBoard => this.symbolsOnBoard;

        public Symbol GetRowSymbol(int row)
        {
            Symbol symbol = this.SymbolsOnBoard[row, 0];
            if (symbol == Symbol.None)
            {
                return Symbol.None;
            }

            for (int col = 1; col < this.Columns; col++)
            {
                if (this.symbolsOnBoard[row, col] != symbol)
                {
                    return Symbol.None;
                }
            }

            return symbol;
        }

        public Symbol GetColumnSymbol(int column)
        {
            Symbol symbol = this.symbolsOnBoard[0, column];
            if (symbol == Symbol.None)
            {
                return Symbol.None;
            }

            for (int row = 1; row < this.Rows; row++)
            {
                if (this.symbolsOnBoard[row, column] != symbol)
                {
                    return Symbol.None;
                }
            }

            return symbol;
        }

        public Symbol GetDiagonalRightToLeftSymbol()
        {
            Symbol symbol = this.symbolsOnBoard[0, 0];
            if (symbol == Symbol.None)
            {
                return Symbol.None;
            }

            for (int i = 1; i < this.Rows; i++)
            {
                if (this.symbolsOnBoard[i, i] != symbol)
                {
                    return Symbol.None;
                }
            }

            return symbol;
        }

        public Symbol GetDiagonalLeftToRightSymbol()
        {
            Symbol symbol = this.symbolsOnBoard[0, this.symbolsOnBoard.GetLength(1) - 1];
            if (symbol == Symbol.None)
            {
                return Symbol.None;
            }

            for (int i = 1; i < this.Rows; i++)
            {
                if (this.symbolsOnBoard[i, this.symbolsOnBoard.GetLength(1) - 1 - i] != symbol)
                {
                    return Symbol.None;
                }
            }

            return symbol;
        }

        public IEnumerable<Position> GetEmptyPositions()
        {
            List<Position> positions = new List<Position>();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Columns; col++)
                {
                    if (this.symbolsOnBoard[row, col] == Symbol.None)
                    {
                        positions.Add(new Position(row, col));
                    }
                }
            }

            return positions;
        }

        public bool IsFull()
        {
            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Columns; col++)
                {
                    if (this.symbolsOnBoard[row, col] == Symbol.None)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void PlaceSymbol(Position position, Symbol symbol)
        {
            if (position.Row < 0 || position.Row >= Rows ||
                position.Column < 0 || position.Column >= Columns)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            this.symbolsOnBoard[position.Row, position.Column] = symbol;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col < this.Columns; col++)
                {
                    if (this.SymbolsOnBoard[row, col] != Symbol.None)
                    {
                        sb.Append(this.SymbolsOnBoard[row, col]);
                    }
                    else
                    {
                        sb.Append('-');
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString().TrimEnd();
        }
    }
}