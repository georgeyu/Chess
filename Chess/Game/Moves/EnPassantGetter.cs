﻿using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class EnPassantGetter : MoveGetter
    {
        public EnPassantGetter(Position position)
        {
            Position = position;
        }

        public override Position Position { get; }

        public override List<Move> GetMovesIgnoringKing()
        {
            var direction = Position.WhiteMove ? -1 : 1;
            IEnumerable<Move> leftCaptureEnPassants = GetEnPassants(Position.enPassantSquares, true, direction);
            IEnumerable<Move> rightCaptureEnPassants = GetEnPassants(Position.enPassantSquares, false, direction);
            return leftCaptureEnPassants.Concat(rightCaptureEnPassants).ToList();
        }

        private IEnumerable<Move> GetEnPassants(
            List<BoardVector> enPassantSquares,
            bool captureLeft,
            int direction)
        {
            return enPassantSquares
                .Select(x => new EnPassant(new BoardVector(x.File + (captureLeft ? 1 : -1), x.Rank + direction), x))
                .Where(x => Position.Board.OnBoard(x.StartSquareVector))
                .Where(x => PawnColorMatch(x));
        }

        private bool PawnColorMatch(EnPassant enPassant)
        {
            var pawn = Position.Board[enPassant.StartSquareVector] as Pawn;
            if (pawn == null)
            {
                return false;
            }
            return pawn.White == Position.WhiteMove;
        }
    }
}
