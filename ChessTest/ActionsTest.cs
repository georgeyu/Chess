using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Game;
using Chess.Game.Moves;
using System.Collections.Generic;
using Chess;

namespace ChessTest
{
    [TestClass]
    public class ActionsTest
    {
        private const string Files = "abcdefgh";
        private const int PawnMoves = 2;
        private const int MovesAtStart = 20;
        private readonly Dictionary<string, SquareAbsolute> squareByString;

        public ActionsTest()
        {
            var squareByString = SquareStringGenerator.GenerateSquaresByString();
            this.squareByString = squareByString;
        }

        [TestMethod]
        public void ArePassingSquaresEmpty_A1_False()
        {
            var position = new Position();
            var passingSquares = new List<SquareAbsolute> { squareByString["a1"] };
            bool arePassingSquaresEmpty = MovesUtil.ArePassingSquaresEmpty(passingSquares, position.Board);
            Assert.IsFalse(arePassingSquaresEmpty);
        }

        [TestMethod]
        public void ArePassingSquaresEmpty_B3_True()
        {
            var Position = new Position();
            var passingSquares = new List<SquareAbsolute> { squareByString["b3"] };
            bool arePassingSquaresEmpty = MovesUtil.ArePassingSquaresEmpty(passingSquares, Position.Board);
            Assert.IsTrue(arePassingSquaresEmpty);
        }

        [TestMethod]
        public void AreSquaresOnBoard_A1B2_True()
        {
            var squares = new List<SquareAbsolute> { squareByString["a1"], squareByString["b2"] };
            bool areSquaresOnBoard = MovesUtil.AreSquaresOnBoard(
                squares,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsTrue(areSquaresOnBoard);
        }

        [TestMethod]
        public void AreSquaresOnBoard_I1A9_False()
        {
            var squares = new List<SquareAbsolute> { new SquareAbsolute(8, 0), new SquareAbsolute(0, 8) };
            bool areSquaresOnBoard = MovesUtil.AreSquaresOnBoard(
                squares,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsFalse(areSquaresOnBoard);
        }

        [TestMethod]
        public void IsSquareOnBoard_A1_True()
        {
            bool isSquareOnBoard = MovesUtil.IsSquareOnBoard(
                squareByString["a1"],
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsTrue(isSquareOnBoard);
        }

        [TestMethod]
        public void IsSquareOnBoard_I1_False()
        {
            var square = new SquareAbsolute(8, 0);
            bool isSquareOnBoard = MovesUtil.IsSquareOnBoard(
                square,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsFalse(isSquareOnBoard);
        }

        [TestMethod]
        public void GetMoves_StartBoard_Count()
        {
            var position = new Position();
            var moves = MoveGetter.GetMoves(position);
            Assert.IsTrue(moves.Count == MovesAtStart);
        }

        [TestMethod]
        public void GetCaptures_StartBoard_Count()
        {
            var position = new Position();
            var captures = CaptureGetter.GetCaptures(position);
            Assert.IsTrue(captures.Count == 0);
        }
    }
}
