using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class Rook : Piece
    {
        private const string WhiteFen = "R";

        public Rook(bool white, bool moved = false) : base(WhiteFen, white, moved) { }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateStraightMoves(Board.Length - 1);
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateStraightMoves(Board.Length - 1);
        }
    }
}
