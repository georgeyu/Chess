using System.Diagnostics;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a king.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class King : IPiece
    {
        private const int Range = 1;
        private const string FenWhite = "K";
        private const string FenBlack = "k";

        public King(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }
        
        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateEmptyMoves()
        {
            var moves = ActionGenerator.GenerateAllMoves(Range);
            return moves;
        }

        public CaptureRelative[] GenerateCaptures()
        {
            var captures = ActionGenerator.GenerateAllCaptures(Range);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }
    }
}
