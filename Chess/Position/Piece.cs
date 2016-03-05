using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    internal interface Piece
    {
        bool IsWhite { get; set; }

        bool HasMoved { get; set; }

        SquareChange[][] GetMoves();

        Capture[] GetCaptures();
    }
}
