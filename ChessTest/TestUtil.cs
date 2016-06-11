using Chess.Game;
using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessTest
{
    internal class TestUtil
    {
        public const string Files = "abcdefgh";
        private readonly Dictionary<string, BoardVector> squaresByString;

        public TestUtil()
        {
            squaresByString = new Dictionary<string, BoardVector>();
            for (var i = 0; i < Board.Length; i++)
            {
                for (var j = 0; j < Board.Length; j++)
                {
                    var id = Files[i] + (j + 1).ToString();
                    squaresByString[id] = new BoardVector(i, j);
                }
            }
        }

        public List<BoardVector> GetSquaresFromStrings(params string[] strings)
        {
            return strings.Select(x => squaresByString[x]).ToList();
        }

        public BoardVector GetSquareFromString(string value)
        {
            return squaresByString[value];
        }
    }
}
