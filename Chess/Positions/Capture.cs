using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions
{
    // For both captures and moves, the passing squares must be unoccupied. Captures differ from moves in that the final
    // square for a capture must be occupied by a piece of the opposite color.
    internal class Capture
    {
        public Capture(SquareChange finalSquare, SquareChange[] passingSquares)
        {
            FinalSquare = finalSquare;
            PassingSquares = passingSquares;
        }

        public SquareChange FinalSquare { get; private set; }

        public SquareChange[] PassingSquares { get; private set; }
    }
}
