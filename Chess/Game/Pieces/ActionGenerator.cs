using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    /// <summary>
    /// Generates moves and captures relative to a starting square.
    /// </summary>
    internal static class ActionGenerator
    {
        /// <summary>
        /// Generates moves along the file and rank.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <returns>Moves along the file and rank.</returns>
        public static SquareChange[][] GenerateStraightMoves(int length)
        {
            var moves = GenerateActions(length, GenerateStraightDirections, GenerateMoves);
            return moves;
        }

        /// <summary>
        /// Generates moves along the diagonals.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <returns>Moves along the diagonals.</returns>
        public static SquareChange[][] GenerateDiagonalMoves(int length)
        {
            var moves = GenerateActions(length, GenerateDiagonalDirections, GenerateMoves);
            return moves;
        }

        /// <summary>
        /// Generates captures along the file and rank.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <returns>Captures along the file and rank.</returns>
        public static CaptureRelative[] GenerateStraightCaptures(int length)
        {
            var captures = GenerateActions(length, GenerateStraightDirections, GenerateCaptures);
            return captures;
        }

        /// <summary>
        /// Generates captures along the diagonals.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <returns>Captures along the diagonals.</returns>
        public static CaptureRelative[] GenerateDiagonalCaptures(int length)
        {
            var captures = GenerateActions(length, GenerateDiagonalDirections, GenerateCaptures);
            return captures;
        }

        /// <summary>
        /// Generate moves or captures.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length">The max displacement.</param>
        /// <param name="directionGenerator">The directions to use.</param>
        /// <param name="actionGenerator">The type of action: move or capture.</param>
        /// <returns>Actions.</returns>
        private static T[] GenerateActions<T>(
            int length,
            Func<List<SquareChange>> directionGenerator,
            Func<int, SquareChange, List<T>> actionGenerator)
        {
            List<SquareChange> directions = directionGenerator();
            var actionsByDirection = directions.Select(x => actionGenerator(length, x));
            var actionEnumerable = actionsByDirection.SelectMany(x => x);
            var actionArray = actionEnumerable.ToArray();
            return actionArray;
        }

        /// <summary>
        /// Generate directions along the file and rank.
        /// </summary>
        /// <returns>Directions along the file and rank.</returns>
        private static List<SquareChange> GenerateStraightDirections()
        {
            var directions = new List<SquareChange>();
            var north = new SquareChange(0, 1);
            var south = new SquareChange(0, -1);
            var east = new SquareChange(1, 0);
            var west = new SquareChange(-1, 0);
            directions.Add(north);
            directions.Add(south);
            directions.Add(east);
            directions.Add(west);
            return directions;
        }

        /// <summary>
        /// Generate directions along the diagonals.
        /// </summary>
        /// <returns>Directions along the diagonals.</returns>
        private static List<SquareChange> GenerateDiagonalDirections()
        {
            var directions = new List<SquareChange>();
            var northeast = new SquareChange(1, 1);
            var northwest = new SquareChange(-1, 1);
            var southeast = new SquareChange(1, -1);
            var southwest = new SquareChange(-1, -1);
            directions.Add(northeast);
            directions.Add(northwest);
            directions.Add(southeast);
            directions.Add(southwest);
            return directions;
        }

        /// <summary>
        /// Generate moves along the direction.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <param name="direction">The unit direction.</param>
        /// <returns>Moves along the direction.</returns>
        private static List<SquareChange[]> GenerateMoves(int length, SquareChange direction)
        {
            var moves = new List<SquareChange[]>();
            for (var i = 1; i <= length; i++)
            {
                var passingSquaresIndices = Enumerable.Range(1, i);
                var passingSquares = passingSquaresIndices.Select(
                    x => new SquareChange(direction.FileChange * x, direction.RankChange * x));
                var move = passingSquares.ToArray();
                moves.Add(move);
            }
            return moves;
        }

        /// <summary>
        /// Generates captures along the direction.
        /// </summary>
        /// <param name="length">The max displacement.</param>
        /// <param name="direction">The unit direction.</param>
        /// <returns>Captures along the direction.</returns>
        private static List<CaptureRelative> GenerateCaptures(int length, SquareChange direction)
        {
            var captures = new List<CaptureRelative>();
            for (var i = 1; i <= length; i++)
            {
                var captureSquare = new SquareChange(direction.FileChange * i, direction.RankChange * i);
                var passingSquaresIndices = Enumerable.Range(1, i - 1);
                var passingSquareEnumerable = passingSquaresIndices.Select(
                    x => new SquareChange(direction.FileChange * x, direction.RankChange * x));
                var passingSquareArray = passingSquareEnumerable.ToArray();
                var capture = new CaptureRelative(captureSquare, passingSquareArray);
                captures.Add(capture);
            }
            return captures;
        }
    }
}
