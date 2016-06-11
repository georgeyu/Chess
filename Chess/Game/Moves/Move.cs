using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal abstract class Move
    {
        public abstract BoardVector StartSquareVector { get; }

        public abstract BoardVector EndSquareVector { get; }

        public void MakeMove(Position position)
        {
            position.enPassantSquares = new List<BoardVector>();
            Change(position);
            position.IncrementTurn();
        }

        public void UndoMove(Position position, List<BoardVector> enPassantSquares)
        {
            UndoChange(position);
            position.enPassantSquares = enPassantSquares;
            position.DecrementTurn();
        }

        public bool KingSafe(Position position)
        {
            List<BoardVector> enPassantSquares = position.enPassantSquares.ToArray().ToList();
            MakeMove(position);
            bool kingSafeAfterMove = position.KingSafe();
            UndoMove(position, enPassantSquares);
            return kingSafeAfterMove;
        }

        public abstract void Change(Position position);

        public abstract void UndoChange(Position position);
    }
}
