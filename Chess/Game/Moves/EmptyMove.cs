using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class EmptyMove : IMove
    {
        private readonly SquareAbsolute firstSquare;
        private readonly SquareAbsolute lastSquare;

        public EmptyMove(List<SquareAbsolute> squares, bool hasMoved)
        {
            Squares = squares;
            HasMoved = hasMoved;
            firstSquare = squares.First();
            lastSquare = squares.Last();
        }

        public List<SquareAbsolute> Squares { get; private set; }

        public bool HasMoved { get; private set; }

        public void MakeMove(Position position)
        {
            ISquare startSquare = position.Board[firstSquare.File, firstSquare.Rank];
            var piece = startSquare as IPiece;
            piece.HasMoved = true;
            var emptySquare = new EmptySquare();
            position.Board[firstSquare.File, firstSquare.Rank] = emptySquare;
            position.Board[lastSquare.File, lastSquare.Rank] = piece;
            position.IncrementTurn();
        }

        public void UndoMove(Position position)
        {

            ISquare endSquare = position.Board[lastSquare.File, lastSquare.Rank];
            var piece = endSquare as IPiece;
            piece.HasMoved = HasMoved;
            var emptySquare = new EmptySquare();
            position.Board[lastSquare.File, lastSquare.Rank] = emptySquare;
            position.Board[firstSquare.File, firstSquare.Rank] = piece;
            position.DecrementTurn();
        }
    }
}
