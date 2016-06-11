using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class Promote : Move
    {
        private readonly Piece pawn;
        private readonly ISquare promoteSquare;
        private readonly Piece promotedPiece;

        public Promote(
            Piece pawn,
            BoardVector pawnSquareVector,
            BoardVector promoteSquareVector,
            ISquare promoteSquare,
            Piece promotedPiece)
        {
            this.pawn = pawn;
            StartSquareVector = pawnSquareVector;
            EndSquareVector = promoteSquareVector;
            this.promoteSquare = promoteSquare;
            this.promotedPiece = promotedPiece;
        }

        public override BoardVector StartSquareVector { get; }

        public override BoardVector EndSquareVector { get; }

        public override void Change(Position position)
        {
            position.Board[EndSquareVector] = promotedPiece;
            position.Board[StartSquareVector] = new EmptySquare();
        }

        public override void UndoChange(Position position)
        {
            position.Board[StartSquareVector] = pawn;
            position.Board[EndSquareVector] = promoteSquare;
        }
    }
}
