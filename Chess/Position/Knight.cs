using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Knight : Piece
    {
        public Knight(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            throw new NotImplementedException();
        }

        public Capture[] GetCaptures()
        {
            throw new NotImplementedException();
        }
    }
}
