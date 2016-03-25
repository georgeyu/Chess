using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position.Piece
{
    internal interface Piece
    {
        bool IsWhite { get; }

        bool HasMoved { get; }

        SquareChange[][] GetMoves();

        Capture[] GetCaptures();
    }
}
