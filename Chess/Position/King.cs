﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class King : Piece
    {
        private const int KingRange = 1;

        public King(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            SquareChange[][] moves = GetActions(MoveCreator.GetHorizontalVerticalMoves, MoveCreator.GetDiagonalMoves);
            return moves;
        }

        public Capture[] GetCaptures()
        {
            Capture[] captures = GetActions(MoveCreator.GetHorizontalVerticalCaptures, MoveCreator.GetDiagonalCaptures);
            return captures;
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
