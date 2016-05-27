using System.Collections.Generic;

namespace Chess.Game.Moves
{
    internal static class MoveGetter
    {
        public static List<Move> GetMoves(Position position)
        {
            var moves = new List<Move>();
            var emptyMoves = EmptyMoveGetter.GetEmptyMoves(position);
            moves.AddRange(emptyMoves);
            var captures = CaptureGetter.GetCaptures(position);
            moves.AddRange(captures);
            return moves;
        }
    }
}
