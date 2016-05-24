using System.Collections.Generic;
using System.Diagnostics;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a pawn.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Pawn : IPiece
    {
        private const int unmovedDisplacement = 2;
        private const string FenWhite = "P";
        private const string FenBlack = "p";
        private readonly int direction;

        public Pawn(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
            direction = isWhite ? 1 : -1;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateEmptyMoves()
        {
            var moveList = new List<SquareChange[]>();
            var defaultSquareChange = new SquareChange(0, direction);
            SquareChange[] defaultMove = { defaultSquareChange };
            moveList.Add(defaultMove);
            if (!HasMoved)
            {
                var unmovedSquareChange = new SquareChange(0, direction * unmovedDisplacement);
                SquareChange[] unmovedMove = { defaultSquareChange, unmovedSquareChange };
                moveList.Add(unmovedMove);
            }
            var moveArray = moveList.ToArray();
            return moveArray;
        }

        public CaptureRelative[] GenerateCaptures()
        {
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
    }
}
