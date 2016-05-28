using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class Promote : Move
    {
        private readonly Piece pawn;
        private readonly BoardVector pawnSquareVector;
        private readonly BoardVector promoteSquareVector;
        private readonly ISquare promoteSquare;
        private readonly Piece promotedPiece;

        public Promote(
            Piece pawn,
            BoardVector pawnSquareVector,
            BoardVector promoteSquareVector,
            ISquare promoteSquare,
            Piece promotedPiece)
        {
            this.pawnSquareVector = pawnSquareVector;
            this.promoteSquareVector = promoteSquareVector;
            this.promoteSquare = promoteSquare;
            this.promotedPiece = promotedPiece;
        }

        public override void Change(Position position)
        {
            position.Board[promoteSquareVector] = promotedPiece;
            position.Board[pawnSquareVector] = new EmptySquare();
        }

        public override void UndoChange(Position position)
        {
            position.Board[pawnSquareVector] = pawn;
            position.Board[promoteSquareVector] = promoteSquare;
        }
    }
}
