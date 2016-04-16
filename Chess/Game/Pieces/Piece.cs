using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a piece.
    /// </summary>
    internal interface Piece : Square
    {
        bool IsWhite { get; }

        bool HasMoved { get; set; }

        SquareChange[][] GenerateMoves();

        CaptureRelative[] GenerateCaptures();
    }
}
