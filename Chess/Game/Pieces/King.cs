﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class King : Piece
    {
        private const int KingRange = 1;
        private const string FenWhite = "K";
        private const string FenBlack = "k";

        public King(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareRelative[][] GetMoves()
        {
            SquareRelative[][] moves = GetActions(MoveCreator.GetHorizontalVerticalMoves, MoveCreator.GetDiagonalMoves);
            return moves;
        }

        public CaptureRelative[] GetCaptures()
        {
            CaptureRelative[] captures = GetActions(MoveCreator.GetHorizontalVerticalCaptures, MoveCreator.GetDiagonalCaptures);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
        }

        private T[] GetActions<T>(Func<int, T[]> getHorizontalVerticalActions, Func<int, T[]> getDiagonalActions)
        {
            T[] horizontalVerticalActions = getHorizontalVerticalActions(KingRange);
            T[] diagonalActions = getDiagonalActions(KingRange);
            IEnumerable<T> actionsEnumerable = horizontalVerticalActions.Concat(diagonalActions);
            var actionsArray = actionsEnumerable.ToArray();
            return actionsArray;
        }
    }
}