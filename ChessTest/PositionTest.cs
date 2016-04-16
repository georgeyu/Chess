using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Game;
using Chess;
using Chess.Game.Pieces;
using System.Collections;
using System.Collections.Generic;

namespace ChessTest
{
    [TestClass]
    public class PositionTest
    {
        private const int EmptySquareCount = Constants.BoardLength * 4;
        private const int PawnCount = Constants.BoardLength * 2;
        private const int DoublePieceCount = 2 * 2;
        private const int SinglePieceCount = 2;
        private const int StartBoardMoveCount = 20;
        private const string StartBoardFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private const string RuyLopezFen = "r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R";
        private const string files = "abcdefgh";
        private const string NajdorfFen = "rnbqkb1r/1p2pppp/p2p1n2/8/3NP3/2N5/PPP2PPP/R1BQKB1R";
        private readonly Dictionary<string, SquareAbsolute> squareByString = new Dictionary<string, SquareAbsolute>();

        public PositionTest()
        {
            for (int i = 0; i < files.Length; i++)
            {
                LoopSquaresByFile(i);
            }
        }

        [TestMethod]
        public void Position_Empty_BoardSize()
        {
            Position position = new Position();
            Assert.IsTrue(position.Board.Length == Constants.BoardLength * Constants.BoardLength);
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
            MoveAbsolute[] moves = MoveGetter.GetMoves(position);
            Assert.IsTrue(moves.Length == StartBoardMoveCount);
        }

        [TestMethod]
        public void PositionGetFen_StartBoard_GetFen()
        {
            Position position = new Position();
            string fen = FenGetter.GetFen(position);
            Assert.IsTrue(fen == StartBoardFen);
        }

        [TestMethod]
        public void PositionGetFen_RuyLopez_GetFen()
        {
            Position position = new Position();
            MoveAbsolute white1 = new MoveAbsolute(
                squareByString["e2"],
                new SquareAbsolute[] { squareByString["e3"], squareByString["e4"] });
            position.MakeMove(white1);
            MoveAbsolute black1 = new MoveAbsolute(
                squareByString["e7"],
                new SquareAbsolute[] { squareByString["e6"], squareByString["e5"] });
            position.MakeMove(black1);
            MoveAbsolute white2 = new MoveAbsolute(squareByString["g1"], new SquareAbsolute[] { squareByString["f3"] });
            position.MakeMove(white2);
            MoveAbsolute black2 = new MoveAbsolute(squareByString["b8"], new SquareAbsolute[] { squareByString["c6"] });
            position.MakeMove(black2);
            MoveAbsolute white3 = new MoveAbsolute(
                squareByString["f1"],
                new SquareAbsolute[] {
                    squareByString["e2"],
                    squareByString["d3"],
                    squareByString["c4"],
                    squareByString["b5"]
                });
            position.MakeMove(white3);
            string fen = FenGetter.GetFen(position);
            Assert.IsTrue(fen == RuyLopezFen);
        }

        [TestMethod]
        public void PositionGetFen_Najdorf_GetFen()
        {
            Position position = new Position();
            MoveAbsolute white1 = new MoveAbsolute(
                squareByString["e2"],
                new SquareAbsolute[] { squareByString["e3"], squareByString["e4"] });
            position.MakeMove(white1);
            MoveAbsolute black1 = new MoveAbsolute(
                squareByString["c7"],
                new SquareAbsolute[] { squareByString["c6"], squareByString["c5"] });
            position.MakeMove(black1);
            MoveAbsolute white2 = new MoveAbsolute(squareByString["g1"], new SquareAbsolute[] { squareByString["f3"] });
            position.MakeMove(white2);
            MoveAbsolute black2 = new MoveAbsolute(squareByString["d7"], new SquareAbsolute[] { squareByString["d6"] });
            position.MakeMove(black2);
            MoveAbsolute white3 = new MoveAbsolute(
                squareByString["d2"],
                new SquareAbsolute[] { squareByString["d3"], squareByString["d4"] });
            position.MakeMove(white3);
            CaptureAbsolute black3 = new CaptureAbsolute(
                squareByString["c5"],
                squareByString["d4"],
                new SquareAbsolute[] { });
            position.MakeCapture(black3);
            CaptureAbsolute white4 = new CaptureAbsolute(
                squareByString["f3"],
                squareByString["d4"],
                new SquareAbsolute[] { });
            position.MakeCapture(white4);
            MoveAbsolute black4 = new MoveAbsolute(squareByString["g8"], new SquareAbsolute[] { squareByString["f6"] });
            position.MakeMove(black4);
            MoveAbsolute white5 = new MoveAbsolute(squareByString["b1"], new SquareAbsolute[] { squareByString["c3"] });
            position.MakeMove(white5);
            MoveAbsolute black5 = new MoveAbsolute(squareByString["a7"], new SquareAbsolute[] { squareByString["a6"] });
            position.MakeMove(black5);
            string fen = FenGetter.GetFen(position);
            Assert.AreEqual(fen, NajdorfFen);
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

        private void LoopSquaresByFile(int file)
        {
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                string id = files[file] + (i + 1).ToString();
                squareByString[id] = new SquareAbsolute(file, i);
            }
        }
    }
}
