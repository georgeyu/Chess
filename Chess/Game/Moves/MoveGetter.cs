using System.Collections.Generic;

namespace Chess.Game.Moves
{
    internal static class MoveGetter
    {
        public static List<IMove> GetMoves(Position position)
        {
            var moves = new List<IMove>();
            var emptyMoves = EmptyMoveGetter.GetEmptyMoves(position);
            var captures = CaptureGetter.GetCaptures(position);
            moves.AddRange(emptyMoves);
            moves.AddRange(captures);
            return moves;
        }
    }
}
