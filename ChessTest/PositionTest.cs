using Chess;
using Chess.Game;
using Chess.Game.Actions;
using Chess.Game.Pieces;
using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

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
        private const int RandomMoves = 20;
        private const int MaxCharInLine = 80;
        private const string StartBoardFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private const string RuyLopezFen = "r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R";
        private const string files = "abcdefgh";
        private const string NajdorfFen = "rnbqkb1r/1p2pppp/p2p1n2/8/3NP3/2N5/PPP2PPP/R1BQKB1R";
        private const string MakeActionRandomLogFile = "MakeAction_Random_Log.txt";
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Dictionary<string, SquareAbsolute> squareByString;

        public PositionTest()
        {
            var squareByString = SquareStringGenerator.GenerateSquaresByString();
            this.squareByString = squareByString;
            XmlConfigurator.Configure();
        }

        [TestMethod]
        public void FenGetterGetFen_StartBoard_GetFen()
        {
            Position position = new Position();
            string fen = FenGetter.GetFen(position);
            Assert.IsTrue(fen == StartBoardFen);
        }

        [TestMethod]
        public void MakeMove_RuyLopez_GetFen()
        {
            var position = new Position();
            var white1 = new MoveAbsolute(
                squareByString["e2"],
                new SquareAbsolute[] { squareByString["e3"], squareByString["e4"] });
            position.MakeMove(white1);
            var black1 = new MoveAbsolute(
                squareByString["e7"],
                new SquareAbsolute[] { squareByString["e6"], squareByString["e5"] });
            position.MakeMove(black1);
            var white2 = new MoveAbsolute(squareByString["g1"], new SquareAbsolute[] { squareByString["f3"] });
            position.MakeMove(white2);
            var black2 = new MoveAbsolute(squareByString["b8"], new SquareAbsolute[] { squareByString["c6"] });
            position.MakeMove(black2);
            var white3 = new MoveAbsolute(
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
        public void MakeCapture_Najdorf_GetFen()
        {
            Position position = new Position();
            var white1 = new MoveAbsolute(
                squareByString["e2"],
                new SquareAbsolute[] { squareByString["e3"], squareByString["e4"] });
            position.MakeMove(white1);
            var black1 = new MoveAbsolute(
                squareByString["c7"],
                new SquareAbsolute[] { squareByString["c6"], squareByString["c5"] });
            position.MakeMove(black1);
            var white2 = new MoveAbsolute(squareByString["g1"], new SquareAbsolute[] { squareByString["f3"] });
            position.MakeMove(white2);
            var black2 = new MoveAbsolute(squareByString["d7"], new SquareAbsolute[] { squareByString["d6"] });
            position.MakeMove(black2);
            var white3 = new MoveAbsolute(
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
            var black4 = new MoveAbsolute(squareByString["g8"], new SquareAbsolute[] { squareByString["f6"] });
            position.MakeMove(black4);
            var white5 = new MoveAbsolute(squareByString["b1"], new SquareAbsolute[] { squareByString["c3"] });
            position.MakeMove(white5);
            var black5 = new MoveAbsolute(squareByString["a7"], new SquareAbsolute[] { squareByString["a6"] });
            position.MakeMove(black5);
            string fen = FenGetter.GetFen(position);
            Assert.AreEqual(fen, NajdorfFen);
        }

        [TestMethod]
        public void MakeAction_Random_Log()
        {
            var position = new Position();
            var text = new List<string>();
            for (var i = 0; i < RandomMoves; i++)
            {
                string header = GetHeader(i + 1);
                text.Add(header);
                string board = GetBoardStringFromPosition(position);
                text.Add(board + Environment.NewLine);
                position = TakeRandomAction(position);
            }
            File.WriteAllLines(MakeActionRandomLogFile, text);
        }

        private string GetBoardStringFromPosition(Position position)
        {
            string fen = FenGetter.GetFen(position);
            string rankSeparated = fen.Replace(Constants.FenRankSeparator, Environment.NewLine);
            var regex = new Regex(@"[0-9]");
            string integersReplacedWithSpaces = regex.Replace(
                rankSeparated,
                delegate (Match match)
                {
                    var count = Int32.Parse(match.Value);
                    var spaces = new string(' ', count);
                    return spaces;
                });
            return integersReplacedWithSpaces;
        }

        private string GetHeader(int move)
        {
            var moveString = move.ToString();
            var filler = new string('-', MaxCharInLine - moveString.Length);
            var header = String.Format("{0}{1}", moveString, filler);
            return header;
        }

        private Position TakeRandomAction(Position position)
        {
            var moves = MoveGetter.GetMoves(position);
            var captures = CaptureGetter.GetCaptures(position);
            var random = new Random();
            int index = random.Next(0, moves.Length + captures.Length - 1);
            if (index < moves.Length)
            {
                position.MakeMove(moves[index]);
            }
            else
            {
                position.MakeCapture(captures[index - moves.Length]);
            }
            return position;
        }
    }
}
