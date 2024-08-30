using Tetris.Contracts;

namespace Tetris
{
    public class TetrisGameManager
    {
        private readonly ITetrisGame game;
        private readonly IInputHandler inputHandler;
        private readonly ITetrisWriter tetrisConsoleWriter;
        private readonly IScoreManager scoreManager;

        public TetrisGameManager(ITetrisGame game, IInputHandler inputHandler, ITetrisWriter tetrisConsoleWriter, IScoreManager scoreManager)
        {
            this.game = game;
            this.inputHandler = inputHandler;
            this.tetrisConsoleWriter = tetrisConsoleWriter;
            this.scoreManager = scoreManager;
        }

        public void MainLoop()
        {
            while (true)
            {
                this.tetrisConsoleWriter.Frame++;
                this.game.UpdateLevel(this.scoreManager.Score);

                //read input
                var input = this.inputHandler.GetInput();
                switch (input)
                {
                    case Enums.TetrisGameInput.Left:
                        if (this.game.CanMoveToLeft())
                        {
                            this.game.CurrentFigureCol--;
                        }
                        break;
                    case Enums.TetrisGameInput.Right:
                        if (this.game.CanMoveToRight())
                        {
                            this.game.CurrentFigureCol++;
                        }
                        break;
                    case Enums.TetrisGameInput.Down:
                        this.tetrisConsoleWriter.Frame = 1;
                        this.game.CurrentFigureRow++;

                        this.scoreManager.AddToScore(this.game.Level, null);
                        break;
                    case Enums.TetrisGameInput.Rotate:
                        var newFigure = this.game.CurrentFigure.Rotate();

                        if (!this.game.Collision(newFigure))
                        {
                            this.game.CurrentFigure = newFigure;
                        }
                        break;
                    case Enums.TetrisGameInput.Exit:
                        this.scoreManager.AddToHighScore(this.scoreManager.Score);
                        this.tetrisConsoleWriter.GameOver(this.scoreManager.Score);
                        break;

                }

                if (this.tetrisConsoleWriter.Frame % (this.tetrisConsoleWriter.FramesToMoveFigure - this.game.Level) == 0)
                {
                    this.game.CurrentFigureRow++;
                    this.tetrisConsoleWriter.Frame = 0;
                }

                if (this.game.Collision(this.game.CurrentFigure))
                {
                    this.game.AddCurrentFigureToTetrisField();
                    int lines = this.game.CheckForFullLines();
                    this.scoreManager.AddToScore(this.game.Level, lines);

                    this.game.NewRandomFigure();

                    if (this.game.Collision(this.game.CurrentFigure))
                    {
                        this.scoreManager.AddToHighScore(this.scoreManager.Score);
                        this.tetrisConsoleWriter.GameOver(this.scoreManager.Score);
                    }
                }

                //redraw UI
                this.tetrisConsoleWriter.DrawBorder(this.tetrisConsoleWriter.TetrisRows, this.tetrisConsoleWriter.TetrisColumns, this.tetrisConsoleWriter.InfoColumns);
                this.tetrisConsoleWriter.DrawGameState(this.tetrisConsoleWriter.TetrisColumns, this.game, this.scoreManager);
                this.tetrisConsoleWriter.DrawTetrisField(this.game.TetrisField);
                this.tetrisConsoleWriter.DrawCurrentFigure(this.game.CurrentFigure, this.game.CurrentFigureRow, this.game.CurrentFigureCol);

                Thread.Sleep(40);
            }
        }
    }
}