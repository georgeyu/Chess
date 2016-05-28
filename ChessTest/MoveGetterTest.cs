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
        private readonly Dictionary<string, BoardVector> squareByString;

        public MoveGetterTest()
        {
            squareByString = SquareStringGenerator.GenerateSquareStrings();
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
            var white1 = new EmptyMove(new List<BoardVector>() { squareByString["e2"], squareByString["e3"] }, false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(new List<BoardVector>() { squareByString["f7"], squareByString["f6"] }, false);
            black1.MakeMove(position);
            var white2 = new EmptyMove
            (
                new List<BoardVector>()
                {
                    squareByString["d1"],
                    squareByString["e2"],
                    squareByString["f3"],
                    squareByString["g4"],
                    squareByString["h5"]
                },
                false
            );
            white2.MakeMove(position);
            var moves = position.GetMoves();
            Assert.IsTrue(moves.Count == 1);
        }

        [TestMethod]
        public void GetMoves_CastleLegal_Count()
        {
            var position = new Position();
            var white1 = new EmptyMove(new List<BoardVector>() { squareByString["e2"], squareByString["e3"] }, false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(new List<BoardVector>() { squareByString["a7"], squareByString["a6"] }, false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(new List<BoardVector>() { squareByString["f1"], squareByString["e2"] }, false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(new List<BoardVector>() { squareByString["b7"], squareByString["b6"] }, false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(new List<BoardVector>() { squareByString["g1"], squareByString["f3"] }, false);
            white3.MakeMove(position);
            var black3 = new EmptyMove(new List<BoardVector>() { squareByString["c7"], squareByString["c6"] }, false);
            black3.MakeMove(position);
            List<Move> moves = position.GetMoves();
            Assert.IsTrue(moves.Count == CastleLegalTestMoveCount);
        }
    }
}