using System;
using Chess.Game.Pieces;

namespace Chess.Game.Moves
{
    internal class Castle : Move
    {
        public const int CastleFileOffset = 2;
        private readonly BoardVector rookStartSquare;
        private readonly BoardVector rookEndSquare;

        public Castle(bool white, bool kingSide)
        {
            var pieceRank = white ? Board.PieceRankOffset : Board.Length - 1 - Board.PieceRankOffset;
            StartSquareVector = new BoardVector(Board.KingFile, pieceRank);
            EndSquareVector = new BoardVector(
                Board.KingFile + (kingSide ? CastleFileOffset : -CastleFileOffset),
                pieceRank);
            rookStartSquare = new BoardVector(
                kingSide ? Board.Length - 1 - Board.RookFileOffset : Board.RookFileOffset,
                pieceRank);
            rookEndSquare = new BoardVector(EndSquareVector.File + (kingSide ? -1 : 1), pieceRank);
        }

        public override BoardVector StartSquareVector { get; }

        public override BoardVector EndSquareVector { get; }

        public override void Change(Position position)
        {
            position.Board[EndSquareVector] = position.Board[StartSquareVector];
            position.Board[StartSquareVector] = new EmptySquare();
            var king = position.Board[EndSquareVector] as Piece;
            king.Moved = true;
            position.Board[rookEndSquare] = position.Board[rookStartSquare];
            position.Board[rookStartSquare] = new EmptySquare();
            var rook = position.Board[rookEndSquare] as Piece;
            rook.Moved = true;
        }

        public override void UndoChange(Position position)
        {
            position.Board[StartSquareVector] = position.Board[EndSquareVector];
            position.Board[EndSquareVector] = new EmptySquare();
            var king = position.Board[StartSquareVector] as Piece;
            king.Moved = false;
            position.Board[rookStartSquare] = position.Board[rookEndSquare];
            position.Board[rookEndSquare] = new EmptySquare();
            var rook = position.Board[rookStartSquare] as Piece;
            rook.Moved = false;
        }
    }
}
