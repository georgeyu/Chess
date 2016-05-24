using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class Capture : IMove
    {
        private readonly SquareAbsolute startSquareLocation;
        private readonly SquareAbsolute captureSquareLocation;

        public Capture(
            List<SquareAbsolute> squares,
            bool hasMoved,
            IPiece capturedPiece)
        {
            Squares = squares;
            HasMoved = hasMoved;
            CapturedPiece = capturedPiece;
            startSquareLocation = squares.First();
            captureSquareLocation = squares.Last();
            if (squares.Count > 2)
            {
                PassingSquares = squares.Skip(1).Take(squares.Count - 2).ToList();
            }
        }

        public List<SquareAbsolute> Squares { get; private set; }

        public bool HasMoved { get; private set; }

        public IPiece CapturedPiece { get; private set; }

        public List<SquareAbsolute> PassingSquares { get; private set; }

        public void MakeMove(Position position)
        {
            var emptySquare = new EmptySquare();
            ISquare startSquare = position.Board[startSquareLocation.File, startSquareLocation.Rank];
            var capturingPiece = startSquare as IPiece;
            capturingPiece.HasMoved = true;
            position.Board[startSquareLocation.File, startSquareLocation.Rank] = emptySquare;
            position.Board[captureSquareLocation.File, captureSquareLocation.Rank] = capturingPiece;
            position.IncrementTurn();
        }

        public void UndoMove(Position position)
        {
            ISquare captureSquare = position.Board[captureSquareLocation.File, captureSquareLocation.Rank];
            var capturingPiece = captureSquare as IPiece;
            capturingPiece.HasMoved = HasMoved;
            position.Board[captureSquareLocation.File, captureSquareLocation.Rank] = CapturedPiece;
            position.Board[startSquareLocation.File, startSquareLocation.Rank] = capturingPiece;
            position.DecrementTurn();
        }
    }
}
