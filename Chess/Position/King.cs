using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class King : Piece
    {
        public King(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            List<SquareChange> moves = CreateMoves();
            var movesEnumerable = moves.Select(x => new SquareChange[] { x });
            var movesArray = movesEnumerable.ToArray();
            return movesArray;
        }

        public Capture[] GetCaptures()
        {
            List<SquareChange> moves = CreateMoves();
            var capturesEnumerable = moves.Select(x => new Capture(x, new SquareChange[] { }));
            var capturesArray = capturesEnumerable.ToArray();
            return capturesArray;
        }

        private List<SquareChange> CreateMoves()
        {
            var moves = new List<SquareChange>();
            for (int i = -1; i <= 1; i++)
            {
                List<SquareChange> movesForFile = LoopOverRank(i);
                moves.AddRange(movesForFile);
            }
            var noMove = new SquareChange(0, 0);
            moves.Remove(noMove);
            return moves;
        }

        private List<SquareChange> LoopOverRank(int file)
        {
            var moves = new List<SquareChange>();
            for (int i = -1; i <= 1; i++)
            {
                var move = new SquareChange(file, i);
                moves.Add(move);
            }
            return moves;
        }
    }
}
