using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    /// <summary>
    /// Common logic used to get all legal actions.
    /// </summary>
    internal class MovesUtil
    {
        /// <summary>
        /// Gets all actions a single square can take.
        /// </summary>
        /// <typeparam name="T">The absolute action.</typeparam>
        /// <typeparam name="U">The relative action.</typeparam>
        /// <param name="square">The square to get actions from.</param>
        /// <param name="file">The number of files.</param>
        /// <param name="rank">The number of ranks.</param>
        /// <param name="isWhiteTurn">Whose turn it is.</param>
        /// <param name="getAbsoluteFromRelative">Function for converting a relative action into an absolute action.</param>
        /// <param name="getActions">Function for getting all relative actions from a piece.</param>
        /// <returns>Absolute actions.</returns>
        public static List<IMove> GetActionsFromSquare<T>(
            ISquare square,
            int file,
            int rank,
            bool isWhiteTurn,
            Func<T, IMove> getAbsoluteFromRelative,
            Func<IPiece, T[]> getActions)
        {
            var actionAbsoluteList = new List<IMove>();
            var isEmpty = square is EmptySquare;
            if (isEmpty)
            {
                return actionAbsoluteList;
            }
            var piece = square as IPiece;
            if (piece == null)
            {
                throw new NotImplementedException("Unable to cast Square as Piece.");
            }
            if (piece.IsWhite != isWhiteTurn)
            {
                return actionAbsoluteList;
            }
            T[] actionsRelative = getActions(piece);
            var startSquare = new SquareAbsolute(file, rank);
            foreach (var actionRelative in actionsRelative)
            {
                IMove actionAbsolute = getAbsoluteFromRelative(actionRelative);
                actionAbsoluteList.Add(actionAbsolute);
            }
            return actionAbsoluteList;
        }

        /// <summary>
        /// Checks whether the square is on the board.
        /// </summary>
        /// <param name="square">The square to check.</param>
        /// <param name="files">The number of files.</param>
        /// <param name="ranks">The number of ranks.</param>
        /// <returns>Whether the square is on the board.</returns>
        public static bool IsSquareOnBoard(SquareAbsolute square, int files, int ranks)
        {
            return
                (square.File < files) &&
                (square.File >= 0) &&
                (square.Rank < ranks) &&
                (square.Rank >= 0);
        }

        /// <summary>
        /// Checks whether all squares are on the board.
        /// </summary>
        /// <param name="squares">The squares to check.</param>
        /// <param name="files">The number of files.</param>
        /// <param name="ranks">The number of ranks.</param>
        /// <returns>Whether all squares are on the board.</returns>
        public static bool AreSquaresOnBoard(List<SquareAbsolute> squares, int files, int ranks)
        {
            var isSquareOnBoard = squares.Select(x => IsSquareOnBoard(x, files, ranks));
            bool areSquaresOnBoard = isSquareOnBoard.Aggregate((x, y) => x && y);
            return areSquaresOnBoard;
        }

        /// <summary>
        /// Checks whether all passing squares are empty.
        /// </summary>
        /// <param name="passingSquares">The passing squares to check.</param>
        /// <param name="board">The board to check for empty squares.</param>
        /// <returns>Whether all passing squares are empty.</returns>
        public static bool ArePassingSquaresEmpty(List<SquareAbsolute> passingSquares, ISquare[,] board)
        {
            if (passingSquares == null)
            {
                return true;
            }
            if (passingSquares.Count == 0)
            {
                return true;
            }
            var isPassingSquareEmpty = passingSquares.Select(x => board[x.File, x.Rank] is EmptySquare);
            var arePassingSquaresEmpty = isPassingSquareEmpty.Aggregate((x, y) => x && y);
            return arePassingSquaresEmpty;
        }
    }
}
