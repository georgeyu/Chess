using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class Pawn : Piece
    {
        private const int unmovedMove = 2;
        private const string WhiteFen = "P";
        private readonly int direction;

        public Pawn(bool white, bool moved = false) : base(WhiteFen, white, moved)
        {
            direction = White ? 1 : -1;
        }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            var singleSquare = new BoardVector(0, direction);
            var emptyMoves = new List<List<BoardVector>>()
            {
                new List<BoardVector>() { singleSquare }
            };
            if (!Moved)
            {
                emptyMoves.Add(new List<BoardVector>() { singleSquare, new BoardVector(0, direction * unmovedMove) });
            }
            return emptyMoves;
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return new List<List<BoardVector>>()
            {
                new List<BoardVector>() { new BoardVector(-1, direction) },
                new List<BoardVector>() { new BoardVector(1, direction) }
            };
        }
    }
}
