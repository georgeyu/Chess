using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Actions
{
    internal class ActionGetterUtil
    {
        public static T[] GetActionsIgnoringLegality<T>(
            Square[,] board,
            bool isWhiteTurn,
            int files,
            int ranks,
            Func<Square, int, int, bool, T[]> getActionsFromSquare)
        {
            var actionList = new List<T>();
            for (var i = 0; i < files; i++)
            {
                for (var j = 0; j < ranks; j++)
                {
                    var actionsFromSquare = getActionsFromSquare(board[i, j], i, j, isWhiteTurn);
                    actionList.AddRange(actionsFromSquare);
                }
            }
            var actionArray = actionList.ToArray();
            return actionArray;
        }

        public static T[] GetActionsFromSquare<T, U>(
            Square square,
            int file,
            int rank,
            bool isWhiteTurn,
            Func<U, T> getAbsoluteFromRelative,
            Func<Piece, U[]> getActions)
        {
            var actionAbsoluteList = new List<T>();
            var actionAbsoluteArray = actionAbsoluteList.ToArray();
            var isEmpty = square is EmptySquare;
            if (isEmpty)
            {
                return actionAbsoluteArray;
            }
            var piece = square as Piece;
            if (piece == null)
            {
                throw new NotImplementedException("Unable to cast Square as Piece.");
            }
            if (piece.IsWhite != isWhiteTurn)
            {
                return actionAbsoluteArray;
            }
            U[] actionsRelative = getActions(piece);
            var startSquare = new SquareAbsolute(file, rank);
            foreach (var actionRelative in actionsRelative)
            {
                T actionAbsolute = getAbsoluteFromRelative(actionRelative);
                actionAbsoluteList.Add(actionAbsolute);
            }
            actionAbsoluteArray = actionAbsoluteList.ToArray();
            return actionAbsoluteArray;
        }

        public static bool IsSquareOnBoard(SquareAbsolute square, int files, int ranks)
        {
            return
                (square.File < files) &&
                (square.File >= 0) &&
                (square.Rank < ranks) &&
                (square.Rank >= 0);
        }

        public static bool AreSquaresOnBoard(SquareAbsolute[] squares, int files, int ranks)
        {
            var isSquareOnBoard = squares.Select(x => IsSquareOnBoard(x, files, ranks));
            bool areSquaresOnBoard = isSquareOnBoard.Aggregate((x, y) => x && y);
            return areSquaresOnBoard;
        }

        public static bool ArePassingSquaresEmpty(SquareAbsolute[] passingSquares, Square[,] board)
        {
            if (passingSquares.Length == 0)
            {
                return true;
            }
            var isPassingSquareEmpty = passingSquares.Select(x => board[x.File, x.Rank] is EmptySquare);
            var arePassingSquaresEmpty = isPassingSquareEmpty.Aggregate((x, y) => x && y);
            return arePassingSquaresEmpty;
        }
    }
}
