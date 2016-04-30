using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Actions
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
            var movesIgnoringLegality = ActionGetterUtil.GetActionsIgnoringLegality(
                position.Board,
                position.IsWhiteTurn,
                files,
                ranks,
                GetMovesFromSquare);
            var movesStayingOnBoard = GetMovesStayingOnBoard(movesIgnoringLegality, files, ranks);
            var movesWithEmptyPassingSquares = GetMovesWithEmptyPassingSquares(movesStayingOnBoard, position.Board);
            var movesWithSafeKing = movesWithEmptyPassingSquares.Where(
                x => KingSafetyChecker.IsKingSafe(position, x));
            var moves = movesWithSafeKing.ToArray();
            return moves;
        }

        private static MoveAbsolute[] GetMovesFromSquare(Square square, int file, int rank, bool isWhiteTurn)
        {
            var startSquare = new SquareAbsolute(file, rank);
            var moves = ActionGetterUtil.GetActionsFromSquare(
                square,
                file,
                rank,
                isWhiteTurn,
                x => new MoveAbsolute(
                    startSquare,
                    x.Select(y => new SquareAbsolute(file + y.FileChange, rank + y.RankChange)).ToArray()),
                x => x.GenerateMoves());
            return moves;
        }

        private static IEnumerable<MoveAbsolute> GetMovesStayingOnBoard(MoveAbsolute[] moves, int files, int ranks)
        {
            var movesStayingOnBoard = moves.Where(
                x => ActionGetterUtil.AreSquaresOnBoard(x.PassingSquares, files, ranks));
            return movesStayingOnBoard;
        }

        private static IEnumerable<MoveAbsolute> GetMovesWithEmptyPassingSquares(
            IEnumerable<MoveAbsolute> moves,
            Square[,] board)
        {
            var movesWithEmptyPassingSquares = moves.Where(
                x => ActionGetterUtil.ArePassingSquaresEmpty(x.PassingSquares, board));
            return movesWithEmptyPassingSquares;
        }
    }
}
