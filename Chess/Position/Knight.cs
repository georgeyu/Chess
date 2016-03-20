using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("IsWhite: {IsWhite}, HasMoved: {HasMoved}")]
    internal class Knight : Piece
    {
        private const int Short = 1;
        private const int Long = 2;
        private int[] signs = { -1, 1 };

        public Knight(bool isWhite, bool hasMoved)
        {
            IsWhite = isWhite;
            HasMoved = hasMoved;
        }

        public bool IsWhite { get; private set; }

        public bool HasMoved { get; set; }

        public SquareChange[][] GetMoves()
        {
            List<SquareChange> finalSquares = GetFinalSquares();
            IEnumerable<SquareChange[]> movesEnumerable = finalSquares.Select(x => new SquareChange[] { x });
            SquareChange[][] movesArray = movesEnumerable.ToArray();
            return movesArray;
        }

        public Capture[] GetCaptures()
        {
            List<SquareChange> finalSquares = GetFinalSquares();
            IEnumerable<Capture> capturesEnumerable = finalSquares.Select(x => new Capture(x, new SquareChange[] { }));
            Capture[] capturesArray = capturesEnumerable.ToArray();
            return capturesArray;
        }

        private List<SquareChange> GetFinalSquares()
        {
            var finalSquares = new List<SquareChange>();
            var fileShort = new Tuple<int, int>(Short, Long);
            var rankShort = new Tuple<int, int>(Long, Short);
            Tuple<int, int>[] moveBaseCases = { fileShort, rankShort };
            foreach (var moveBaseCase in moveBaseCases)
            {
                List<SquareChange> fileSignFlipped = FlipFileSign(moveBaseCase);
                finalSquares.AddRange(fileSignFlipped);
            }
            return finalSquares;
        }

        private List<SquareChange> FlipFileSign(Tuple<int, int> moveBaseCase)
        {
            var moves = new List<SquareChange>();
            foreach (var sign in signs)
            {
                List<SquareChange> rankSignFlipped = FlipRankSign(moveBaseCase, sign);
                moves.AddRange(rankSignFlipped);
            }
            return moves;
        }

        private List<SquareChange> FlipRankSign(Tuple<int, int> moveBaseCase, int fileSign)
        {
            var moves = new List<SquareChange>();
            foreach (var sign in signs)
            {
                SquareChange move = new SquareChange(moveBaseCase.Item1, sign * moveBaseCase.Item2);
                moves.Add(move);
            }
            return moves;
        }
    }
}
