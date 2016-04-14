using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a queen.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Queen : Piece
    {
        private const string FenWhite = "Q";
        private const string FenBlack = "q";

        public Queen(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateMoves()
        {
            var moves = ActionGenerator.GenerateAllMoves(Constants.BoardLength - 1);
            return moves;
        }

        public CaptureRelative[] GenerateCaptures()
        {
            var captures = ActionGenerator.GenerateAllCaptures(Constants.BoardLength - 1);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }
    }
}
