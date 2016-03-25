using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    internal interface Piece : Square
    {
        bool IsWhite { get; }

        bool HasMoved { get; }

        SquareChange[][] GetMoves();

        Capture[] GetCaptures();
    }
}
