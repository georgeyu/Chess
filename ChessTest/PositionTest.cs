using Chess.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTest
{
    [TestClass]
    public class PositionTest
    {
        private const string StartBoardFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

        [TestMethod]
        public void GetFen_Start_Result()
        {
            var position = new Position();
            string fen = position.GetFen();
            Assert.IsTrue(fen == StartBoardFen);
        }
    }
}
