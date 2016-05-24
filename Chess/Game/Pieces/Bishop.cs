using System.Diagnostics;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a bishop.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Bishop : IPiece
    {
        private const string FenWhite = "B";
        private const string FenBlack = "b";

        public Bishop(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateEmptyMoves()
        {
            SquareChange[][] moves = ActionGenerator.GenerateDiagonalMoves(Constants.BoardLength - 1);
            return moves;
        }

        public CaptureRelative[] GenerateCaptures()
        {
            CaptureRelative[] captures = ActionGenerator.GenerateDiagonalCaptures(Constants.BoardLength - 1);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }
    }
}
