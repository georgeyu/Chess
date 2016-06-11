using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class EnPassant : Move
    {
        public const int RankChangeForMoveBeforeEnPassant = 2;
        private readonly BoardVector endSquare;
        private readonly BoardVector captureSquare;

        public EnPassant(BoardVector startSquare, BoardVector endSquare)
        {
            StartSquare = startSquare;
            this.endSquare = endSquare;
            captureSquare = new BoardVector(endSquare.File, startSquare.Rank);
        }

        public BoardVector StartSquare { get; private set; }

        public override void Change(Position position)
        {
            position.Board[captureSquare] = new EmptySquare();
            position.Board[endSquare] = position.Board[StartSquare];
            position.Board[StartSquare] = new EmptySquare();
        }

        public override void UndoChange(Position position)
        {
            position.Board[StartSquare] = position.Board[endSquare];
            position.Board[endSquare] = new EmptySquare();
            position.Board[captureSquare] = new Pawn(position.WhiteMove, true);
        }
    }
}
