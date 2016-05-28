using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal abstract class MoveGetter
    {
        public abstract Position Position { get; }

        public abstract List<Move> GetMovesIgnoringKing();

        public List<Move> GetMoves()
        {
            List<Move> movesIgnoringKing = GetMovesIgnoringKing();
            return movesIgnoringKing.Where(x => x.KingSafe(Position)).ToList();
        }
    }
}
