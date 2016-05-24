using Chess.Game.Pieces;
using log4net;
using System.Reflection;

namespace Chess.Game.Moves
{
    internal static class KingSafetyChecker
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Checks whether the king can be captured after making the move.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <param name="move">The move to make.</param>
        /// <returns>Whether the king can be captured after making the move.</returns>
        public static bool IsKingSafe(Position position, IMove move)
        {
            move.MakeMove(position);
            bool isKingSafe = IsKingSafe(position);
            move.UndoMove(position);
            return isKingSafe;
        }

        private static bool IsKingSafe(Position position)
        {
            var captures = CaptureGetter.GetCapturesIgnoringKingSafety(position);
            foreach (var capture in captures)
            {
                capture.MakeMove(position);
                var doesKingExist = DoesKingExist(position);
                capture.UndoMove(position);
                if (!doesKingExist)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool DoesKingExist(Position position)
        {
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            for (var i = 0; i < files; i++)
            {
                for (var j = 0; j < ranks; j++)
                {
                    ISquare square = position.Board[i, j];
                    if (square is EmptySquare)
                    {
                        continue;
                    }
                    IPiece piece = square as IPiece;
                    var king = piece as King;
                    if (king == null)
                    {
                        continue;
                    }
                    if (king.IsWhite == position.IsWhiteTurn)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
