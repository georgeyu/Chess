using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class EnPassant : Move
    {
        public const int RankChangeForMoveBeforeEnPassant = 2;
        private readonly BoardVector captureSquare;

        public EnPassant(BoardVector startSquare, BoardVector endSquare)
        {
            StartSquareVector = startSquare;
            EndSquareVector = endSquare;
            captureSquare = new BoardVector(endSquare.File, startSquare.Rank);
        }

        public override BoardVector StartSquareVector { get; }

        public override BoardVector EndSquareVector { get; }

        public override void Change(Position position)
        {
            position.Board[captureSquare] = new EmptySquare();
            position.Board[EndSquareVector] = position.Board[StartSquareVector];
            position.Board[StartSquareVector] = new EmptySquare();
        }

        public override void UndoChange(Position position)
        {
            position.Board[StartSquareVector] = position.Board[EndSquareVector];
            position.Board[EndSquareVector] = new EmptySquare();
            position.Board[captureSquare] = new Pawn(position.WhiteMove, true);
        }
    }
}
