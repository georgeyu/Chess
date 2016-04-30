using Chess.Game.Actions;
using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Actions
{
    internal static class KingSafetyChecker
    {
        /// <summary>
        /// Checks whether the king can be captured after making the move.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <param name="move">The move to make.</param>
        /// <returns>Whether the king can be captured after making the move.</returns>
        public static bool IsKingSafe(Position position, MoveAbsolute move)
        {
            bool hasMoved = position.HasMoved(move.StartSquare);
            position.Move(move);
            var isKingSafe = IsKingSafe(position);
            position.UndoMove(move, hasMoved);
            return isKingSafe;
        }

        /// <summary>
        /// Checks whether the king can be captured after making the capture.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <param name="capture">The capture to make.</param>
        /// <returns>Whether the king can be captured after making the capture.</returns>
        public static bool IsKingSafe(Position position, CaptureAbsolute capture)
        {
            bool hasMoved = position.HasMoved(capture.StartSquare);
            Piece capturedPiece = position.GetPiece(capture.CaptureSquare);
            position.Capture(capture);
            var isKingSafe = IsKingSafe(position);
            position.UndoCapture(capture, hasMoved, capturedPiece);
            return isKingSafe;
        }

        private static bool IsKingSafe(Position position)
        {
            var captures = CaptureGetter.GetCapturesIgnoringKingSafety(position);
            foreach (var capture in captures)
            {
                var square = position.Board[capture.CaptureSquare.File, capture.CaptureSquare.Rank];
                var isEmpty = square is EmptySquare;
                if (isEmpty)
                {
                    continue;
                }
                var piece = square as Piece;
                var king = piece as King;
                if ((object)king == null)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
