using Tic_Tac_Toe.GameBoard;
using Tic_Tac_Toe.GameLogic;
using Tic_Tac_Toe.Players;
using Tic_Tac_Toe.Players.Contracts;

namespace Tic_Tac_Toe_Tests
{
    [TestFixture]
    public class PlayersTests
    {
        [Test]
        public void AiPlayerPlayMethod_ShouldReturnCorrectInitialPosition()
        {
            IPlayer player = new AiPlayer();
            TicTacToeGame game = new TicTacToeGame(player, new RandomPlayer());
            GameResult gameResult = game.Play();

            Assert.AreEqual(Symbol.X, gameResult.Board.SymbolsOnBoard[0,0]);
        }

        [Test]
        public void AiPlayer_ShouldAlwaysDefeatRandomPlayerWhenStartsFirst()
        {
            for (int i = 0; i < 10; i++)
            {
                TicTacToeGame game = new TicTacToeGame(new AiPlayer(), new RandomPlayer());
                GameResult gameResult = game.Play();

                Assert.AreEqual(Symbol.X, gameResult.Winner);
            }
        }

        [Test]
        public void AiPlayerVsAiPlayerGame_ShouldAlwaysBeDraw()
        {
            for (int i = 0; i < 10; i++)
            {
                TicTacToeGame game = new TicTacToeGame(new AiPlayer(), new AiPlayer());
                GameResult gameResult = game.Play();

                Assert.AreEqual(Symbol.None, gameResult.Winner);
            }
        }
    }
}
