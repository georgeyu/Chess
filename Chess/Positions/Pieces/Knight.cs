using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
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

        public SquareRelative[][] GetMoves()
        {
            List<SquareRelative> finalSquares = GetFinalSquares();
            IEnumerable<SquareRelative[]> movesEnumerable = finalSquares.Select(x => new SquareRelative[] { x });
            SquareRelative[][] movesArray = movesEnumerable.ToArray();
            return movesArray;
        }

        public CaptureRelative[] GetCaptures()
        {
            List<SquareRelative> finalSquares = GetFinalSquares();
            IEnumerable<CaptureRelative> capturesEnumerable = finalSquares.Select(x => new CaptureRelative(x, new SquareRelative[] { }));
            CaptureRelative[] capturesArray = capturesEnumerable.ToArray();
            return capturesArray;
        }

        private List<SquareRelative> GetFinalSquares()
        {
            var finalSquares = new List<SquareRelative>();
            var fileShort = new Tuple<int, int>(Short, Long);
            var rankShort = new Tuple<int, int>(Long, Short);
            Tuple<int, int>[] moveBaseCases = { fileShort, rankShort };
            foreach (var moveBaseCase in moveBaseCases)
            {
                List<SquareRelative> fileSignFlipped = FlipFileSign(moveBaseCase);
                finalSquares.AddRange(fileSignFlipped);
            }
            return finalSquares;
        }

        private List<SquareRelative> FlipFileSign(Tuple<int, int> moveBaseCase)
        {
            var moves = new List<SquareRelative>();
            foreach (var sign in signs)
            {
                List<SquareRelative> rankSignFlipped = FlipRankSign(moveBaseCase, sign);
                moves.AddRange(rankSignFlipped);
            }
            return moves;
        }

        private List<SquareRelative> FlipRankSign(Tuple<int, int> moveBaseCase, int fileSign)
        {
            var moves = new List<SquareRelative>();
            foreach (var sign in signs)
            {
                SquareRelative move = new SquareRelative(fileSign * moveBaseCase.Item1, sign * moveBaseCase.Item2);
                moves.Add(move);
            }
            return moves;
        }
    }
}
