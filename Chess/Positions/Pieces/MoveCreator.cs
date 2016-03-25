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
        /// <returns></returns>
        public static SquareChange[][] GetHorizontalVerticalMoves(int length)
        {
            SquareChange[][] moves = GetMoves(length, GetHorizontalVerticalFinalSquaresSet);
            return moves;
        }

        /// <summary>
        /// Get moves along the diagonal directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        /// <returns></returns>
        public static SquareChange[][] GetDiagonalMoves(int length)
        {
            SquareChange[][] moves = GetMoves(length, GetDiagonalFinalSquaresSet);
            return moves;
        }

        /// <summary>
        /// Get captures along the horizontal and vertical directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        /// <returns></returns>
        public static Capture[] GetHorizontalVerticalCaptures(int length)
        {
            Capture[] captures = GetCaptures(length, GetHorizontalVerticalFinalSquaresSet);
            return captures;
        }

        /// <summary>
        /// Get captures along the diagonal directions.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        /// <returns></returns>
        public static Capture[] GetDiagonalCaptures(int length)
        {
            Capture[] captures = GetCaptures(length, GetDiagonalFinalSquaresSet);
            return captures;
        }

        /// <summary>
        /// Get moves in a set of directions.
        /// </summary>
        /// <param name="length">Max distance for moves.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        /// <returns></returns>
        private static SquareChange[][] GetMoves(int length, Func<int, List<List<SquareChange>>> getFinalSquaresSet)
        {
            SquareChange[][] moves = GetActions(length, getFinalSquaresSet, ExpandToMoves);
            return moves;
        }

        /// <summary>
        /// Get Captures in a set of directions.
        /// </summary>
        /// <param name="length">Max distance for Captures.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        /// <returns></returns>
        private static Capture[] GetCaptures(int length, Func<int, List<List<SquareChange>>> getFinalSquaresSet)
        {
            Capture[] captures = GetActions(length, getFinalSquaresSet, ExpandToCaptures);
            return captures;
        }

        /// <summary>
        /// Get all Captures or all moves.
        /// </summary>
        /// <typeparam name="T">Action.</typeparam>
        /// <param name="length">Max distance for action.</param>
        /// <param name="getFinalSquaresSet">Get final squares in certain directions.</param>
        /// <param name="expand">Get all actions leading up to the final squares.</param>
        /// <returns></returns>
        private static T[] GetActions<T>(
            int length,
            Func<int, List<List<SquareChange>>> getFinalSquaresSet,
            Func<List<SquareChange>, List<T>> expand)
        {
            var actionsList = new List<T>();
            List <List<SquareChange>> finalSquaresSet = getFinalSquaresSet(length);
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
        /// <returns></returns>
        private static List<SquareChange[]> ExpandToMoves(List<SquareChange> longestMove)
        {
            var expansions = new List<SquareChange[]>();
            for (var i = 1; i <= longestMove.Count; i++)
            {
                IEnumerable<SquareChange> expansionEnumerable = longestMove.Take(i);
                var expansionArray = expansionEnumerable.ToArray();
                expansions.Add(expansionArray);
            }
            return expansions;
        }

        /// <summary>
        /// Expand the squares along a direction into Captures. One capture for each square.
        /// </summary>
        /// <param name="longestMove">All squares leading to the final square.</param>
        /// <returns></returns>
        private static List<Capture> ExpandToCaptures(List<SquareChange> longestMove)
        {
            var expansions = new List<Capture>();
            for (var i = 0; i < longestMove.Count; i++)
            {
                SquareChange finalSquare = longestMove.ElementAt(i);
                IEnumerable<SquareChange> passingSquaresEnumerable = longestMove.Take(i);
                var passingSquaresArray = passingSquaresEnumerable.ToArray();
                Capture capture = new Capture(finalSquare, passingSquaresArray);
                expansions.Add(capture);
            }
            return expansions;
        }

        /// <summary>
        /// Get a list of list of squares for horizontal and vertical directions. One list for each direction.
        /// </summary>
        /// <param name="length">Max distance along the directions.</param>
        /// <returns></returns>
        private static List<List<SquareChange>> GetHorizontalVerticalFinalSquaresSet(int length)
        {
            var finalSquaresSet = new List<List<SquareChange>>();
            List<SquareChange> North = GetFinalSquares(length, GetNorth);
            List<SquareChange> South = GetFinalSquares(length, GetSouth);
            List<SquareChange> East = GetFinalSquares(length, GetEast);
            List<SquareChange> West = GetFinalSquares(length, GetWest);
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
        /// <returns></returns>
        private static List<List<SquareChange>> GetDiagonalFinalSquaresSet(int length)
        {
            var finalSquaresSet = new List<List<SquareChange>>();
            List<SquareChange> NorthEastFinalSquares = GetFinalSquares(length, GetNorthEast);
            List<SquareChange> NorthWestFinalSquares = GetFinalSquares(length, GetNorthWest);
            List<SquareChange> SouthEastFinalSquares = GetFinalSquares(length, GetSouthEast);
            List<SquareChange> SouthWestFinalSquares = GetFinalSquares(length, GetSouthWest);
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
        /// <returns></returns>
        private static List<SquareChange> GetFinalSquares(int length, Func<int, SquareChange> getFinalSquare)
        {
            var finalSquares = new List<SquareChange>();
            for (var i = 1; i <= length; i++)
            {
                SquareChange finalSquare = getFinalSquare(i);
                finalSquares.Add(finalSquare);
            }
            return finalSquares;
        }

        /// <summary>
        /// Get the SquareChange in the north direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetNorth(int length)
        {
            var north = new SquareChange(0, length);
            return north;
        }

        /// <summary>
        /// Get the SquareChange in the south direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetSouth(int length)
        {
            var south = new SquareChange(0, -length);
            return south;
        }

        /// <summary>
        /// Get the SquareChange in the east direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetEast(int length)
        {
            var east = new SquareChange(length, 0);
            return east;
        }

        /// <summary>
        /// Get the SquareChange in the west direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetWest(int length)
        {
            var west = new SquareChange(-length, 0);
            return west;
        }

        /// <summary>
        /// Get the SquareChange in the northeast direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetNorthEast(int length)
        {
            var northEast = new SquareChange(length, length);
            return northEast;
        }

        /// <summary>
        /// Get the SquareChange in the northwest direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetNorthWest(int length)
        {
            var northWest = new SquareChange(-length, length);
            return northWest;
        }

        /// <summary>
        /// Get the SquareChange in the southeast direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetSouthEast(int length)
        {
            var southEast = new SquareChange(length, -length);
            return southEast;
        }

        /// <summary>
        /// Get the SquareChange in the southwest direction.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static SquareChange GetSouthWest(int length)
        {
            var southWest = new SquareChange(-length, -length);
            return southWest;
        }
    }
}
