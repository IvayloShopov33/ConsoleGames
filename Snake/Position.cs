﻿namespace SnakeGame
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }
    }
}