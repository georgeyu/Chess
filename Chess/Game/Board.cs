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

        public Board()
        {
            Squares = new ISquare[Length, Length];
            for (var i = 0; i < Length; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    Squares[i, j] = new EmptySquare();
                }
            }
            for (var i = 0; i < Length; i++)
            {
                Squares[i, PawnRankOffset] = new Pawn(true);
                Squares[i, Length - 1 - PawnRankOffset] = new Pawn(false);
            }
            AddTwinPieces(RookFileOffset, typeof(Rook));
            AddTwinPieces(KnightFileOffset, typeof(Knight));
            AddTwinPieces(BishopFileOffset, typeof(Bishop));
            Squares[QueenFile, PieceRankOffset] = new Queen(true);
            Squares[KingFile, PieceRankOffset] = new King(true);
            Squares[QueenFile, Length - 1 - PieceRankOffset] = new Queen(false);
            Squares[KingFile, Length - 1 - PieceRankOffset] = new King(false);
        }

        public int FileCount => Squares.GetLength(FilesIndex);

        public int RankCount => Squares.GetLength(RanksIndex);

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
                return Squares[file, rank];
            }
            set
            {
                Squares[file, rank] = value;
            }
        }

        public ISquare[,] Squares { get; private set; }

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
            Squares[fileOffset, PieceRankOffset] = Piece.GetPiece(pieceType, true);
            Squares[Length - 1 - fileOffset, PieceRankOffset] = Piece.GetPiece(pieceType, true);
            Squares[fileOffset, Length - 1 - PieceRankOffset] = Piece.GetPiece(pieceType, false);
            Squares[Length - 1 - fileOffset, Length - 1 - PieceRankOffset] = Piece.GetPiece(pieceType, false);
        }
    }
}
