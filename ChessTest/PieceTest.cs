using Chess.Position;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTest
{
    [TestClass]
    public class PieceTest
    {
        private const int KingMoves = 8;
        private const int KingCaptures = 8;

        [TestMethod]
        public void KingGetMoves_None_Count()
        {
            var king = new King(true, false);
            SquareChange[][] moves = king.GetMoves();
            Assert.IsTrue(moves.Length == KingMoves);
        }

        [TestMethod]
        public void KingGetCaptures_None_Count()
        {
            var king = new King(true, false);
            Capture[] captures = king.GetCaptures();
            Assert.IsTrue(captures.Length == KingMoves);
        }
    }
}
