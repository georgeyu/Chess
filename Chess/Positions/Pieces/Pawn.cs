using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Pawn : Piece
    {
        private const int SpecialChange = 2;

        public Pawn(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareRelative[][] GetMoves()
        {
            var movesList = new List<SquareRelative[]>();
            int direction = GetDirection();
            var defaultSquareChange = new SquareRelative(0, direction);
            SquareRelative[] defaultMove = { defaultSquareChange };
            movesList.Add(defaultMove);
            if (!HasMoved)
            {
                var specialSquareChange = new SquareRelative(0, direction * SpecialChange);
                SquareRelative[] specialMove = { defaultSquareChange, specialSquareChange };
                movesList.Add(specialMove);
            }
            var movesArray = movesList.ToArray();
            return movesArray;
        }

        public CaptureRelative[] GetCaptures()
        {
            int direction = GetDirection();
            var leftSquareChange = new SquareRelative(-1, direction);
            var rightSquareChange = new SquareRelative(1, direction);
            CaptureRelative leftCapture = new CaptureRelative(leftSquareChange, new SquareRelative[] { });
            CaptureRelative rightCapture = new CaptureRelative(rightSquareChange, new SquareRelative[] { });
            CaptureRelative[] captures = { leftCapture, rightCapture };
            return captures;
        }

        private int GetDirection()
        {
            return IsWhite ? 1 : -1;
        }
    }
}
