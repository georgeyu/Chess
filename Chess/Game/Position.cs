using Chess.Game.Moves;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Chess.Game
{
    internal class Position
    {
        public const string FenDelimiter = "/";
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Position()
        {
            WhiteMove = true;
            MoveCount = 1;
            Board = new Board();
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
            List<Capture> captures = CaptureGetter.GetCapturesIgnoringKingSafety(this);
            foreach (Capture capture in captures)
            {
                capture.MakeMove(this);
                bool kingExists = Board.KingExists(WhiteMove);
                capture.UndoMove(this);
                if (!kingExists)
                {
                    return false;
                }
            }
            return true;
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
            var boardFen = String.Join(FenDelimiter, squareFens);
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
