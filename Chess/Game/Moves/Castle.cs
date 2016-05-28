using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class Castle : Move
    {
        public const int CastleFileOffset = 2;
        private readonly BoardVector kingStartSquare;
        private readonly BoardVector kingEndSquare;
        private readonly BoardVector rookStartSquare;
        private readonly BoardVector rookEndSquare;

        public Castle(bool white, bool kingSide)
        {
            var pieceRank = white ? Board.PieceRankOffset : Board.Length - 1 - Board.PieceRankOffset;
            kingStartSquare = new BoardVector(Board.KingFile, pieceRank);
            kingEndSquare = new BoardVector(
                Board.KingFile + (kingSide ? CastleFileOffset : -CastleFileOffset),
                pieceRank);
            rookStartSquare = new BoardVector(
                kingSide ? Board.Length - 1 - Board.RookFileOffset : Board.RookFileOffset,
                pieceRank);
            rookEndSquare = new BoardVector(kingEndSquare.File + (kingSide ? 1 : -1), pieceRank);
        }

        public override void Change(Position position)
        {
            position.Board[kingEndSquare] = position.Board[kingStartSquare];
            position.Board[kingStartSquare] = new EmptySquare();
            var king = position.Board[kingEndSquare] as Piece;
            king.Moved = true;
            position.Board[rookEndSquare] = position.Board[rookStartSquare];
            position.Board[rookStartSquare] = new EmptySquare();
            var rook = position.Board[rookEndSquare] as Piece;
            rook.Moved = true;
        }

        public override void UndoChange(Position position)
        {
            position.Board[kingStartSquare] = position.Board[kingEndSquare];
            position.Board[kingEndSquare] = new EmptySquare();
            var king = position.Board[kingStartSquare] as Piece;
            king.Moved = false;
            position.Board[rookStartSquare] = position.Board[rookEndSquare];
            position.Board[rookEndSquare] = new EmptySquare();
            var rook = position.Board[rookStartSquare] as Piece;
            rook.Moved = false;
        }
    }
}
