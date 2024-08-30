using Moq;
using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameBoard.Contracts;
using Tic_Tac_Toe.GameLogic;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe_Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void PlayMethod_ShouldReturnValidValueOfSymbol()
        {
            Mock<IPlayer> player = new Mock<IPlayer>();
            player.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return x.GetEmptyPositions().First();
                });

            TicTacToeGame game = new TicTacToeGame(player.Object, player.Object);

            Assert.AreEqual(Symbol.O, game.Play().Winner);
        }

        [Test]
        public void PlayMethod_GameShouldBeDraw()
        {
            int firstPlayerRow = 0;
            int firstPlayerCol = 0;
            int secondPlayerRow = 2;
            int secondPlayerCol = 1;
            int countOfMoves = 0;

            Mock<IPlayer> firstPlayer = new Mock<IPlayer>();
            firstPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    countOfMoves++;

                    if (countOfMoves < 5)
                    {
                        return new Position(firstPlayerRow, firstPlayerCol++);
                    }

                    if (countOfMoves == 5)
                    {
                        firstPlayerRow = 1;
                        firstPlayerCol = 1;

                        return new Position(firstPlayerRow, firstPlayerCol++);
                    }

                    if (countOfMoves == 7)
                    {
                        return new Position(firstPlayerRow, firstPlayerCol);
                    }

                    firstPlayerRow++;
                    firstPlayerCol = 0;

                    return new Position(firstPlayerRow, firstPlayerCol);
                });

            Mock<IPlayer> secondPlayer = new Mock<IPlayer>();
            secondPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    countOfMoves++;

                    if (countOfMoves < 5)
                    {
                        return new Position(secondPlayerRow, secondPlayerCol++);
                    }

                    if (countOfMoves == 6)
                    {
                        secondPlayerRow = 0;
                        secondPlayerCol--;

                        return new Position(secondPlayerRow, secondPlayerCol);
                    }

                    secondPlayerRow++;
                    secondPlayerCol = 0;

                    return new Position(1, 0);
                });

            TicTacToeGame game = new TicTacToeGame(firstPlayer.Object, secondPlayer.Object);
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.None, gameResult.Winner);
        }

        [Test]
        public void PlayMethod_ShouldReturnCorrectRowWinner()
        {
            int firstPlayerColumn = 0;
            int secondPlayerColumn = 0;

            Mock<IPlayer> firstPlayer = new Mock<IPlayer>();
            firstPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(0, firstPlayerColumn++);
                });

            Mock<IPlayer> secondPlayer = new Mock<IPlayer>();
            secondPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(1, secondPlayerColumn++);
                });

            TicTacToeGame game = new TicTacToeGame(firstPlayer.Object, secondPlayer.Object);
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.X, gameResult.Winner);
        }

        [Test]
        public void PlayMethod_ShouldReturnCorrectColumnWinner()
        {
            int firstPlayerRow = 0;
            int secondPlayerRow = 0;

            Mock<IPlayer> firstPlayer = new Mock<IPlayer>();
            firstPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(firstPlayerRow++, 1);
                });

            Mock<IPlayer> secondPlayer = new Mock<IPlayer>();
            secondPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(secondPlayerRow++, 0);
                });

            TicTacToeGame game = new TicTacToeGame(firstPlayer.Object, secondPlayer.Object);
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.X, gameResult.Winner);
        }

        [Test]
        public void PlayMethod_ShouldReturnCorrectDiagonalRightToLeftWinner()
        {
            int firstPlayerRow = 0;
            int firstPlayerCol = 0;
            int secondPlayerRow = 0;
            int secondPlayerCol = 1;

            Mock<IPlayer> firstPlayer = new Mock<IPlayer>();
            firstPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(firstPlayerRow++, firstPlayerCol++);
                });

            Mock<IPlayer> secondPlayer = new Mock<IPlayer>();
            secondPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(secondPlayerRow, secondPlayerCol++);
                });

            TicTacToeGame game = new TicTacToeGame(firstPlayer.Object, secondPlayer.Object);
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.X, gameResult.Winner);
        }

        [Test]
        public void PlayMethod_ShouldReturnCorrectDiagonalLeftToRightWinner()
        {
            int firstPlayerRow = 0;
            int firstPlayerCol = 2;
            int secondPlayerRow = 0;
            int secondPlayerCol = 0;

            Mock<IPlayer> firstPlayer = new Mock<IPlayer>();
            firstPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(firstPlayerRow++, firstPlayerCol--);
                });

            Mock<IPlayer> secondPlayer = new Mock<IPlayer>();
            secondPlayer.Setup(x => x.Play(It.IsAny<IBoard>(), It.IsAny<Symbol>()))
                .Returns((IBoard x, Symbol s) =>
                {
                    return new Position(secondPlayerRow, secondPlayerCol++);
                });

            TicTacToeGame game = new TicTacToeGame(firstPlayer.Object, secondPlayer.Object);
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.X, gameResult.Winner);
        }
    }
}
