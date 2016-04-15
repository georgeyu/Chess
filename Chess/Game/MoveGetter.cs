using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    /// <summary>
    /// Utility that gets legal moves from a position.
    /// </summary>
    internal static class MoveGetter
    {
        /// <summary>
        /// Get legal moves.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Legal moves.</returns>
        public static MoveAbsolute[] GetMoves(Position position)
        {
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            var movesIgnoringLegality = GetMovesIgnoringLegality(position.Board, position.IsWhiteTurn, files, ranks);
            var movesStayingOnBoard = GetMovesStayingOnBoard(movesIgnoringLegality, files, ranks);
            var movesWithEmptyPassingSquares = GetMovesWithEmptyPassingSquares(movesStayingOnBoard, position.Board);
            var moves = movesWithEmptyPassingSquares.ToArray();
            return moves;
        }

        /// <summary>
        /// Get both legal and illegal moves.
        /// </summary>
        /// <param name="board">The board to get moves from.</param>
        /// <param name="isWhiteTurn">Whose turn it is.</param>
        /// <param name="files">The number of files.</param>
        /// <param name="ranks">The number of ranks.</param>
        /// <returns>Legal and illegal moves.</returns>
        private static List<MoveAbsolute> GetMovesIgnoringLegality(
            Square[,] board,
            bool isWhiteTurn,
            int files,
            int ranks)
        {
            var moves = new List<MoveAbsolute>();
            for (var i = 0; i < files; i++)
            {
                for (var j = 0; j < ranks; j++)
                {
                    var movesFromSquare = GetMovesFromSquare(board[i, j], i, j, isWhiteTurn);
                    moves.AddRange(movesFromSquare);
                }
            }
            return moves;
        }

        /// <summary>
        /// Get both legal and illegal moves.
        /// </summary>
        /// <param name="square">The square to get moves from.</param>
        /// <param name="file">The number of files.</param>
        /// <param name="rank">The number of ranks.</param>
        /// <param name="isWhiteTurn">Whose turn it is.</param>
        /// <returns>Legal and illegal moves.</returns>
        private static List<MoveAbsolute> GetMovesFromSquare(Square square, int file, int rank, bool isWhiteTurn)
        {
            var movesAbsolute = new List<MoveAbsolute>();
            var isEmpty = square is EmptySquare;
            if (isEmpty)
            {
                return movesAbsolute;
            }
            var piece = square as Piece;
            if (piece == null)
            {
                throw new NotImplementedException("Unable to cast Square as Piece");
            }
            if (piece.IsWhite != isWhiteTurn)
            {
                return movesAbsolute;
            }
            var movesRelative = piece.GenerateMoves();
            var startSquare = new SquareAbsolute(file, rank);
            foreach (var moveRelative in movesRelative)
            {
                var squareAbsoluteEnumerable = moveRelative.Select(
                    x => new SquareAbsolute(file + x.FileChange, rank + x.RankChange));
                var squareAbsoluteArray = squareAbsoluteEnumerable.ToArray();
                var moveAbsolute = new MoveAbsolute(startSquare, squareAbsoluteArray);
                movesAbsolute.Add(moveAbsolute);
            }
            return movesAbsolute;
        }

        /// <summary>
        /// Get moves that stay on the board.
        /// </summary>
        /// <param name="moves">The moves to check.</param>
        /// <param name="files">The number of files.</param>
        /// <param name="ranks">The number of ranks.</param>
        /// <returns>Only moves that stay on the board.</returns>
        private static List<MoveAbsolute> GetMovesStayingOnBoard(List<MoveAbsolute> moves, int files, int ranks)
        {
            var movesStayingOnBoard = new List<MoveAbsolute>();
            foreach (MoveAbsolute move in moves)
            {
                var isSquareOnBoard = move.PassingSquares.Select(x => (
                    (x.File < files) &&
                    (x.Rank < ranks) &&
                    (x.File >= 0) &&
                    (x.Rank >= 0)));
                bool areAllSquaresOnBoard = isSquareOnBoard.Aggregate((x, y) => x && y);
                if (areAllSquaresOnBoard)
                {
                    movesStayingOnBoard.Add(move);
                }
            }
            return movesStayingOnBoard;
        }

        /// <summary>
        /// Get moves with empty passing squares.
        /// </summary>
        /// <param name="moves">The moves to check.</param>
        /// <param name="board">The board to check for empty squares.</param>
        /// <returns>Only moves with empty passing squares.</returns>
        private static List<MoveAbsolute> GetMovesWithEmptyPassingSquares(List<MoveAbsolute> moves, Square[,] board)
        {
            var movesWithEmptyPassingSquares = new List<MoveAbsolute>();
            foreach (MoveAbsolute move in moves)
            {
                var isPassingSquareEmpty = move.PassingSquares.Select(x => board[x.File, x.Rank] is EmptySquare);
                var areAllPassingSquaresEmpty = isPassingSquareEmpty.Aggregate((x, y) => x && y);
                if (areAllPassingSquaresEmpty)
                {
                    movesWithEmptyPassingSquares.Add(move);
                }
            }
            return movesWithEmptyPassingSquares;
        }
    }
}
