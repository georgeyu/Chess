using System.Collections.Generic;

namespace Chess.Game.Pieces
{
    internal class Bishop : Piece
    {
        private const string WhiteFen = "B";

        public Bishop(bool white, bool moved = false) : base(WhiteFen, white, moved) { }

        public override List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateDiagonalMoves(Board.Length - 1);
        }

        public override List<List<BoardVector>> GenerateCapturesWithoutOrigin()
        {
            return MoveShiftGenerator.GenerateDiagonalMoves(Board.Length - 1);
        }
    }
}
