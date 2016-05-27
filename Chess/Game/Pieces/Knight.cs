using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class Knight : Piece
    {
        private const int LongPart = 2;
        private const int ShortPart = 1;
        private const string WhiteFen = "N";

        public Knight(bool white, bool moved = false) : base(WhiteFen, white, moved) { }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            return GenerateMoves();
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return GenerateMoves();
        }

        private List<List<BoardVector>> GenerateMoves()
        {
            return new List<List<BoardVector>>()
            {

                new List<BoardVector>() { new BoardVector(LongPart, ShortPart) },
                new List<BoardVector>() { new BoardVector(LongPart, -ShortPart) },
                new List<BoardVector>() { new BoardVector(-LongPart, ShortPart) },
                new List<BoardVector>() { new BoardVector(-LongPart, -ShortPart) },
                new List<BoardVector>() { new BoardVector(ShortPart, LongPart) },
                new List<BoardVector>() { new BoardVector(ShortPart, -LongPart) },
                new List<BoardVector>() { new BoardVector(-ShortPart, LongPart) },
                new List<BoardVector>() { new BoardVector(-ShortPart, -LongPart) }
            };
        }
    }
}
