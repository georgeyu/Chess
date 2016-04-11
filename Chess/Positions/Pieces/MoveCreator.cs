using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    // A move is an array of SquareChanges. An action is either a move or a Capture.
    internal static class MoveCreator
    {
        /// <summary>
        /// Get moves along the horizontal and vertical directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        public static SquareRelative[][] GetHorizontalVerticalMoves(int length)
        {
            SquareRelative[][] moves = GetMoves(length, GetHorizontalVerticalFinalSquaresSet);
            return moves;
        }

        /// <summary>
        /// Get moves along the diagonal directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        public static SquareRelative[][] GetDiagonalMoves(int length)
        {
            SquareRelative[][] moves = GetMoves(length, GetDiagonalFinalSquaresSet);
            return moves;
        }

        /// <summary>
        /// Get captures along the horizontal and vertical directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        public static CaptureRelative[] GetHorizontalVerticalCaptures(int length)
        {
            CaptureRelative[] captures = GetCaptures(length, GetHorizontalVerticalFinalSquaresSet);
            return captures;
        }

        /// <summary>
        /// Get captures along the diagonal directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        public static CaptureRelative[] GetDiagonalCaptures(int length)
        {
            CaptureRelative[] captures = GetCaptures(length, GetDiagonalFinalSquaresSet);
            return captures;
        }

        /// <summary>
        /// Get moves in a set of directions.
        /// </summary>
        /// <param name="length">Max distance for moves.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        private static SquareRelative[][] GetMoves(int length, Func<int, List<List<SquareRelative>>> getFinalSquaresSet)
        {
            SquareRelative[][] moves = GetActions(length, getFinalSquaresSet, ExpandToMoves);
            return moves;
        }

        /// <summary>
        /// Get Captures in a set of directions.
        /// </summary>
        /// <param name="length">Max distance for Captures.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        private static CaptureRelative[] GetCaptures(int length, Func<int, List<List<SquareRelative>>> getFinalSquaresSet)
        {
            CaptureRelative[] captures = GetActions(length, getFinalSquaresSet, ExpandToCaptures);
            return captures;
        }

        /// <summary>
        /// Get all Captures or all moves.
        /// </summary>
        /// <typeparam name="T">Action.</typeparam>
        /// <param name="length">Max distance for action.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        /// <param name="expand">Get all actions leading up to the final squares.</param>
        private static T[] GetActions<T>(
            int length,
            Func<int, List<List<SquareRelative>>> getFinalSquaresSet,
            Func<List<SquareRelative>, List<T>> expand)
        {
            var actionsList = new List<T>();
            List <List<SquareRelative>> finalSquaresSet = getFinalSquaresSet(length);
            foreach (var finalSquares in finalSquaresSet)
            {
                List<T> actions = expand(finalSquares);
                actionsList.AddRange(actions);
            }
            var actionsArray = actionsList.ToArray();
            return actionsArray;
        }

        /// <summary>
        /// Expand the squares along a direction into moves. One move for each square.
        /// </summary>
        /// <param name="longestMove">All squares leading to the final square.</param>
        private static List<SquareRelative[]> ExpandToMoves(List<SquareRelative> longestMove)
        {
            var expansions = new List<SquareRelative[]>();
            for (var i = 1; i <= longestMove.Count; i++)
            {
                IEnumerable<SquareRelative> expansionEnumerable = longestMove.Take(i);
                var expansionArray = expansionEnumerable.ToArray();
                expansions.Add(expansionArray);
            }
            return expansions;
        }

        /// <summary>
        /// Expand the squares along a direction into Captures. One capture for each square.
        /// </summary>
        /// <param name="longestMove">All squares leading to the final square.</param>
        private static List<CaptureRelative> ExpandToCaptures(List<SquareRelative> longestMove)
        {
            var expansions = new List<CaptureRelative>();
            for (var i = 0; i < longestMove.Count; i++)
            {
                SquareRelative finalSquare = longestMove.ElementAt(i);
                IEnumerable<SquareRelative> passingSquaresEnumerable = longestMove.Take(i);
                var passingSquaresArray = passingSquaresEnumerable.ToArray();
                CaptureRelative capture = new CaptureRelative(finalSquare, passingSquaresArray);
                expansions.Add(capture);
            }
            return expansions;
        }

        /// <summary>
        /// Get a list of list of squares for horizontal and vertical directions. One list for each direction.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        private static List<List<SquareRelative>> GetHorizontalVerticalFinalSquaresSet(int length)
        {
            var finalSquaresSet = new List<List<SquareRelative>>();
            List<SquareRelative> North = GetFinalSquares(length, GetNorth);
            List<SquareRelative> South = GetFinalSquares(length, GetSouth);
            List<SquareRelative> East = GetFinalSquares(length, GetEast);
            List<SquareRelative> West = GetFinalSquares(length, GetWest);
            finalSquaresSet.Add(North);
            finalSquaresSet.Add(South);
            finalSquaresSet.Add(East);
            finalSquaresSet.Add(West);
            return finalSquaresSet;
        }

        /// <summary>
        /// Get a list of list of squares for diagonal directions. One list for each direction.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        private static List<List<SquareRelative>> GetDiagonalFinalSquaresSet(int length)
        {
            var finalSquaresSet = new List<List<SquareRelative>>();
            List<SquareRelative> NorthEastFinalSquares = GetFinalSquares(length, GetNorthEast);
            List<SquareRelative> NorthWestFinalSquares = GetFinalSquares(length, GetNorthWest);
            List<SquareRelative> SouthEastFinalSquares = GetFinalSquares(length, GetSouthEast);
            List<SquareRelative> SouthWestFinalSquares = GetFinalSquares(length, GetSouthWest);
            finalSquaresSet.Add(NorthEastFinalSquares);
            finalSquaresSet.Add(NorthWestFinalSquares);
            finalSquaresSet.Add(SouthEastFinalSquares);
            finalSquaresSet.Add(SouthWestFinalSquares);
            return finalSquaresSet;
        }

        /// <summary>
        /// Get all SquareChanges leading up to the last square in a direction.
        /// </summary>
        /// <param name="length">Distance of the last square.</param>
        /// <param name="getFinalSquare">Get the last square from the distance.</param>
        private static List<SquareRelative> GetFinalSquares(int length, Func<int, SquareRelative> getFinalSquare)
        {
            var finalSquares = new List<SquareRelative>();
            for (var i = 1; i <= length; i++)
            {
                SquareRelative finalSquare = getFinalSquare(i);
                finalSquares.Add(finalSquare);
            }
            return finalSquares;
        }

        /// <summary>
        /// Get the SquareChange in the north direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetNorth(int length)
        {
            var north = new SquareRelative(0, length);
            return north;
        }

        /// <summary>
        /// Get the SquareChange in the south direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetSouth(int length)
        {
            var south = new SquareRelative(0, -length);
            return south;
        }

        /// <summary>
        /// Get the SquareChange in the east direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetEast(int length)
        {
            var east = new SquareRelative(length, 0);
            return east;
        }

        /// <summary>
        /// Get the SquareChange in the west direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetWest(int length)
        {
            var west = new SquareRelative(-length, 0);
            return west;
        }

        /// <summary>
        /// Get the SquareChange in the northeast direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetNorthEast(int length)
        {
            var northEast = new SquareRelative(length, length);
            return northEast;
        }

        /// <summary>
        /// Get the SquareChange in the northwest direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetNorthWest(int length)
        {
            var northWest = new SquareRelative(-length, length);
            return northWest;
        }

        /// <summary>
        /// Get the SquareChange in the southeast direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetSouthEast(int length)
        {
            var southEast = new SquareRelative(length, -length);
            return southEast;
        }

        /// <summary>
        /// Get the SquareChange in the southwest direction.
        /// </summary>
        /// <param name="length"></param>
        private static SquareRelative GetSouthWest(int length)
        {
            var southWest = new SquareRelative(-length, -length);
            return southWest;
        }
    }
}
