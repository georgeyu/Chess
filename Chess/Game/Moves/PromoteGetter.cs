using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Moves
{
    internal class PromoteGetter : MoveGetter
    {
        public const int RankBeforePromoteOffset = 6;

        public PromoteGetter(Position position)
        {
            Position = position;
        }

        public static bool Promote(Piece piece, int rank, int rankCount)
        {
            return
                !(piece is Pawn) || (
                    (piece.White) &&
                    (rank != RankBeforePromoteOffset)) || (
                    (!piece.White) &&
                    (rank != rankCount - 1 - RankBeforePromoteOffset));
        }

        public override Position Position { get; }

        public override List<Move> GetMovesIgnoringKing()
        {
            var moves = new List<Move>();
            var rankBeforePromote = Position.WhiteMove ?
                RankBeforePromoteOffset :
                Position.Board.RankCount - RankBeforePromoteOffset;
            var promoteRank = Position.WhiteMove ? Position.Board.RankCount - 1: 0;
            for (var i = 0; i < Position.Board.FileCount; i++)
            {
                var pawn = Position.Board[i, rankBeforePromote] as Pawn;
                if ((pawn == null) ||
                    (pawn.White != Position.WhiteMove))
                {
                    continue;
                }
                var emptySquare = Position.Board[i, promoteRank] as EmptySquare;
                var pawnSquareVector = new BoardVector(i, rankBeforePromote);
                if (emptySquare != null)
                {
                    var promoteSquareVector = new BoardVector(i, promoteRank);
                    List<Promote> promotes = GetPromotesByPiece(
                        Position.WhiteMove,
                        pawn,
                        pawnSquareVector,
                        promoteSquareVector,
                        emptySquare);
                    moves.AddRange(promotes);
                }
                AddCapturePromote(i, Position, true, moves, rankBeforePromote, promoteRank, pawn);
                AddCapturePromote(i, Position, false, moves, rankBeforePromote, promoteRank, pawn);
            }
            return moves;
        }

        private List<Promote> GetPromotesByPiece(
            bool white,
            Piece pawn,
            BoardVector pawnSquareVector,
            BoardVector promoteSquareVector,
            ISquare promoteSquare)
        {
            var pieces = new List<Piece>()
            {
                new Queen(white, true),
                new Rook(white, true),
                new Bishop(white, true),
                new Knight(white, true)
            };
            return pieces
                .Select(x => new Promote(pawn, pawnSquareVector, promoteSquareVector, promoteSquare, x))
                .ToList();
        }

        private void AddCapturePromote(
            int file,
            Position position,
            bool leftCapture,
            List<Move> moves,
            int rankBeforePromote,
            int promoteRank,
            Piece pawn)
        {
            if ((file == 0) && leftCapture)
            {
                return;
            }
            if ((file == position.Board.FileCount - 1) && !leftCapture)
            {
                return;
            }
            var captureFile = file + (leftCapture ? -1 : 1);
            var capturedPiece = position.Board[captureFile, promoteRank] as Piece;
            if (capturedPiece != null)
            {
                var pawnSquareVector = new BoardVector(file, rankBeforePromote);
                var promoteSquareVector = new BoardVector(captureFile, promoteRank);
                List<Promote> promotes = GetPromotesByPiece(
                    position.WhiteMove,
                    pawn,
                    pawnSquareVector,
                    promoteSquareVector, 
                    position.Board[promoteSquareVector]);
                moves.AddRange(promotes);
            }
        }
    }
}
