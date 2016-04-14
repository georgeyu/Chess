using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Queen : Piece
    {
        private const string FenWhite = "Q";
        private const string FenBlack = "q";

        public Queen(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            SquareChange[][] moves = GetActions(ActionGenerator.GenerateStraightMoves, ActionGenerator.GenerateDiagonalMoves);
            return moves;
        }

        public CaptureRelative[] GetCaptures()
        {
            CaptureRelative[] captures = GetActions(ActionGenerator.GenerateStraightCaptures, ActionGenerator.GenerateDiagonalCaptures);
            return captures;
        }

        public string GetFen()
        {
            return IsWhite ? FenWhite : FenBlack;
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
