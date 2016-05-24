using Chess.Game.Pieces;
using log4net;
using System.Reflection;

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
            Board = new ISquare[Constants.BoardLength, Constants.BoardLength];
            SetupBoard();
        }

        public Position(ISquare[,] board, bool isWhiteTurn = true, int turnNumber = 1)
        {
            Board = board;
            IsWhiteTurn = isWhiteTurn;
            TurnNumber = turnNumber;
        }

        public bool IsWhiteTurn { get; private set; }

        public int TurnNumber { get; private set; }

        public ISquare[,] Board { get; private set; }

        public void IncrementTurn()
        {
            if (!IsWhiteTurn)
            {
                TurnNumber++;
            }
            IsWhiteTurn = !IsWhiteTurn;
        }

        public void DecrementTurn()
        {
            if (IsWhiteTurn)
            {
                TurnNumber--;
            }
            IsWhiteTurn = !IsWhiteTurn;
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
    }
}
