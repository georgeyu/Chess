using Chess.Game.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Moves
{
    internal static class CastleGetter
    {
        public static List<Castle> GetCastles(Position position)
        {
            var castles = new List<Castle>();
            bool kingSideLegal = CastleLegal(true, position);
            var kingSideCastle = new Castle(position.WhiteMove, true);
            if (kingSideLegal)
            {
                castles.Add(kingSideCastle);
            }
            bool queenSideLegal = CastleLegal(false, position);
            var queenSideCastle = new Castle(position.WhiteMove, false);
            if (queenSideLegal)
            {
                castles.Add(queenSideCastle);
            }
            return castles;
        }

        private static bool CastleLegal(bool kingSide, Position position)
        {
            var castles = new List<Castle>();
            var rank = position.WhiteMove ? Board.PieceRankOffset : Board.Length - 1 - Board.PieceRankOffset;
            var kingSquare = new BoardVector(Board.KingFile, rank);
            var king = position.Board[kingSquare] as King;
            var rookSquare = new BoardVector(
                kingSide ? Board.Length - 1 - Board.RookFileOffset : Board.RookFileOffset,
                rank);
            var rook = position.Board[rookSquare] as Rook;
            var firstPassingSquare = new BoardVector(Board.KingFile + (kingSide ? 1 : -1), rank);
            var secondPassingSquare = new BoardVector(
                Board.KingFile + (kingSide ? Castle.CastleFileOffset : -Castle.CastleFileOffset),
                rank);
            var passingSquares = new List<BoardVector>() { firstPassingSquare, secondPassingSquare };
            var firstMove = new EmptyMove(new List<BoardVector>() { kingSquare, firstPassingSquare }, false);
            var secondMove = new EmptyMove(
                new List<BoardVector>()
                {
                    kingSquare,
                    firstPassingSquare,
                    secondPassingSquare
                },
                false);
            return
                (king != null) &&
                (king.White == position.WhiteMove) &&
                (!king.Moved) &&
                (rook != null) &&
                (rook.White == position.WhiteMove) &&
                (!rook.Moved) &&
                (passingSquares.All(x => position.Board.EmptySquare(x))) &&
                (!position.KingInCheck()) &&
                (firstMove.KingSafe(position)) &&
                (secondMove.KingSafe(position));
        }
    }
}
