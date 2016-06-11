using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class EmptyMove : Move
    {
        public EmptyMove(List<BoardVector> squares, bool moved)
        {
            StartSquareVector = squares.First();
            EndSquareVector = squares.Last();
            PassingSquares = squares.Skip(1).ToList();
            Moved = moved;
        }

        public override BoardVector StartSquareVector { get; }

        public override BoardVector EndSquareVector { get; }

        public List<BoardVector> PassingSquares { get; private set; }

        public bool Moved { get; private set; }

        public override void Change(Position position)
        {
            ISquare startSquare = position.Board[StartSquareVector];
            var piece = startSquare as Piece;
            piece.Moved = true;
            position.Board[StartSquareVector] = new EmptySquare();
            position.Board[EndSquareVector] = piece;
            var pawn = piece as Pawn;
            if (pawn == null)
            {
                return;
            }
            var absRankChange = Math.Abs(EndSquareVector.Rank - StartSquareVector.Rank);
            if (absRankChange == EnPassant.RankChangeForMoveBeforeEnPassant)
            {
                var enPassantRank = (EndSquareVector.Rank + StartSquareVector.Rank) / 2;
                var enPassantSquare = new BoardVector(StartSquareVector.File, enPassantRank);
                position.enPassantSquares = new List<BoardVector>() { enPassantSquare };
            }
        }

        public override void UndoChange(Position position)
        {
            ISquare endSquare = position.Board[EndSquareVector];
            var piece = endSquare as Piece;
            piece.Moved = Moved;
            position.Board[EndSquareVector] = new EmptySquare();
            position.Board[StartSquareVector] = piece;
        }
    }
}
