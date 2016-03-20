using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
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

        public SquareChange[][] GetMoves()
        {
            SquareChange[][] moves = MoveCreator.GetHorizontalVerticalMoves(Constants.MaxMoveRange);
            return moves;
        }

        public Capture[] GetCaptures()
        {
            Capture[] captures = MoveCreator.GetHorizontalVerticalCaptures(Constants.MaxMoveRange);
            return captures;
        }
    }
}
