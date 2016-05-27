using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class King : Piece
    {
        private const string WhiteFen = "K";

        public King(bool white, bool moved = false) : base(WhiteFen, white, moved) { }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateAllMoves(1);
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateAllMoves(1);
        }
    }
}
