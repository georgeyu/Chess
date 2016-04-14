using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Bishop : Piece
    {
        private const string FenWhite = "B";
        private const string FenBlack = "b";

        public Bishop(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            SquareChange[][] moves = ActionGenerator.GenerateDiagonalMoves(Constants.BoardDimension - 1);
            return moves;
        }

        public CaptureRelative[] GetCaptures()
        {
            CaptureRelative[] captures = ActionGenerator.GenerateDiagonalCaptures(Constants.BoardDimension - 1);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }
    }
}
