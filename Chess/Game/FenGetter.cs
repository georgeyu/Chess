using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess.Game
{
    internal static class FenGetter
    {
        private const string FenRankSeparator = "/";

        /// <summary>
        /// Gets the FEN for a position.
        /// </summary>
        /// <param name="position">The position to get the FEN from.</param>
        /// <returns>FEN.</returns>
        public static string GetFen(Position position)
        {
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            var squareFens = "";
            for (var i = ranks - 1; i >= 0; i--)
            {
                for (var j = 0; j < files; j++)
                {
                    string squareFen = position.Board[j, i].GetFen();
                    squareFens += squareFen;
                }
                if (i != 0)
                {
                    squareFens += FenRankSeparator;
                }
            }
            var boardFen = String.Join(FenRankSeparator, squareFens);
            string fen = ReplaceConsecutiveEmptySquaresWithIntegers(boardFen);
            return fen;
        }

        private static string ReplaceConsecutiveEmptySquaresWithIntegers(string fenWithEmptySquares)
        {
            int emptyCountInt = 0;
            string[] fenSplit = Regex.Split(fenWithEmptySquares, "");
            string fen = "";
            foreach (string square in fenSplit)
            {
                if (square == Constants.EmptySquare)
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
