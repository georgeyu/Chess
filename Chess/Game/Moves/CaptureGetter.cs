using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
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
        public static List<IMove> GetCaptures(Position position)
        {
            var capturesIgnoringKingSafety = GetCapturesIgnoringKingSafety(position);
            var capturesWithSafeKing = capturesIgnoringKingSafety.Where(x => KingSafetyChecker.IsKingSafe(position, x));
            var captures = capturesWithSafeKing.ToList();
            return captures;
        }

        /// <summary>
        /// Gets all captures ignoring whether the king can be captured next turn.
        /// </summary>
        /// <param name="position">The position to get captures from.</param>
        /// <returns>All captures ignoring whether the king can be captured next turn.</returns>
        public static List<IMove> GetCapturesIgnoringKingSafety(Position position)
        {
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            var moves = new List<IMove>();
            for (var i = 0; i < files; i++)
            {
                for (var j = 0; j < ranks; j++)
                {
                    ISquare square = position.Board[i, j];
                    var piece = square as IPiece;
                    if (piece == null)
                    {
                        continue;
                    }
                    if (piece.IsWhite != position.IsWhiteTurn)
                    {
                        continue;
                    }
                    var relativeCaptures = piece.GenerateCaptures();
                    var absoluteCaptures = relativeCaptures.Select(x => GetCaptureAbsoluteFromRelative(x, i, j));
                    var capturesStayingOnBoard = GetCapturesStayingOnBoard(absoluteCaptures.ToList(), files, ranks);
                    var allCaptures = capturesStayingOnBoard.Select(
                        x => GetCaptureFromCaptureAbsolute(x, piece.HasMoved, position.Board)).ToList();
                    var capturesWithEmptyPassingSquares = GetCapturesWithEmptyPassingSquares(allCaptures, position.Board);
                    var capturesWhereFinalSquareIsEnemyPiece = GetCapturesWhereCaptureSquareIsEnemyPiece(capturesWithEmptyPassingSquares, position.Board);
                    moves.AddRange(capturesWhereFinalSquareIsEnemyPiece);
                }
            }
            return moves;
        }

        private static CaptureAbsolute GetCaptureAbsoluteFromRelative(
            CaptureRelative captureRelative,
            int file,
            int rank)
        {
            var captureAbsolute = new CaptureAbsolute(
                new SquareAbsolute(file, rank),
                new SquareAbsolute(file + captureRelative.CaptureSquare.FileChange, rank + captureRelative.CaptureSquare.RankChange),
                captureRelative.PassingSquares.Select(x => new SquareAbsolute(file + x.FileChange, rank + x.RankChange)).ToArray());
            return captureAbsolute;
        }

        private static Capture GetCaptureFromCaptureAbsolute(
            CaptureAbsolute captureAbsolute,
            bool hasMoved,
            ISquare[,] board)
        {
            var squares = new List<SquareAbsolute>();
            squares.Add(captureAbsolute.StartSquare);
            squares.AddRange(captureAbsolute.PassingSquares);
            squares.Add(captureAbsolute.CaptureSquare);
            ISquare capturedSquare = board[captureAbsolute.CaptureSquare.File, captureAbsolute.CaptureSquare.Rank];
            var capturedPiece = capturedSquare as IPiece;
            var capture = new Capture(squares, hasMoved, capturedPiece);
            return capture;
        }

        private static List<CaptureAbsolute> GetCapturesStayingOnBoard(List<CaptureAbsolute> captures, int files, int ranks)
        {
            var capturesStayingOnBoard = captures.Where(
                x =>
                    MovesUtil.IsSquareOnBoard(x.CaptureSquare, files, ranks) &&
                    (
                        (x.PassingSquares.Length == 0) ||
                        MovesUtil.AreSquaresOnBoard(x.PassingSquares.ToList(), files, ranks)
                    ));
            return capturesStayingOnBoard.ToList();
        }

        private static List<Capture> GetCapturesWithEmptyPassingSquares(
            List<Capture> captures,
            ISquare[,] board)
        {
            var capturesWithEmptyPassingSquares = captures.Where(
                x => MovesUtil.ArePassingSquaresEmpty(x.PassingSquares, board)).ToList();
            return capturesWithEmptyPassingSquares;
        }

        private static List<Capture> GetCapturesWhereCaptureSquareIsEnemyPiece(
            List<Capture> captures,
            ISquare[,] board)
        {
            var capturesWhereCaptureSquareIsEnemyPiece = captures.Where(x => IsCaptureSquareEnemyPiece(x, board));
            return capturesWhereCaptureSquareIsEnemyPiece.ToList();
        }

        private static bool IsCaptureSquareEnemyPiece(Capture capture, ISquare[,] board)
        {
            if (board[capture.Squares.Last().File, capture.Squares.Last().Rank] is EmptySquare)
            {
                return false;
            }
            var piece = board[capture.Squares.First().File, capture.Squares.First().Rank] as IPiece;
            var pieceToCapture = board[capture.Squares.Last().File, capture.Squares.Last().Rank] as IPiece;
            return piece.IsWhite != pieceToCapture.IsWhite;
        }
    }
}
