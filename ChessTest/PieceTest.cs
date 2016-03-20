﻿using Chess.Position;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;

namespace ChessTest
{
    [TestClass]
    public class PieceTest
    {
        private const int KingCount = 8;
        private const int QueenCount = 56;
        private const int RookCount = 28;
        private const int BishopCount = 28;
        private const int KnightCount = 8;
        private const int PawnMovesHasntMoved = 2;
        private const int PawnMovesHasMoved = 1;
        private const int PawnCaptures = 2;
        private const int HorizontalVerticalVectors = 4;
        private const int DiagonalVectors = 4;

        [TestMethod]
        public void KingGetMoves_None_Count()
        {
            var king = new King(true, false);
            TestMoves(king, KingCount);
        }

        [TestMethod]
        public void KingGetCaptures_None_Count()
        {
            var king = new King(true, false);
            TestCaptures(king, KingCount);
        }

        [TestMethod]
        public void QueenGetMoves_None_Count()
        {
            var queen = new Queen(true, false);
            TestMoves(queen, QueenCount);
        }

        [TestMethod]
        public void QueenGetCaptures_None_Count()
        {
            var queen = new Queen(true, false);
            TestCaptures(queen, QueenCount);
        }

        [TestMethod]
        public void RookGetMoves_None_Count()
        {
            var rook = new Rook(true, false);
            TestMoves(rook, RookCount);
        }

        [TestMethod]
        public void RookGetCaptures_None_Count()
        {
            var rook = new Rook(true, false);
            TestCaptures(rook, RookCount);
        }

        [TestMethod]
        public void BishopGetMoves_None_Count()
        {
            var bishop = new Bishop(true, false);
            TestMoves(bishop, BishopCount);
        }

        [TestMethod]
        public void BishopGetCaptures_None_Count()
        {
            var bishop = new Bishop(true, false);
            TestCaptures(bishop, BishopCount);
        }

        [TestMethod]
        public void KnightGetMoves_None_Count()
        {
            var knight = new Knight(true, false);
            TestMoves(knight, KnightCount);
        }

        [TestMethod]
        public void KnightGetCaptures_None_Count()
        {
            var knight = new Knight(true, false);
            TestCaptures(knight, KnightCount);
        }

        [TestMethod]
        public void PawnGetMoves_HasntMoved_Count()
        {
            var pawn = new Pawn(true, false);
            TestMoves(pawn, PawnMovesHasntMoved);
        }

        [TestMethod]
        public void PawnGetMoves_HasMoved_Count()
        {
            var pawn = new Pawn(true, true);
            TestMoves(pawn, PawnMovesHasntMoved);
        }

        [TestMethod]
        public void PawnGetCaptures_None_Count()
        {
            var pawn = new Pawn(true, false);
            TestCaptures(pawn, PawnCaptures);
        }

        [TestMethod]
        public void GetHorizontalVerticalMoves_MaxRange_Count()
        {
            SquareChange[][] moves = MoveCreator.GetHorizontalVerticalMoves(Constants.MaxMoveRange);
            Assert.IsTrue(moves.Length == Constants.MaxMoveRange * HorizontalVerticalVectors);
        }

        [TestMethod]
        public void GetDiagonalMoves_MaxRange_Count()
        {
            SquareChange[][] moves = MoveCreator.GetDiagonalMoves(Constants.MaxMoveRange);
            Assert.IsTrue(moves.Length == Constants.MaxMoveRange * DiagonalVectors);
        }

        [TestMethod]
        public void GetHorizontalVerticalCaptures_MaxRange_Count()
        {
            Capture[] captures = MoveCreator.GetHorizontalVerticalCaptures(Constants.MaxMoveRange);
            Assert.IsTrue(captures.Length == Constants.MaxMoveRange * HorizontalVerticalVectors);
        }

        [TestMethod]
        public void GetDiagonalCaptures_MaxRange_Count()
        {
            Capture[] captures = MoveCreator.GetDiagonalCaptures(Constants.MaxMoveRange);
            Assert.IsTrue(captures.Length == Constants.MaxMoveRange * DiagonalVectors);
        }

        private void TestMoves(Piece piece, int count)
        {
            SquareChange[][] moves = piece.GetMoves();
            Assert.IsTrue(moves.Length == count);
        }

        private void TestCaptures(Piece piece, int count)
        {
            Capture[] captures = piece.GetCaptures();
            Assert.IsTrue(captures.Length == count);
        }
    }
}
