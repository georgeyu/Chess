namespace Chess.Game.Moves
{
    /// <summary>
    /// Represents a move using absolute squares.
    /// </summary>
    internal class MoveAbsolute
    {
        public MoveAbsolute(SquareAbsolute startSquare, SquareAbsolute[] passingSquares)
        {
            StartSquare = startSquare;
            PassingSquares = passingSquares;
        }

        public SquareAbsolute StartSquare { get; private set; }

        public SquareAbsolute[] PassingSquares { get; private set; }
    }
}
