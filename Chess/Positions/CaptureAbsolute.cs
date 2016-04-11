using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions
{
    internal class CaptureAbsolute
    {
        public CaptureAbsolute(SquareAbsolute startSquare, SquareAbsolute finalSquare, SquareAbsolute[] passingSquares)
        {
            StartSquare = startSquare;
            FinalSquare = finalSquare;
            PassingSquares = passingSquares;
        }

        public SquareAbsolute StartSquare { get; private set; }

        public SquareAbsolute FinalSquare { get; private set; }

        public SquareAbsolute[] PassingSquares { get; private set; }
    }
}
