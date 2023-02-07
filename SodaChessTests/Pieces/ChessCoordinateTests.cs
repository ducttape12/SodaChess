using SodaChess.Pieces;

namespace SodaChessTests.Pieces
{
    [TestClass]
    public class ChessCoordinateTests
    {
        [TestMethod]
        public void GivenTwoCoordinates_WhenCompletelyDifferent_ThenNotEqual()
        {
            var one = new ChessCoordinate("1", "A");
            var two = new ChessCoordinate("2", "B");

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoCoordinates_WhenOnlyRankEqual_ThenNotEqual()
        {
            var one = new ChessCoordinate("1", "A");
            var two = new ChessCoordinate("2", "A");

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoCoordinates_WhenOnlyFileEqual_ThenNotEqual()
        {
            var one = new ChessCoordinate("1", "A");
            var two = new ChessCoordinate("1", "B");

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoCoordinates_WhenFileAndRankEqual_ThenEqual()
        {
            var one = new ChessCoordinate("1", "A");
            var two = new ChessCoordinate("1", "A");

            Assert.AreEqual(one, two);
        }
    }
}