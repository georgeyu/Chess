using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position.Piece
{
    internal static class MoveCreator
    {
        public static SquareChange[][] GetHorizontalVerticalMoves(int length)
        {
            SquareChange[][] moves = GetMoves(length, GetHorizontalVerticalFinalSquaresSet);
            return moves;
        }

        public static SquareChange[][] GetDiagonalMoves(int length)
        {
            SquareChange[][] moves = GetMoves(length, GetDiagonalFinalSquaresSet);
            return moves;
        }

        public static Capture[] GetHorizontalVerticalCaptures(int length)
        {
            Capture[] captures = GetCaptures(length, GetHorizontalVerticalFinalSquaresSet);
            return captures;
        }

        public static Capture[] GetDiagonalCaptures(int length)
        {
            Capture[] captures = GetCaptures(length, GetDiagonalFinalSquaresSet);
            return captures;
        }

        private static SquareChange[][] GetMoves(int length, Func<int, List<List<SquareChange>>> getFinalSquaresSet)
        {
            SquareChange[][] moves = GetActions(length, getFinalSquaresSet, ExpandToMoves);
            return moves;
        }

        private static Capture[] GetCaptures(int length, Func<int, List<List<SquareChange>>> getFinalSquaresSet)
        {
            Capture[] captures = GetActions(length, getFinalSquaresSet, ExpandToCaptures);
            return captures;
        }

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

        private static SquareChange GetNorth(int length)
        {
            var north = new SquareChange(0, length);
            return north;
        }

        private static SquareChange GetSouth(int length)
        {
            var south = new SquareChange(0, -length);
            return south;
        }

        private static SquareChange GetEast(int length)
        {
            var east = new SquareChange(length, 0);
            return east;
        }

        private static SquareChange GetWest(int length)
        {
            var west = new SquareChange(-length, 0);
            return west;
        }

        private static SquareChange GetNorthEast(int length)
        {
            var northEast = new SquareChange(length, length);
            return northEast;
        }

        private static SquareChange GetNorthWest(int length)
        {
            var northWest = new SquareChange(-length, length);
            return northWest;
        }

        private static SquareChange GetSouthEast(int length)
        {
            var southEast = new SquareChange(length, -length);
            return southEast;
        }

        private static SquareChange GetSouthWest(int length)
        {
            var southWest = new SquareChange(-length, -length);
            return southWest;
        }
    }
}
