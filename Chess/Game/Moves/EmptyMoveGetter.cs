using Chess.Game.Pieces;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chess.Game.Moves
{
    internal static class EmptyMoveGetter
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static List<EmptyMove> GetEmptyMoves(Position position)
        {
            var moves = new List<EmptyMove>();
            for (var i = 0; i < position.Board.FileCount; i++)
            {
                for (var j = 0; j < position.Board.RankCount; j++)
                {
                    ISquare square = position.Board[i, j];
                    var piece = square as Piece;
                    if ((piece == null) || piece.White != position.WhiteMove)
                    {
                        continue;
                    }
                    List<List<BoardVector>> moveVectors = piece.GenerateEmptyMoves();
                    IEnumerable<EmptyMove> filteredMoves = moveVectors
                        .Select(x => x.Select(y => new BoardVector(i + y.File, j + y.Rank)))
                        .Where(x => x.All(y => position.Board.OnBoard(y)))
                        .Select(x => new EmptyMove(x.ToList(), piece.Moved))
                        .Where(x => x.PassingSquares.All(y => position.Board.EmptySquare(y)))
                        .Where(x => x.KingSafe(position));
                    moves.AddRange(filteredMoves);
                }
            }
            return moves;
        }
    }
}
