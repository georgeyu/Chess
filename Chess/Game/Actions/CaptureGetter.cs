using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Actions
{
    /// <summary>
    /// Gets all legal captures from a position.
    /// </summary>
    internal static class CaptureGetter
    {
        /// <summary>
        /// Gets all legal captures based on whose turn it is.
        /// </summary>
        /// <param name="position">The position to get captures from.</param>
        /// <returns>Absolute captures.</returns>
        public static CaptureAbsolute[] GetCaptures(Position position)
        {
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            var capturesIgnoringLegality = ActionGetterUtil.GetActionsIgnoringLegality(
                position.Board,
                position.IsWhiteTurn,
                files,
                ranks,
                GetCapturesFromSquare);
            var capturesStayingOnBoard = GetCapturesStayingOnBoard(capturesIgnoringLegality, files, ranks);
            var capturesWithEmptyPassingSquares = GetCapturesWithEmptyPassingSquares(
                capturesStayingOnBoard,
                position.Board);
            var capturesWhereFinalSquareIsEnemyPiece = GetCapturesWhereCaptureSquareIsEnemyPiece(
                capturesWithEmptyPassingSquares,
                position.Board);
            var captures = capturesWhereFinalSquareIsEnemyPiece.ToArray();
            return captures;
        }

        private static CaptureAbsolute[] GetCapturesFromSquare(Square square, int file, int rank, bool isWhiteTurn)
        {
            var startSquare = new SquareAbsolute(file, rank);
            var captures = ActionGetterUtil.GetActionsFromSquare(
                square,
                file,
                rank,
                isWhiteTurn,
                x => new CaptureAbsolute(
                    startSquare,
                    new SquareAbsolute(file + x.CaptureSquare.FileChange, rank + x.CaptureSquare.RankChange),
                    x.PassingSquares.Select(
                        y => new SquareAbsolute(file + y.FileChange, rank + y.RankChange)).ToArray()),
                x => x.GenerateCaptures());
            return captures;
        }

        private static IEnumerable<CaptureAbsolute> GetCapturesStayingOnBoard(CaptureAbsolute[] captures, int files, int ranks)
        {
            var capturesStayingOnBoard = captures.Where(
                x =>
                    ActionGetterUtil.IsSquareOnBoard(x.CaptureSquare, files, ranks) &&
                    (
                        (x.PassingSquares.Length == 0) ||
                        ActionGetterUtil.AreSquaresOnBoard(x.PassingSquares, files, ranks)
                    ));
            return capturesStayingOnBoard;
        }

        private static IEnumerable<CaptureAbsolute> GetCapturesWithEmptyPassingSquares(
            IEnumerable<CaptureAbsolute> captures,
            Square[,] board)
        {
            var capturesWithEmptyPassingSquares = captures.Where(
                x => ActionGetterUtil.ArePassingSquaresEmpty(x.PassingSquares, board));
            return capturesWithEmptyPassingSquares;
        }

        private static IEnumerable<CaptureAbsolute> GetCapturesWhereCaptureSquareIsEnemyPiece(
            IEnumerable<CaptureAbsolute> captures,
            Square[,] board)
        {
            var capturesWhereCaptureSquareIsEnemyPiece = captures.Where(x => IsCaptureSquareEnemyPiece(x, board));
            return capturesWhereCaptureSquareIsEnemyPiece;
        }

        private static bool IsCaptureSquareEnemyPiece(CaptureAbsolute capture, Square[,] board)
        {
            if (board[capture.CaptureSquare.File, capture.CaptureSquare.Rank] is EmptySquare)
            {
                return false;
            }
            var piece = board[capture.StartSquare.File, capture.StartSquare.Rank] as Piece;
            var pieceToCapture = board[capture.CaptureSquare.File, capture.CaptureSquare.Rank] as Piece;
            return piece.IsWhite != pieceToCapture.IsWhite;
        }
    }
}
