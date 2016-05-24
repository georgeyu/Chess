namespace Chess.Game.Moves
{
    /// <summary>
    /// Represents a capture using absolute squares.
    /// </summary>
    internal class CaptureAbsolute
    {
        public CaptureAbsolute(SquareAbsolute startSquare, SquareAbsolute captureSquare, SquareAbsolute[] passingSquares)
        {
            StartSquare = startSquare;
            CaptureSquare = captureSquare;
            PassingSquares = passingSquares;
        }

        public SquareAbsolute StartSquare { get; private set; }

        public SquareAbsolute CaptureSquare { get; private set; }

        public SquareAbsolute[] PassingSquares { get; private set; }
    }
}
