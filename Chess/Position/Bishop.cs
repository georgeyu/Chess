using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Bishop : Piece
    {
        public Bishop(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            SquareChange[][] moves = MoveCreator.GetDiagonalMoves(Constants.BoardDimension - 1);
            return moves;
        }

        public Capture[] GetCaptures()
        {
            Capture[] captures = MoveCreator.GetDiagonalCaptures(Constants.BoardDimension - 1);
            return captures;
        }
    }
}
