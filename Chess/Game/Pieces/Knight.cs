using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a knight.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Knight : Piece
    {
        private const int LongChange = 2;
        private const int ShortChange = 1;
        private const string FenWhite = "N";
        private const string FenBlack = "n";
        private int[] signs = { -1, 1 };

        public Knight(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateMoves()
        {
            var squares = GenerateSquares();
            var moveEnumerable = squares.Select(x => new SquareChange[] { x });
            var moveArray = moveEnumerable.ToArray();
            return moveArray;
        }

        public CaptureRelative[] GenerateCaptures()
        {
            var squares = GenerateSquares();
            var captureEnumerable = squares.Select(x => new CaptureRelative(x, new SquareChange[] { }));
            var captureArray = captureEnumerable.ToArray();
            return captureArray;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }

        /// <summary>
        /// Generate the squares the knight lands on.
        /// </summary>
        /// <returns>The squares the knight lands on.</returns>
        private SquareChange[] GenerateSquares()
        {
            var square0 = new SquareChange(LongChange, ShortChange);
            var square1 = new SquareChange(-LongChange, ShortChange);
            var square2 = new SquareChange(LongChange, -ShortChange);
            var square3 = new SquareChange(-LongChange, -ShortChange);
            var square4 = new SquareChange(ShortChange, LongChange);
            var square5 = new SquareChange(-ShortChange, LongChange);
            var square6 = new SquareChange(ShortChange, -LongChange);
            var square7 = new SquareChange(-ShortChange, -LongChange);
            SquareChange[] squares = { square0, square1, square2, square3, square4, square5, square6, square7 };
            return squares;
        }
    }
}
