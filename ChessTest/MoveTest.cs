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
        private readonly Dictionary<string, BoardVector> squareByString;

        public MoveTest()
        {
            squareByString = SquareStringGenerator.GenerateSquareStrings();
        }

        [TestMethod]
        public void MakeMove_RuyLopez_Fen()
        {
            var position = new Position();
            var white1 = new EmptyMove(
                new List<BoardVector>() { squareByString["e2"], squareByString["e3"], squareByString["e4"] },
                false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(
                new List<BoardVector>() { squareByString["e7"], squareByString["e6"], squareByString["e5"] },
                false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(
                new List<BoardVector>() { squareByString["g1"], squareByString["f3"] },
                false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(
                new List<BoardVector>() { squareByString["b8"], squareByString["c6"] },
                false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(
                new List<BoardVector>()
                {
                    squareByString["f1"],
                    squareByString["e2"],
                    squareByString["d3"],
                    squareByString["c4"],
                    squareByString["b5"] },
                false);
            white3.MakeMove(position);
            string fen = position.GetFen();
            Assert.IsTrue(fen == RuyLopezFen);
        }

        [TestMethod]
        public void MakeMove_Najdorf_Fen()
        {
            var position = new Position();
            var white1 = new EmptyMove(
                new List<BoardVector>() { squareByString["e2"], squareByString["e3"], squareByString["e4"] },
                false);
            white1.MakeMove(position);
            var black1 = new EmptyMove(
                new List<BoardVector>() { squareByString["c7"], squareByString["c6"], squareByString["c5"] },
                false);
            black1.MakeMove(position);
            var white2 = new EmptyMove(new List<BoardVector>() { squareByString["g1"], squareByString["f3"] }, false);
            white2.MakeMove(position);
            var black2 = new EmptyMove(new List<BoardVector>() { squareByString["d7"], squareByString["d6"] }, false);
            black2.MakeMove(position);
            var white3 = new EmptyMove(
                new List<BoardVector>() { squareByString["d2"], squareByString["d3"], squareByString["d4"] },
                false);
            white3.MakeMove(position);
            var black3 = new Capture(
                new List<BoardVector>() { squareByString["c5"], squareByString["d4"] },
                true,
                position.Board[3, 3] as Piece);
            black3.MakeMove(position);
            var white4 = new Capture(
                new List<BoardVector>() { squareByString["f3"], squareByString["d4"] },
                true,
                position.Board[3, 3] as Piece);
            white4.MakeMove(position);
            var black4 = new EmptyMove(new List<BoardVector>() { squareByString["g8"], squareByString["f6"] }, false);
            black4.MakeMove(position);
            var white5 = new EmptyMove(new List<BoardVector>() { squareByString["b1"], squareByString["c3"] }, false);
            white5.MakeMove(position);
            var black5 = new EmptyMove(new List<BoardVector>() { squareByString["a7"], squareByString["a6"] }, false);
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
