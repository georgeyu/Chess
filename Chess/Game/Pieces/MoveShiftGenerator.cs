using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Pieces
{
    internal static class MoveShiftGenerator
    {
        public static List<List<BoardVector>> GenerateStraightMoves(int length)
        {
            List<BoardVector> straightVectors = GenerateStraightVectors();
            return straightVectors.Select(x => GenerateMovesAlongVector(length, x)).SelectMany(x => x).ToList();
        }

        public static List<List<BoardVector>> GenerateDiagonalMoves(int length)
        {
            List<BoardVector> diagonalVectors = GenerateDiagonalVectors();
            return diagonalVectors.Select(x => GenerateMovesAlongVector(length, x)).SelectMany(x => x).ToList();
        }

        public static List<List<BoardVector>> GenerateAllMoves(int length)
        {
            List<List<BoardVector>> straightMoves = GenerateStraightMoves(length);
            List<List<BoardVector>> diagonalMoves = GenerateDiagonalMoves(length);
            return straightMoves.Concat(diagonalMoves).ToList();
        }

        private static List<BoardVector> GenerateStraightVectors()
        {
            return new List<BoardVector>()
            {
                new BoardVector(0, 1),
                new BoardVector(0, -1),
                new BoardVector(1, 0),
                new BoardVector(-1, 0)
            };
        }

        private static List<BoardVector> GenerateDiagonalVectors()
        {
            return new List<BoardVector>()
            {
                new BoardVector(1, 1),
                new BoardVector(1, -1),
                new BoardVector(-1, 1),
                new BoardVector(-1, -1)
            };
        }

        private static List<List<BoardVector>> GenerateMovesAlongVector(int length, BoardVector vector)
        {
            return Enumerable
                .Range(1, length)
                .Select(x => Enumerable
                    .Range(1, x)
                    .Select(y => new BoardVector(vector.File * y, vector.Rank * y))
                    .ToList())
                .ToList();
        }
    }
}
