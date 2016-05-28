using Chess.Game;
using Chess.Game.Moves;
using Chess.Game.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChessTest
{
    [TestClass]
    public class MoveGetterTest
    {
        private const int StartMoveCount = 20;
        private const int CastleLegalTestMoveCount = 29;
        private const int PromoteLegalTestMoveCount = 31;
        private readonly TestUtil testUtil;

        public MoveGetterTest()
        {
            testUtil = new TestUtil();
        }

        [TestMethod]
        public void GetMoves_Start_Count()
        {
            var position = new Position();
            List<Move> moves = position.GetMoves();
            Assert.IsTrue(moves.Count == StartMoveCount);
        }

        [TestMethod]
        public void GetMoves_InCheck_Count()
        {
            var position = new Position();
            var white1 = new EmptyMove(testUtil.GetSquaresFromStrings("e2", "e3"), false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(testUtil.GetSquaresFromStrings("f7", "f6"), false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(testUtil.GetSquaresFromStrings("d1", "e2", "f3", "g4", "h5"), false);
            white2.MakeMove(position);
            var moves = position.GetMoves();
            Assert.IsTrue(moves.Count == 1);
        }

        [TestMethod]
        public void GetMoves_CastleLegal_Count()
        {
            var position = new Position();
            var white1 = new EmptyMove(testUtil.GetSquaresFromStrings("e2", "e3"), false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(testUtil.GetSquaresFromStrings("a7", "a6"), false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(testUtil.GetSquaresFromStrings("f1", "e2"), false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(testUtil.GetSquaresFromStrings("b7", "b6"), false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(testUtil.GetSquaresFromStrings("g1", "f3"), false);
            white3.MakeMove(position);
            var black3 = new EmptyMove(testUtil.GetSquaresFromStrings("c7", "c6"), false);
            black3.MakeMove(position);
            List<Move> moves = position.GetMoves();
            Assert.IsTrue(moves.Count == CastleLegalTestMoveCount);
        }

        [TestMethod]
        public void GetMoves_PromoteLegal_Count()
        {
            var position = new Position();
            var white1 = new EmptyMove(testUtil.GetSquaresFromStrings("a2", "a3", "a4"), false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(testUtil.GetSquaresFromStrings("h7", "h6"), false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(testUtil.GetSquaresFromStrings("a4", "a5"), true);
            white2.MakeMove(position);
            var black2 = new EmptyMove(testUtil.GetSquaresFromStrings("h6", "h5"), true);
            black2.MakeMove(position);
            var white3 = new EmptyMove(testUtil.GetSquaresFromStrings("a5", "a6"), true);
            white3.MakeMove(position);
            var black3 = new EmptyMove(testUtil.GetSquaresFromStrings("h5", "h4"), true);
            black3.MakeMove(position);
            var white4 = new Capture(
                testUtil.GetSquaresFromStrings("a6", "b7"),
                true,
                position.Board[testUtil.GetSquareFromString("b7")] as Piece);
            white4.MakeMove(position);
            var black4 = new EmptyMove(testUtil.GetSquaresFromStrings("h4", "h3"), true);
            black4.MakeMove(position);
            List<Move> moves = position.GetMoves();
            Assert.IsTrue(moves.Count == PromoteLegalTestMoveCount);
        }
    }
}