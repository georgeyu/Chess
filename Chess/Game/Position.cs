using Chess.Game.Pieces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess.Game
{
    internal class Position
    {
        private const int WhitePawnRank = 1;
        private const int BlackPawnRank = Constants.BoardLength - 2;
        private const int WhitePieceRank = 0;
        private const int BlackPieceRank = Constants.BoardLength - 1;
        private const int RookFileOffset = 0;
        private const int KnightFileOffset = 1;
        private const int BishopFileOffset = 2;
        private const int QueenFile = 3;
        private const int KingFile = 4;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Position()
        {
            IsWhiteTurn = true;
            TurnNumber = 1;
            Board = new Square[Constants.BoardLength, Constants.BoardLength];
            SetupBoard();
        }

        public Position(Square[,] board, bool isWhiteTurn = true, int turnNumber = 1)
        {
            Board = board;
            IsWhiteTurn = isWhiteTurn;
            TurnNumber = turnNumber;
        }

        public bool IsWhiteTurn { get; private set; }

        public int TurnNumber { get; private set; }

        public Square[,] Board { get; private set; }

        /// <summary>
        /// Changes the position to match the move made.
        /// </summary>
        /// <param name="move">Move to make.</param>
        public void MakeMove(MoveAbsolute move)
        {
            var startSquare = Board[move.StartSquare.File, move.StartSquare.Rank];
            Board[move.StartSquare.File, move.StartSquare.Rank] = new EmptySquare();
            SquareAbsolute finalSquare = move.PassingSquares.Last();
            var piece = startSquare as Piece;
            piece.HasMoved = true;
            Board[finalSquare.File, finalSquare.Rank] = piece;
            IncrementTurn();
        }

        /// <summary>
        /// Changes the position to match the capture made.
        /// </summary>
        /// <param name="capture">Capture to make.</param>
        public void MakeCapture(CaptureAbsolute capture)
        {
            var startSquare = Board[capture.StartSquare.File, capture.StartSquare.Rank];
            var piece = startSquare as Piece;
            Board[capture.StartSquare.File, capture.StartSquare.Rank] = new EmptySquare();
            piece.HasMoved = true;
            Board[capture.CaptureSquare.File, capture.CaptureSquare.Rank] = piece;
            IncrementTurn();
        }

        private void SetupBoard()
        {
            int files = Board.GetLength(Constants.FileIndex);
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                for (var j = 0; j < Constants.BoardLength; j++)
                {
                    Board[i, j] = new EmptySquare();
                }
            }
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                Board[i, WhitePawnRank] = new Pawn(true);
                Board[i, BlackPawnRank] = new Pawn(false);
            }
            Board[RookFileOffset, WhitePieceRank] = new Rook(true);
            Board[KnightFileOffset, WhitePieceRank] = new Knight(true);
            Board[BishopFileOffset, WhitePieceRank] = new Bishop(true);
            Board[QueenFile, WhitePieceRank] = new Queen(true);
            Board[KingFile, WhitePieceRank] = new King(true);
            Board[Constants.BoardLength - BishopFileOffset - 1, WhitePieceRank] = new Bishop(true);
            Board[Constants.BoardLength - KnightFileOffset - 1, WhitePieceRank] = new Knight(true);
            Board[Constants.BoardLength - RookFileOffset - 1, WhitePieceRank] = new Rook(true);
            Board[RookFileOffset, BlackPieceRank] = new Rook(false);
            Board[KnightFileOffset, BlackPieceRank] = new Knight(false);
            Board[BishopFileOffset, BlackPieceRank] = new Bishop(false);
            Board[QueenFile, BlackPieceRank] = new Queen(false);
            Board[KingFile, BlackPieceRank] = new King(false);
            Board[Constants.BoardLength - BishopFileOffset - 1, BlackPieceRank] = new Bishop(false);
            Board[Constants.BoardLength - KnightFileOffset - 1, BlackPieceRank] = new Knight(false);
            Board[Constants.BoardLength - RookFileOffset - 1, BlackPieceRank] = new Rook(false);
        }

        private void IncrementTurn()
        {
            if (!IsWhiteTurn)
            {
                TurnNumber++;
            }
            IsWhiteTurn = !IsWhiteTurn;
        }
    }
}
