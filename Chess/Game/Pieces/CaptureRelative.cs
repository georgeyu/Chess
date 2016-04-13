using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    // For both captures and moves, the passing squares must be unoccupied. Captures differ from moves in that the final
    // square for a capture must be occupied by a piece of the opposite color.
    internal class CaptureRelative
    {
        public CaptureRelative(SquareRelative finalSquare, SquareRelative[] passingSquares)
        {
            FinalSquare = finalSquare;
            PassingSquares = passingSquares;
        }

        public SquareRelative FinalSquare { get; private set; }

        public SquareRelative[] PassingSquares { get; private set; }
    }
}
