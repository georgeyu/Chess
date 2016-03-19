﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    internal class Queen : Piece
    {
        public Queen(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            throw new NotImplementedException();
        }

        public Capture[] GetCaptures()
        {
            throw new NotImplementedException();
        }
    }
}
