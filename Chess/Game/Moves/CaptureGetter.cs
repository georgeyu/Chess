using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class CaptureGetter : MoveGetter
    {
        public CaptureGetter(Position position)
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
                    List<List<BoardVector>> moveVectors = piece.GenerateCaptures();
                    IEnumerable<Capture> filteredMoves = moveVectors
                        .Select(x => x.Select(y => new BoardVector(i + y.File, j + y.Rank)))
                        .Where(x => x.All(y => Position.Board.OnBoard(y)))
                        .Select(x => GetCaptureFromCaptureOnBoard(x.ToList(), piece.Moved, Position.Board))
                        .Where(x => EnemyPiece(x.CaptureSquareVector, Position.Board, Position.WhiteMove))
                        .Where(x => x.PassingSquares.All(y => Position.Board.EmptySquare(y)));
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
