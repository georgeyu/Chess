namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a piece.
    /// </summary>
    internal interface IPiece : ISquare
    {
        bool IsWhite { get; }

        bool HasMoved { get; set; }

        SquareChange[][] GenerateEmptyMoves();

        CaptureRelative[] GenerateCaptures();
    }
}
