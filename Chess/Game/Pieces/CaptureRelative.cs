using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a capture relative to a starting square.
    /// </summary>
    internal class CaptureRelative
    {
        public CaptureRelative(SquareChange captureSquare, SquareChange[] passingSquares)
        {
            CaptureSquare = captureSquare;
            PassingSquares = passingSquares;
        }

        /// <summary>
        /// The capture square.
        /// </summary>
        public SquareChange CaptureSquare { get; private set; }

        /// <summary>
        /// The passing squares.
        /// </summary>
        public SquareChange[] PassingSquares { get; private set; }
    }
}
