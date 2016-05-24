using Chess;
using Chess.Game;
using System.Collections.Generic;

namespace ChessTest
{
    internal static class SquareStringGenerator
    {
        private const string Files = "abcdefgh";

        public static Dictionary<string, SquareAbsolute> GenerateSquaresByString()
        {
            var squareByString = new Dictionary<string, SquareAbsolute>();
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                for (var j = 0; j < Constants.BoardLength; j++)
                {
                    string id = Files[i] + (j + 1).ToString();
                    squareByString[id] = new SquareAbsolute(i, j);
                }
            }
            return squareByString;
        }
    }
}
