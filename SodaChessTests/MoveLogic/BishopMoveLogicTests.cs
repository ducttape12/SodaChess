using SodaChess.MoveLogic;
using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class BishopMoveLogicTests
    {
        [TestMethod]
        public void GivenBishopInCenter_WhenNoOtherPieces_ThenBishopCanMoveDiagonally()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Bishop, SideType.White));
            var logic = new BishopMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(13, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
        }

        [TestMethod]
        public void GivenBishopInTopLeft_WhenNoOtherPieces_ThenBishopCanMoveDiagonally()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("A", "8");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Bishop, SideType.White));
            var logic = new BishopMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(7, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "1")));
        }

        [TestMethod]
        public void GivenBishopInBottomRight_WhenNoOtherPieces_ThenBishopCanMoveDiagonally()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("A", "1");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Bishop, SideType.White));
            var logic = new BishopMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(7, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
        }

        [TestMethod]
        public void GivenWhiteBishop_WhenOtherPiecesExistInItsPath_ThenBishopCanMoveToWhiteAndCaptureBlack()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Bishop, SideType.White));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "7"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new BishopMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
        }

        [TestMethod]
        public void GivenBlackBishop_WhenOtherPiecesExistInItsPath_ThenBishopCanMoveToBlackAndCaptureWhite()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "7"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new BishopMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
        }
    }
}
