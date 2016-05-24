namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a capture relative to a starting square.
    /// </summary>
    internal class CaptureRelative
    {
        public CaptureRelative(SquareChange captureSquare, SquareChange[] passingSquares)
        {
            CaptureSquare = captureSquare;
            PassingSquares = passingSquares;
        }

        public SquareChange CaptureSquare { get; private set; }

        public SquareChange[] PassingSquares { get; private set; }
    }
}
