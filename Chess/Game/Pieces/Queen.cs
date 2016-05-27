using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class Queen : Piece
    {
        private const string WhiteFen = "Q";

        public Queen(bool white, bool moved = false) : base(WhiteFen, white, moved) { }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateAllMoves(Board.Length - 1);
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateAllMoves(Board.Length - 1);
        }
    }
}
