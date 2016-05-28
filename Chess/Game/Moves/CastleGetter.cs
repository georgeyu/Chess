using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal class CastleGetter : MoveGetter
    {
        public CastleGetter(Position position)
        {
            Position = position;
        }

        public override Position Position { get; }

        public override List<Move> GetMovesIgnoringKing()
        {
            var moves = new List<Move>();
            bool kingSideLegal = CastleLegal(true, Position);
            var kingSideCastle = new Castle(Position.WhiteMove, true);
            if (kingSideLegal)
            {
                moves.Add(kingSideCastle);
            }
            bool queenSideLegal = CastleLegal(false, Position);
            var queenSideCastle = new Castle(Position.WhiteMove, false);
            if (queenSideLegal)
            {
                moves.Add(queenSideCastle);
            }
            return moves;
        }

        private static bool CastleLegal(bool kingSide, Position position)
        {
            var castles = new List<Castle>();
            var rank = position.WhiteMove ?
                Board.PieceRankOffset :
                position.Board.RankCount - 1 - Board.PieceRankOffset;
            var kingSquare = new BoardVector(Board.KingFile, rank);
            var king = position.Board[kingSquare] as King;
            var rookSquare = new BoardVector(
                kingSide ? position.Board.FileCount - 1 - Board.RookFileOffset : Board.RookFileOffset,
                rank);
            var rook = position.Board[rookSquare] as Rook;
            var firstPassingSquare = new BoardVector(Board.KingFile + (kingSide ? 1 : -1), rank);
            var secondPassingSquare = new BoardVector(
                Board.KingFile + (kingSide ? Castle.CastleFileOffset : -Castle.CastleFileOffset),
                rank);
            var passingSquares = new List<BoardVector>() { firstPassingSquare, secondPassingSquare };
            var firstMove = new EmptyMove(new List<BoardVector>() { kingSquare, firstPassingSquare }, false);
            return
                (king != null) &&
                (king.White == position.WhiteMove) &&
                (!king.Moved) &&
                (rook != null) &&
                (rook.White == position.WhiteMove) &&
                (!rook.Moved) &&
                (passingSquares.All(x => position.Board.EmptySquare(x))) &&
                (!position.KingInCheck()) &&
                (firstMove.KingSafe(position));
        }
    }
}
