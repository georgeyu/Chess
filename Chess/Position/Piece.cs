using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    internal interface Piece
    {
        bool IsWhite { get; }

        bool HasMoved { get; }

        SquareChange[][] GetMoves();

        Capture[] GetCaptures();
    }
}
