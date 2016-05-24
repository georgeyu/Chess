using Chess.Game.Pieces;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chess.Game.Moves
{
    internal static class EmptyMoveGetter
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static List<IMove> GetEmptyMoves(Position position)
        {
            var files = position.Board.GetLength(Constants.FileIndex);
            var ranks = position.Board.GetLength(Constants.RankIndex);
            var moves = new List<IMove>();
            for (var i = 0; i < files; i++)
            {
                for (var j = 0; j < ranks; j++)
                {
                    ISquare square = position.Board[i, j];
                    var piece = square as IPiece;
                    if (piece == null)
                    {
                        continue;
                    }
                    if (piece.IsWhite != position.IsWhiteTurn)
                    {
                        continue;
                    }
                    SquareChange[][] emptyMoves = piece.GenerateEmptyMoves();
                    var absoluteMoves = emptyMoves.Select(x => GetSquareAbsolutesFromRelatives(x, i, j));
                    var movesStayingOnBoard = GetMovesStayingOnBoard(absoluteMoves, files, ranks);
                    var movesWithEmptyPassingSquares = GetMovesWithEmptyPassingSquares(
                        movesStayingOnBoard,
                        position.Board);
                    var movesIgnoringKingSafety = movesWithEmptyPassingSquares.Select(
                        x => new EmptyMove(x.ToList(), piece.HasMoved));
                    IEnumerable<IMove> movesWithSafeKing = movesIgnoringKingSafety.Where(
                        x => KingSafetyChecker.IsKingSafe(position, x));
                    moves.AddRange(movesWithSafeKing);
                }
            }
            return moves;
        }

        private static IEnumerable<SquareAbsolute> GetSquareAbsolutesFromRelatives(
            SquareChange[] relativeSquares,
            int file,
            int rank)
        {
            IEnumerable<SquareAbsolute> squareAbsolutes = relativeSquares.Select(
                x => GetSquareAbsoluteFromRelative(x, file, rank));
            var firstSquareAdded = new[] { new SquareAbsolute(file, rank) }.Concat(squareAbsolutes);
            return firstSquareAdded;
        }

        private static SquareAbsolute GetSquareAbsoluteFromRelative(SquareChange relativeSquare, int file, int rank)
        {
            var squareAbsolute = new SquareAbsolute(relativeSquare.FileChange + file, relativeSquare.RankChange + rank);
            return squareAbsolute;
        }

        private static IEnumerable<IEnumerable<SquareAbsolute>> GetMovesStayingOnBoard(
            IEnumerable<IEnumerable<SquareAbsolute>> moves, int files, int ranks)
        {
            var movesStayingOnBoard = moves.Where(
                x => MovesUtil.AreSquaresOnBoard(x.ToList(), files, ranks));
            return movesStayingOnBoard;
        }

        private static IEnumerable<IEnumerable<SquareAbsolute>> GetMovesWithEmptyPassingSquares(
            IEnumerable<IEnumerable<SquareAbsolute>> moves,
            ISquare[,] board)
        {
            var movesWithEmptyPassingSquares = moves.Where(
                x => MovesUtil.ArePassingSquaresEmpty(x.Skip(1).ToList(), board));
            return movesWithEmptyPassingSquares;
        }
    }
}
