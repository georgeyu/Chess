using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    internal interface Piece : Square
    {
        bool IsWhite { get; }

        bool HasMoved { get; set; }

        SquareChange[][] GetMoves();

        CaptureRelative[] GetCaptures();
    }
}
