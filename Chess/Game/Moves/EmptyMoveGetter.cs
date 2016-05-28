using Chess.Game.Pieces;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chess.Game.Moves
{
    internal class EmptyMoveGetter : MoveGetter
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public EmptyMoveGetter(Position position)
        {
            Position = position;
        }

        public override Position Position { get; }

        public override List<Move> GetMovesIgnoringKing()
        {
            var moves = new List<Move>();
            for (var i = 0; i < Position.Board.FileCount; i++)
            {
                for (var j = 0; j < Position.Board.RankCount; j++)
                {
                    ISquare square = Position.Board[i, j];
                    var piece = square as Piece;
                    if ((piece == null) || piece.White != Position.WhiteMove)
                    {
                        continue;
                    }
                    List<List<BoardVector>> moveVectors = piece.GenerateEmptyMoves();
                    IEnumerable<EmptyMove> filteredMoves = moveVectors
                        .Select(x => x.Select(y => new BoardVector(i + y.File, j + y.Rank)))
                        .Where(x => x.All(y => Position.Board.OnBoard(y)))
                        .Select(x => new EmptyMove(x.ToList(), piece.Moved))
                        .Where(x => x.PassingSquares.All(y => Position.Board.EmptySquare(y)));
                    moves.AddRange(filteredMoves);
                }
            }
            return moves;
        }
    }
}
