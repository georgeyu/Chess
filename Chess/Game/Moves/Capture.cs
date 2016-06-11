using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class Capture : Move
    {
        public Capture(List<BoardVector> squares, bool moved, Piece capturedPiece)
        {
            StartSquareVector = squares.First();
            EndSquareVector = squares.Last();
            Moved = moved;
            CapturedPiece = capturedPiece;
            PassingSquares = squares
                .Except(new List<BoardVector>() { StartSquareVector, EndSquareVector })
                .ToList();
        }

        public bool Moved { get; private set; }

        public override BoardVector StartSquareVector { get; }

        public override BoardVector EndSquareVector { get; }

        public Piece CapturedPiece { get; private set; }

        public List<BoardVector> PassingSquares { get; private set; }

        public override void Change(Position position)
        {
            ISquare startSquare = position.Board[StartSquareVector];
            var capturingPiece = (Piece)startSquare;
            capturingPiece.Moved = true;
            position.Board[StartSquareVector] = new EmptySquare();
            position.Board[EndSquareVector] = capturingPiece;
        }

        public override void UndoChange(Position position)
        {
            ISquare captureSquare = position.Board[EndSquareVector];
            var capturingPiece = captureSquare as Piece;
            capturingPiece.Moved = Moved;
            position.Board[EndSquareVector] = CapturedPiece;
            position.Board[StartSquareVector] = capturingPiece;
        }
    }
}
