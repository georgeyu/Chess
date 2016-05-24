﻿using System.Diagnostics;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Represents a rook.
    /// </summary>
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Rook : IPiece
    {
        private const string FenWhite = "R";
        private const string FenBlack = "r";

        public Rook(bool isWhite, bool hasMoved = false)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GenerateEmptyMoves()
        {
            SquareChange[][] moves = ActionGenerator.GenerateStraightMoves(Constants.BoardLength - 1);
            return moves;
        }

        public CaptureRelative[] GenerateCaptures()
        {
            CaptureRelative[] captures = ActionGenerator.GenerateStraightCaptures(Constants.BoardLength - 1);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }
    }
}
