using Chess.Game;
using Chess.Game.Moves;
using Chess.Game.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace ChessTest
{
    [TestClass]
    public class MoveTest
    {
        private const int RandomMoveCount = 100;
        private const int MaxCharInLine = 80;
        private const string NajdorfFen = "rnbqkb1r/1p2pppp/p2p1n2/8/3NP3/2N5/PPP2PPP/R1BQKB1R";
        private const string RuyLopezFen = "r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R";
        private const string RandomMovesFile = "RandomMoves.txt";
        private readonly TestUtil testUtil;

        public MoveTest()
        {
            testUtil = new TestUtil();
        }

        [TestMethod]
        public void MakeMove_RuyLopez_Fen()
        {
            var position = new Position();
            var white1 = new EmptyMove(testUtil.GetSquaresFromStrings("e2", "e3", "e4"), false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(testUtil.GetSquaresFromStrings("e7", "e6", "e5"), false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(testUtil.GetSquaresFromStrings("g1", "f3"), false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(testUtil.GetSquaresFromStrings("b8", "c6"), false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(testUtil.GetSquaresFromStrings("f1", "e2", "d3", "c4", "b5"), false);
            white3.MakeMove(position);
            string fen = position.GetFen();
            Assert.IsTrue(fen == RuyLopezFen);
        }

        [TestMethod]
        public void MakeMove_Najdorf_Fen()
        {
            var position = new Position();
            var white1 = new EmptyMove(testUtil.GetSquaresFromStrings("e2", "e3", "e4"), false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(testUtil.GetSquaresFromStrings("c7", "c6", "c5"), false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(testUtil.GetSquaresFromStrings("g1", "f3"), false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(testUtil.GetSquaresFromStrings("d7", "d6"), false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(testUtil.GetSquaresFromStrings("d2", "d3", "d4"), false);
            white3.MakeMove(position);
            var black3 = new Capture(testUtil.GetSquaresFromStrings("c5", "d4"), true, position.Board[3, 3] as Piece);
            black3.MakeMove(position);
            var white4 = new Capture(testUtil.GetSquaresFromStrings("f3", "d4"), true, position.Board[3, 3] as Piece);
            white4.MakeMove(position);
            var black4 = new EmptyMove(testUtil.GetSquaresFromStrings("g8", "f6"), false);
            black4.MakeMove(position);
            var white5 = new EmptyMove(testUtil.GetSquaresFromStrings("b1", "c3"), false);
            white5.MakeMove(position);
            var black5 = new EmptyMove(testUtil.GetSquaresFromStrings("a7", "a6"), false);
            black5.MakeMove(position);
            string fen = position.GetFen();
            Assert.AreEqual(fen, NajdorfFen);
        }

        [TestMethod]
        public void MakeMove_Random_Txt()
        {
            var position = new Position();
            var text = new List<string>();
            for (var i = 0; i < RandomMoveCount; i++)
            {
                var move = (i + 1).ToString();
                var filler = new string('-', MaxCharInLine - move.Length);
                text.Add(move + filler);
                string board = GetBoardString(position);
                text.Add(board + Environment.NewLine);
                var moves = position.GetMoves();
                if (moves.Count == 0)
                {
                    break;
                }
                var random = new Random();
                int index = random.Next(0, moves.Count - 1);
                moves[index].MakeMove(position);
            }
            File.WriteAllLines(RandomMovesFile, text);
        }

        private string GetBoardString(Position position)
        {
            string fen = position.GetFen();
            string rankSeparated = fen.Replace(Position.FenDelimiter, Environment.NewLine);
            var regex = new System.Text.RegularExpressions.Regex(@"[0-9]");
            return regex.Replace(rankSeparated, ReplaceEmptySpaceWithDigit);
        }

        private string ReplaceEmptySpaceWithDigit(System.Text.RegularExpressions.Match match)
        {
            var count = int.Parse(match.Value);
            return new string(' ', count);
        }
    }
}
