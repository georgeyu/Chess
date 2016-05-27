using Chess.Game;
using Chess.Game.Moves;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTest
{
    [TestClass]
    public class MovesTest
    {
        private const int MovesAtStart = 20;

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
