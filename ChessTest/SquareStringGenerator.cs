using Chess.Game;
using Chess.Game.Pieces;
using System.Collections.Generic;

namespace ChessTest
{
    internal static class SquareStringGenerator
    {
        public const string Files = "abcdefgh";

        public static Dictionary<string, BoardVector> GenerateSquaresByString()
        {
            var squareByString = new Dictionary<string, BoardVector>();
            for (var i = 0; i < Board.Length; i++)
            {
                for (var j = 0; j < Board.Length; j++)
                {
                    var id = Files[i] + (j + 1).ToString();
                    squareByString[id] = new BoardVector(i, j);
                }
            }
            return squareByString;
        }
    }
}
