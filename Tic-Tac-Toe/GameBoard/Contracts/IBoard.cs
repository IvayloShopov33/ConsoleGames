namespace Tic_Tac_Toe.GameBoard.Contracts
{
    public interface IBoard
    {
        int Rows {  get; }

        int Columns { get; }

        Symbol[,] SymbolsOnBoard { get; }

        bool IsFull();

        void PlaceSymbol(Position position, Symbol symbol);

        IEnumerable<Position> GetEmptyPositions();

        Symbol GetRowSymbol(int row);

        Symbol GetColumnSymbol(int column);

        Symbol GetDiagonalRightToLeftSymbol();

        Symbol GetDiagonalLeftToRightSymbol();
    }
}