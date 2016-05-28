using Chess.Game.Pieces;
using System;

namespace Chess.Game
{
    internal class Board
    {
        public const int Length = 8;
        public const int KingFile = 4;
        public const int PieceRankOffset = 0;
        public const int RookFileOffset = 0;
        private const int FilesIndex = 0;
        private const int RanksIndex = 1;
        private const int PawnRankOffset = 1;
        private const int KnightFileOffset = 1;
        private const int BishopFileOffset = 2;
        private const int QueenFile = 3;
        private readonly ISquare[,] squares;

        public Board()
        {
            squares = new ISquare[Length, Length];
            for (var i = 0; i < Length; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    squares[i, j] = new EmptySquare();
                }
            }
            for (var i = 0; i < Length; i++)
            {
                squares[i, PawnRankOffset] = new Pawn(true);
                squares[i, Length - 1 - PawnRankOffset] = new Pawn(false);
            }
            AddTwinPieces(RookFileOffset, typeof(Rook));
            AddTwinPieces(KnightFileOffset, typeof(Knight));
            AddTwinPieces(BishopFileOffset, typeof(Bishop));
            squares[QueenFile, PieceRankOffset] = new Queen(true);
            squares[KingFile, PieceRankOffset] = new King(true);
            squares[QueenFile, Length - 1 - PieceRankOffset] = new Queen(false);
            squares[KingFile, Length - 1 - PieceRankOffset] = new King(false);
        }

        public int FileCount => squares.GetLength(FilesIndex);

        public int RankCount => squares.GetLength(RanksIndex);

        public ISquare this[BoardVector square]
        {
            get
            {
                return this[square.File, square.Rank];
            }
            set
            {
                this[square.File, square.Rank] = value;
            }
        }

        public ISquare this[int file, int rank]
        {
            get
            {
                return squares[file, rank];
            }
            set
            {
                squares[file, rank] = value;
            }
        }

        public bool EmptySquare(BoardVector square)
        {
            return this[square] is EmptySquare;
        }

        public bool OnBoard(BoardVector square)
        {
            return
                (square.File >= 0) &&
                (square.File < FileCount) &&
                (square.Rank >= 0) &&
                (square.Rank < RankCount);
        }

        public bool KingExists(bool white)
        {
            for (var i = 0; i < FileCount; i++)
            {
                for (var j = 0; j < RankCount; j++)
                {
                    var king = this[i, j] as King;
                    if (king == null)
                    {
                        continue;
                    }
                    if (king.White == white)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddTwinPieces(int fileOffset, Type pieceType)
        {
            squares[fileOffset, PieceRankOffset] = Piece.GetPiece(pieceType, true);
            squares[Length - 1 - fileOffset, PieceRankOffset] = Piece.GetPiece(pieceType, true);
            squares[fileOffset, Length - 1 - PieceRankOffset] = Piece.GetPiece(pieceType, false);
            squares[Length - 1 - fileOffset, Length - 1 - PieceRankOffset] = Piece.GetPiece(pieceType, false);
        }
    }
}
