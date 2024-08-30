using System.Text.RegularExpressions;
using Tetris.Contracts;

namespace Tetris
{
    public class ScoreManager : IScoreManager
    {
        private readonly string scoreFile;

        private readonly int[] ScorePerLines = { 0, 40, 80, 160, 320, 640, 1280, 2560 };

        public ScoreManager(string scoreFile)
        {
            this.scoreFile = scoreFile;
            this.HighScore = this.GetHighScore();
        }

        public int Score { get; private set; }

        public int HighScore { get; private set; }

        public void AddToHighScore(int score)
        {
            File.AppendAllText("..\\..\\..\\scores.txt", $"[{DateTime.UtcNow.ToString()}] {Environment.UserName} -> {score} points.{Environment.NewLine}");
        }

        public void AddToScore(int level, int? lines)
        {
            if (lines.HasValue)
            {
                this.Score += this.ScorePerLines[lines.Value] * level;
            }
            else
            {
                this.Score += level;
            }

            if (this.Score > this.HighScore)
            {
                this.HighScore = this.Score;
            }
        }

        private int GetHighScore()
        {
            var highScore = 0;

            if (File.Exists(this.scoreFile))
            {
                var allScores = File.ReadAllLines(this.scoreFile);

                foreach (var score in allScores)
                {
                    var match = Regex.Match(score, @" -> (?<score>[0-9]+)");
                    highScore = Math.Max(highScore, int.Parse(match.Groups["score"].Value));
                }
            }

            return highScore;
        }
    }
}