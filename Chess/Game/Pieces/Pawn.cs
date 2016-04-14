using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Pawn : Piece
    {
        private const int SpecialChange = 2;
        private const string FenWhite = "P";
        private const string FenBlack = "p";

        public Pawn(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            var movesList = new List<SquareChange[]>();
            int direction = GetDirection();
            var defaultSquareChange = new SquareChange(0, direction);
            SquareChange[] defaultMove = { defaultSquareChange };
            movesList.Add(defaultMove);
            if (!HasMoved)
            {
                var specialSquareChange = new SquareChange(0, direction * SpecialChange);
                SquareChange[] specialMove = { defaultSquareChange, specialSquareChange };
                movesList.Add(specialMove);
            }
            var movesArray = movesList.ToArray();
            return movesArray;
        }

        public CaptureRelative[] GetCaptures()
        {
            int direction = GetDirection();
            var leftSquareChange = new SquareChange(-1, direction);
            var rightSquareChange = new SquareChange(1, direction);
            CaptureRelative leftCapture = new CaptureRelative(leftSquareChange, new SquareChange[] { });
            CaptureRelative rightCapture = new CaptureRelative(rightSquareChange, new SquareChange[] { });
            CaptureRelative[] captures = { leftCapture, rightCapture };
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }

        private int GetDirection()
        {
            return IsWhite ? 1 : -1;
        }
    }
}
