using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;

namespace Tic_Tac_Toe_Tests
{
    [TestFixture]
    public class BoardTests
    {
        private IBoard board;

        [SetUp]
        public void SetUp()
        {
            this.board = new Board();
        }

        [Test]
        public void Constructor_ShouldThrowAnExceptionWhenRowsAndColumnsAreNotEqual()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => this.board = new Board(4, 5));

            Assert.AreEqual("Rows and columns should be equal.", exception.Message);
        }

        [TestCase(Symbol.X)]
        [TestCase(Symbol.O)]
        public void GetRowSymbolMethod_ShouldWorkCorrectly(Symbol symbol)
        {
            for (int col = 0; col < this.board.Columns; col++)
            {
                Assert.AreEqual(Symbol.None, this.board.GetRowSymbol(0));
                this.board.PlaceSymbol(new Position(0, col), symbol);
            }

            Assert.AreEqual(symbol, this.board.GetRowSymbol(0));
        }

        [TestCase(Symbol.X)]
        [TestCase(Symbol.O)]
        public void GetColumnSymbolMethod_ShouldWorkCorrectly(Symbol symbol)
        {
            for (int row = 0; row < this.board.Rows; row++)
            {
                Assert.AreEqual(Symbol.None, this.board.GetColumnSymbol(1));
                this.board.PlaceSymbol(new Position(row, 1), symbol);
            }

            Assert.AreEqual(symbol, this.board.GetColumnSymbol(1));
        }

        [TestCase(Symbol.X)]
        [TestCase(Symbol.O)]
        public void GetDiagonalRightToLeftSymbolMethod_ShouldWorkProperly(Symbol symbol)
        {
            for (int row = 0; row < this.board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    if (row == col)
                    {
                        Assert.AreEqual(Symbol.None, this.board.GetDiagonalRightToLeftSymbol());
                        this.board.PlaceSymbol(new Position(row, col), symbol);
                    }
                }
            }

            Assert.AreEqual(symbol, this.board.GetDiagonalRightToLeftSymbol());
        }

        [TestCase(Symbol.X)]
        [TestCase(Symbol.O)]
        public void GetDiagonalLeftToRightSymbolMethod_ShouldWorkProperly(Symbol symbol)
        {
            for (int row = 0; row < this.board.Rows; row++)
            {
                for (int col = 0; col < this.board.Columns; col++)
                {
                    if (Math.Abs(row - col) == 0 || Math.Abs(row - col) == 2)
                    {
                        Assert.AreEqual(Symbol.None, this.board.GetDiagonalLeftToRightSymbol());
                        this.board.PlaceSymbol(new Position(row, col), symbol);

                        if (row == 2 && col == 0)
                        {
                            break;
                        }
                    }
                }
            }

            Assert.AreEqual(symbol, this.board.GetDiagonalLeftToRightSymbol());
        }

        [Test]
        public void GetEmptyPositionsMethod_ShouldWorkCorrectlyWhenTheWholeBoardIsEmpty()
        {
            Assert.AreEqual(9, this.board.GetEmptyPositions().Count());
        }

        [Test]
        public void GetEmptyPositionsMethod_ShouldReturnCorrectNumberOfEmptyPositions()
        {
            for (int row = 0; row < this.board.Rows - 1; row++)
            {
                for (int col = 0; col < this.board.Columns; col++)
                {
                    this.board.PlaceSymbol(new Position(row, col), Symbol.X);
                }
            }

            Assert.AreEqual(3, this.board.GetEmptyPositions().Count());
        }

        [Test]
        public void IsFullMethod_ShouldReturnTrueWhenBoardIsFull()
        {
            Assert.False(this.board.IsFull());

            for (int row = 0; row < this.board.Rows; row++)
            {
                for (int col = 0; col < this.board.Columns; col++)
                {
                    Assert.False(this.board.IsFull());
                    this.board.PlaceSymbol(new Position(row, col), Symbol.X);
                }
            }

            Assert.True(this.board.IsFull());
        }

        [Test]
        public void PlaceSymbolMethod_ShouldWorkProperly()
        {
            Position position = new Position(0, 0);
            Assert.AreEqual("Row: 0, Column: 0", position.ToString());
            this.board.PlaceSymbol(position, Symbol.X);
            Assert.AreEqual(Symbol.X, this.board.SymbolsOnBoard[0, 0]);
        }

        [TestCase(-1, 4)]
        [TestCase(1, -6)]
        [TestCase(-89, -65)]
        public void PlaceSymbolMethod_ShouldThrowAnExceptionWhenAnIndexIsOutOfRange(int row, int col)
        {
            IndexOutOfRangeException exception = Assert.Throws<IndexOutOfRangeException>(()
                => this.board.PlaceSymbol(new Position(row, col), Symbol.X));

            Assert.AreEqual("Index is out of range.", exception.Message);
        }

        [Test]
        public void ToStringMethod_ShouldWorkCorrectly()
        {
            for (int col = 0; col < this.board.Columns; col++)
            {
                this.board.PlaceSymbol(new Position(0, col), Symbol.X);
            }

            for (int col = 0; col < this.board.Columns; col++)
            {
                this.board.PlaceSymbol(new Position(1, col), Symbol.O);
            }

            string expectedResult = $"XXX{Environment.NewLine}OOO{Environment.NewLine}---";
            string actualResult = this.board.ToString();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
