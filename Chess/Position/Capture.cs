using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
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
