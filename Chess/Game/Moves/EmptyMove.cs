using Chess.Game.Pieces;
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

        public BoardVector StartSquareVector { get; private set; }

        public BoardVector EndSquareVector { get; private set; }

        public List<BoardVector> PassingSquares { get; private set; }

        public bool Moved { get; private set; }

        public override void Change(Position position)
        {
            ISquare startSquare = position.Board[StartSquareVector];
            var piece = startSquare as Piece;
            piece.Moved = true;
            position.Board[StartSquareVector] = new EmptySquare();
            position.Board[EndSquareVector] = piece;
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
