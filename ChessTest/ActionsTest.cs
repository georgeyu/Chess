using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Game;
using Chess.Game.Actions;
using System.Collections.Generic;
using Chess;
using System.Linq;
using Chess.Game.Pieces;

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
            var passingSquares = new SquareAbsolute[] { squareByString["a1"] };
            bool arePassingSquaresEmpty = ActionGetterUtil.ArePassingSquaresEmpty(passingSquares, position.Board);
            Assert.IsFalse(arePassingSquaresEmpty);
        }

        [TestMethod]
        public void ArePassingSquaresEmpty_B3_True()
        {
            var Position = new Position();
            var passingSquares = new SquareAbsolute[] { squareByString["b3"] };
            bool arePassingSquaresEmpty = ActionGetterUtil.ArePassingSquaresEmpty(passingSquares, Position.Board);
            Assert.IsTrue(arePassingSquaresEmpty);
        }

        [TestMethod]
        public void AreSquaresOnBoard_A1B2_True()
        {
            var squares = new SquareAbsolute[] { squareByString["a1"], squareByString["b2"] };
            bool areSquaresOnBoard = ActionGetterUtil.AreSquaresOnBoard(
                squares,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsTrue(areSquaresOnBoard);
        }

        [TestMethod]
        public void AreSquaresOnBoard_I1A9_False()
        {
            var squares = new SquareAbsolute[] { new SquareAbsolute(8, 0), new SquareAbsolute(0, 8) };
            bool areSquaresOnBoard = ActionGetterUtil.AreSquaresOnBoard(
                squares,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsFalse(areSquaresOnBoard);
        }

        [TestMethod]
        public void IsSquareOnBoard_A1_True()
        {
            bool isSquareOnBoard = ActionGetterUtil.IsSquareOnBoard(
                squareByString["a1"],
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsTrue(isSquareOnBoard);
        }

        [TestMethod]
        public void IsSquareOnBoard_I1_False()
        {
            var square = new SquareAbsolute(8, 0);
            bool isSquareOnBoard = ActionGetterUtil.IsSquareOnBoard(
                square,
                Constants.BoardLength,
                Constants.BoardLength);
            Assert.IsFalse(isSquareOnBoard);
        }

        [TestMethod]
        public void GetActionsFromSquare_A2_Count()
        {
            var position = new Position();
            var file = 0;
            var rank = 1;
            var startSquare = new SquareAbsolute(file, rank);
            var moves = ActionGetterUtil.GetActionsFromSquare(
                position.Board[file, rank],
                file,
                rank,
                position.IsWhiteTurn,
                x => new MoveAbsolute(startSquare, new SquareAbsolute[] { }),
                x => new SquareChange[][] { });
            Assert.IsTrue(moves.Length == 0);
        }

        [TestMethod]
        public void GetActionsIgnoringLegality_StartBoard_Count()
        {
            var position = new Position();
            int files = position.Board.GetLength(Constants.FileIndex);
            int ranks = position.Board.GetLength(Constants.RankIndex);
            int[] actions = ActionGetterUtil.GetActionsIgnoringLegality(
                position.Board,
                position.IsWhiteTurn,
                files,
                ranks,
                (a, b, c, d) => new int[] { 0 });
            Assert.IsTrue(actions.Length == Constants.BoardLength * Constants.BoardLength);
        }

        [TestMethod]
        public void GetMoves_StartBoard_Count()
        {
            var position = new Position();
            var moves = MoveGetter.GetMoves(position);
            Assert.IsTrue(moves.Length == MovesAtStart);
        }

        [TestMethod]
        public void GetCaptures_StartBoard_Count()
        {
            var position = new Position();
            var captures = CaptureGetter.GetCaptures(position);
            Assert.IsTrue(captures.Length == 0);
        }
    }
}
