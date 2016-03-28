using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Rook : Piece
    {
        public Rook(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareRelative[][] GetMoves()
        {
            SquareRelative[][] moves = MoveCreator.GetHorizontalVerticalMoves(Constants.BoardDimension - 1);
            return moves;
        }

        public CaptureRelative[] GetCaptures()
        {
            CaptureRelative[] captures = MoveCreator.GetHorizontalVerticalCaptures(Constants.BoardDimension - 1);
            return captures;
        }
    }
}
