using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Queen : Piece
    {
        public Queen(bool isWhite, bool hasMoved)
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

        private T[] GetActions<T>(Func<int, T[]> getHorizontalVerticalActions, Func<int, T[]> getDiagonalActions)
        {
            T[] horizontalVerticalActions = getHorizontalVerticalActions(Constants.BoardDimension - 1);
            T[] diagonalActions = getDiagonalActions(Constants.BoardDimension - 1);
            IEnumerable<T> actionsEnumerable = horizontalVerticalActions.Concat(diagonalActions);
            var actionsArray = actionsEnumerable.ToArray();
            return actionsArray;
        }
    }
}
