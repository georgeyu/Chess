using Chess.Positions.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions
{
    internal class Position
    {
        private const int EmptySquareOffset = 2;
        private const int WhitePawnRank = 1;
        private const int BlackPawnRank = Constants.BoardDimension - 2;
        private const int WhitePieceRank = 0;
        private const int BlackPieceRank = Constants.BoardDimension - 1;
        private const int RookFileOffset = 0;
        private const int KnightFileOffset = 1;
        private const int BishopFileOffset = 2;
        private const int QueenFile = 3;
        private const int KingFile = 4;

        public Position()
        {
            IsWhiteMove = true;
            MoveNumber = 1;
            Board = new Square[Constants.BoardDimension, Constants.BoardDimension];
            SetupStartPosition();
        }

        public Position(bool isWhiteMove, int moveNumber)
        {
            IsWhiteMove = isWhiteMove;
            MoveNumber = moveNumber;
            throw new NotImplementedException();
        }

        public bool IsWhiteMove { get; private set; }

        public int MoveNumber { get; private set; }

        // The 0th dimension is the file. The 1st dimension is the rank.
        public Square[,] Board { get; private set; }

        private void SetupStartPosition()
        {
            FillWithEmptySquares();
            FillWithPawns();
            FillDoublePieces();
            FillSinglePieces();
        }

        private void FillWithEmptySquares()
        {
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                FillFileWithEmptySquares(i);
            }
        }

        private void FillFileWithEmptySquares(int rank)
        {
            var emptySquare = new EmptySquare();
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                Board[rank, i] = emptySquare;
            }
        }

        private void FillWithPawns()
        {
            Piece whitePawn = new Pawn(true, false);
            Piece blackPawn = new Pawn(false, false);
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                Board[i, WhitePawnRank] = whitePawn;
                Board[i, BlackPawnRank] = blackPawn;
            }
        }

        /// <summary>
        /// Fill with pieces that have a copy with the same color (knight, bishop and rook).
        /// </summary>
        private void FillDoublePieces()
        {
            Piece whiteKnight = new Knight(true, false);
            Piece whiteBishop = new Bishop(true, false);
            Piece whiteRook = new Rook(true, false);
            Piece blackKnight = new Knight(false, false);
            Piece blackBishop = new Bishop(false, false);
            Piece blackRook = new Rook(false, false);
            FillPieceTwice(whiteKnight, KnightFileOffset);
            FillPieceTwice(whiteBishop, BishopFileOffset);
            FillPieceTwice(whiteRook, RookFileOffset);
            FillPieceTwice(blackKnight, KnightFileOffset);
            FillPieceTwice(blackBishop, BishopFileOffset);
            FillPieceTwice(blackRook, RookFileOffset);
        }

        private void FillPieceTwice(Piece piece, int offset)
        {
            int rank = piece.IsWhite ? WhitePieceRank : BlackPieceRank;
            Board[offset, rank] = piece;
            Board[Constants.BoardDimension - 1 - offset, rank] = piece;
        }

        /// <summary>
        /// Fill with pieces that do not have a copy (king and queen).
        /// </summary>
        private void FillSinglePieces()
        {
            Piece whiteQueen = new Queen(true, false);
            Piece whiteKing = new King(true, false);
            Piece blackQueen = new Queen(false, false);
            Piece blackKing = new King(false, false);
            Board[QueenFile, WhitePieceRank] = whiteQueen;
            Board[KingFile, WhitePieceRank] = whiteKing;
            Board[QueenFile, BlackPieceRank] = blackQueen;
            Board[KingFile, BlackPieceRank] = blackKing;
        }
    }
}
