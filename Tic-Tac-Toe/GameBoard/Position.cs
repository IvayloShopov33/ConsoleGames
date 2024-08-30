namespace Tic_Tac_Toe.GameBoard
{
    public class Position
    {
        public Position(string text)
        {
            string[] positionDetails = text.Split(",", StringSplitOptions.RemoveEmptyEntries);
            this.Row = int.Parse(positionDetails[0]);
            this.Column = int.Parse(positionDetails[1]);
        }

        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public override bool Equals(object? obj)
        {
            Position position = obj as Position;

            return position.Row == this.Row &&
                position.Column == this.Column;
        }

        public override string ToString()
        {
            return $"Row: {this.Row}, Column: {this.Column}";
        }
    }
}