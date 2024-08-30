namespace Tetris.Contracts
{
    public interface IScoreManager
    {
        int HighScore { get; }
        int Score { get; }

        void AddToHighScore(int score);
        void AddToScore(int level, int? lines);
    }
}