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
            CaptureSquareVector = squares.Last();
            Moved = moved;
            CapturedPiece = capturedPiece;
            PassingSquares = squares
                .Except(new List<BoardVector>() { StartSquareVector, CaptureSquareVector })
                .ToList();
        }

        public bool Moved { get; private set; }

        public BoardVector StartSquareVector { get; private set; }

        public BoardVector CaptureSquareVector { get; private set; }

        public Piece CapturedPiece { get; private set; }

        public List<BoardVector> PassingSquares { get; private set; }

        public override void Change(Position position)
        {
            ISquare startSquare = position.Board[StartSquareVector];
            var capturingPiece = (Piece)startSquare;
            capturingPiece.Moved = true;
            position.Board[StartSquareVector] = new EmptySquare();
            position.Board[CaptureSquareVector] = capturingPiece;
        }

        public override void UndoChange(Position position)
        {
            ISquare captureSquare = position.Board[CaptureSquareVector];
            var capturingPiece = captureSquare as Piece;
            capturingPiece.Moved = Moved;
            position.Board[CaptureSquareVector] = CapturedPiece;
            position.Board[StartSquareVector] = capturingPiece;
        }
    }
}
