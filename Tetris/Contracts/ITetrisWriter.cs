namespace Tetris.Contracts
{
    public interface ITetrisWriter
    {
        int Frame { get; set; }
        int FramesToMoveFigure { get; }
        int InfoColumns { get; }
        int TetrisColumns { get; }
        int TetrisRows { get; }

        void DrawBorder(int tetrisRows, int tetrisColumns, int infoColumns);
        void DrawCurrentFigure(Tetromino currentFigure, int currentFigureRow, int currentFigureCol);
        void DrawGameState(int tetrisColumns, ITetrisGame state, IScoreManager scoreManager);
        void DrawTetrisField(bool[,] tetrisField);
        void GameOver(int score);
    }
}