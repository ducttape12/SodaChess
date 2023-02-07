using SodaChess.Pieces;

namespace SodaChessTests.Pieces
{
    [TestClass]
    public class ChessPieceTests
    {
        [TestMethod]
        public void GivenTwoPieces_WhenCompletelyDifferent_ThenNotEqual()
        {
            var one = new ChessPiece(PieceType.Bishop, SideType.White);
            var two = new ChessPiece(PieceType.King, SideType.Black);

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoPieces_WhenOnlyPieceTypeEqual_ThenNotEqual()
        {
            var one = new ChessPiece(PieceType.Bishop, SideType.White);
            var two = new ChessPiece(PieceType.Bishop, SideType.Black);

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoPieces_WhenOnlySideTypeEqual_ThenNotEqual()
        {
            var one = new ChessPiece(PieceType.Bishop, SideType.White);
            var two = new ChessPiece(PieceType.King, SideType.White);

            Assert.AreNotEqual(one, two);
        }

        [TestMethod]
        public void GivenTwoPieces_WhenPieceTypeAndSideTypeSame_ThenEqual()
        {
            var one = new ChessPiece(PieceType.Bishop, SideType.White);
            var two = new ChessPiece(PieceType.Bishop, SideType.White);

            Assert.AreEqual(one, two);
        }

        [TestMethod]
        public void GivenChessPiece_WhenCopyConstructorCalled_ThenCopied()
        {
            var original = new ChessPiece(PieceType.Pawn, SideType.White);

            var copy = new ChessPiece(original);

            Assert.AreEqual(original, copy);
        }
    }
}