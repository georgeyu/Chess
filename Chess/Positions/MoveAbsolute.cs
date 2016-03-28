﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions
{
    internal class MoveAbsolute
    {
        public MoveAbsolute(SquareAbsolute startSquare, SquareAbsolute[] passingSquares)
        {
            StartSquare = startSquare;
            PassingSquares = passingSquares;
        }

        public SquareAbsolute StartSquare { get; private set; }

        public SquareAbsolute[] PassingSquares { get; private set; }
    }
}
