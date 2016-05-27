using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal static class CaptureGetter
    {
        public static List<Capture> GetCaptures(Position position)
        {
            var capturesIgnoringKingSafety = GetCapturesIgnoringKingSafety(position);
            return capturesIgnoringKingSafety.Where(x => x.KingSafe(position)).ToList();
        }

        public static List<Capture> GetCapturesIgnoringKingSafety(Position position)
        {
            var moves = new List<Capture>();
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
                    List<List<BoardVector>> moveVectors = piece.GenerateCaptures();
                    IEnumerable<Capture> filteredMoves = moveVectors
                        .Select(x => x.Select(y => new BoardVector(i + y.File, j + y.Rank)))
                        .Where(x => x.All(y => position.Board.OnBoard(y)))
                        .Select(x => GetCaptureFromCaptureOnBoard(x.ToList(), piece.Moved, position.Board))
                        .Where(x => EnemyPiece(x.CaptureSquareVector, position.Board, position.WhiteMove))
                        .Where(x => x.PassingSquares.All(y => position.Board.EmptySquare(y)));
                    moves.AddRange(filteredMoves);
                }
            }
            return moves;
        }

        private static Capture GetCaptureFromCaptureOnBoard(List<BoardVector> captureOnBoard, bool moved, Board board)
        {
            BoardVector captureSquare = captureOnBoard.Last();
            var piece = board[captureSquare] as Piece;
            return new Capture(captureOnBoard, moved, piece);
        }

        private static bool EnemyPiece(BoardVector square, Board board, bool white)
        {
            var piece = board[square] as Piece;
            if (piece == null)
            {
                return false;
            }
            return piece.White != white;
        }
    }
}
