using Tetris.Contracts;

namespace Tetris
{
    public static class Program
    {
        public static void Main()
        {
            var musicPlayer = new MusicPlayer();
            musicPlayer.PlayMusic();

            //Settings
            int TetrisRows = 20;
            int TetrisColumns = 14;
            int InfoColumns = 14;

            //Data of the Tetris game
            ITetrisGame game = new TetrisGame(TetrisRows, TetrisColumns);
            IScoreManager scoreManager = new ScoreManager("scores.txt");

            ITetrisWriter tetrisConsoleWriter = new TetrisConsoleWriter(TetrisRows, TetrisColumns, InfoColumns);
            IInputHandler inputHandler = new ConsoleInputHandler();

            tetrisConsoleWriter.DrawBorder(TetrisRows, TetrisColumns, InfoColumns);
            tetrisConsoleWriter.DrawGameState(TetrisColumns, game, scoreManager);

            new TetrisGameManager(game, inputHandler, tetrisConsoleWriter, scoreManager)
                .MainLoop();
        }
    }
}