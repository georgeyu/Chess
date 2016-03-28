using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Positions;
using Chess;
using Chess.Positions.Pieces;
using System.Collections;

namespace ChessTest
{
    [TestClass]
    public class PositionTest
    {
        private const int EmptySquareCount = Constants.BoardDimension * 4;
        private const int PawnCount = Constants.BoardDimension * 2;
        private const int DoublePieceCount = 2 * 2;
        private const int SinglePieceCount = 2;
        private const int StartBoardMoveCount = 20;

        [TestMethod]
        public void Position_Empty_BoardSize()
        {
            Position position = new Position();
            Assert.IsTrue(position.Board.Length == Constants.BoardDimension * Constants.BoardDimension);
        }

        [TestMethod]
        public void Position_Empty_EmptySquaresCount()
        {
            CountSquares(EmptySquareCount, x => x is EmptySquare);
        }

        [TestMethod]
        public void Position_Empty_PawnCount()
        {
            CountSquares(PawnCount, x => x is Pawn);
        }

        [TestMethod]
        public void Position_Empty_KnightCount()
        {
            CountSquares(DoublePieceCount, x => x is Knight);
        }

        [TestMethod]
        public void Position_Empty_BishopCount()
        {
            CountSquares(DoublePieceCount, x => x is Bishop);
        }

        [TestMethod]
        public void Position_Empty_RookCount()
        {
            CountSquares(DoublePieceCount, x => x is Rook);
        }

        [TestMethod]
        public void Position_Empty_QueenCount()
        {
            CountSquares(SinglePieceCount, x => x is Queen);
        }

        [TestMethod]
        public void Position_Empty_KingCount()
        {
            CountSquares(SinglePieceCount, x => x is King);
        }

        [TestMethod]
        public void GetMoves_StartBoard_Count()
        {
            var position = new Position();
            MoveAbsolute[] moves = position.GetMoves();
            Assert.IsTrue(moves.Length == StartBoardMoveCount);
        }

        private void CountSquares(int expectedCount, Func<Square, bool> checkSquareType)
        {
            Position position = new Position();
            int count = 0;
            foreach (Square square in position.Board)
            {
                count = checkSquareType(square) ? count + 1 : count;
            }
            Assert.IsTrue(count == expectedCount);
        }
    }
}
