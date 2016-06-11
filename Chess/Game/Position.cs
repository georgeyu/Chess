using Chess.Game.Moves;
using Chess.Game.Pieces;
using log4net;
using System.Collections.Generic;
using System.Reflection;

namespace Chess.Game
{
    internal class Position
    {
        public const string FenDelimiter = "/";
        public List<BoardVector> enPassantSquares;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CaptureGetter captureGetter;
        private readonly CastleGetter castleGetter;
        private readonly EmptyMoveGetter emptyMoveGetter;
        private readonly PromoteGetter promoteGetter;
        private readonly EnPassantGetter enPassantGetter;

        public Position()
        {
            WhiteMove = true;
            MoveCount = 1;
            Board = new Board();
            enPassantSquares = new List<BoardVector>();
            captureGetter = new CaptureGetter(this);
            castleGetter = new CastleGetter(this);
            emptyMoveGetter = new EmptyMoveGetter(this);
            promoteGetter = new PromoteGetter(this);
            enPassantGetter = new EnPassantGetter(this);
        }

        public Board Board { get; private set; }

        public bool WhiteMove { get; private set; }

        public int MoveCount { get; private set; }

        public void IncrementTurn()
        {
            if (!WhiteMove)
            {
                MoveCount++;
            }
            WhiteMove = !WhiteMove;
        }

        public void DecrementTurn()
        {
            if (WhiteMove)
            {
                MoveCount--;
            }
            WhiteMove = !WhiteMove;
        }

        public bool KingSafe()
        {
            List<Move> moves = captureGetter.GetMovesIgnoringKing();
            foreach (Move move in moves)
            {
                var enPassantSquares = this.enPassantSquares;
                move.MakeMove(this);
                bool kingExists = Board.KingExists(WhiteMove);
                move.UndoMove(this, enPassantSquares);
                if (!kingExists)
                {
                    return false;
                }
            }
            return true;
        }

        public bool KingInCheck()
        {
            IncrementTurn();
            bool kingSafe = KingSafe();
            DecrementTurn();
            return !kingSafe;
        }

        public List<Move> GetMoves()
        {
            var moves = new List<Move>();
            List<Move> emptyMoves = emptyMoveGetter.GetMoves();
            moves.AddRange(emptyMoves);
            List<Move> captures = captureGetter.GetMoves();
            moves.AddRange(captures);
            List<Move> castles = castleGetter.GetMoves();
            moves.AddRange(castles);
            List<Move> promotes = promoteGetter.GetMoves();
            moves.AddRange(promotes);
            List<Move> enPassants = enPassantGetter.GetMoves();
            moves.AddRange(enPassants);
            return moves;
        }

        public string GetFen()
        {
            var squareFens = "";
            for (var i = Board.RankCount - 1; i >= 0; i--)
            {
                for (var j = 0; j < Board.FileCount; j++)
                {
                    string squareFen = Board[j, i].GetFen();
                    squareFens += squareFen;
                }
                if (i != 0)
                {
                    squareFens += FenDelimiter;
                }
            }
            var boardFen = string.Join(FenDelimiter, squareFens);
            string fen = ReplaceConsecutiveEmptySquaresWithIntegers(boardFen);
            return fen;
        }

        private static string ReplaceConsecutiveEmptySquaresWithIntegers(string fenWithEmptySquares)
        {
            int emptyCountInt = 0;
            var fenSplit = System.Text.RegularExpressions.Regex.Split(fenWithEmptySquares, "");
            var fen = "";
            foreach (string square in fenSplit)
            {
                if (square == EmptySquare.Fen)
                {
                    emptyCountInt++;
                    continue;
                }
                if (emptyCountInt != 0)
                {
                    var emptyCountString = emptyCountInt.ToString();
                    emptyCountInt = 0;
                    fen += emptyCountString;
                }
                fen += square;
            }
            return fen;
        }
    }
}
