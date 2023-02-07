using SodaChess.MoveLogic;
using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class RookMoveLogicTests
    {
        [TestMethod]
        public void GivenRookInCenterOfBoard_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Rook, SideType.White));
            var logic = new RookMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(14, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "1")));
        }

        [TestMethod]
        public void GivenRookTopLeftOfBoard_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("A", "8");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Rook, SideType.White));
            var logic = new RookMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(14, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
        }

        [TestMethod]
        public void GivenRookBottomRightOfBoard_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("H", "1");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Rook, SideType.White));
            var logic = new RookMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(14, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
        }

        [TestMethod]
        public void GivenWhiteRook_WhenTwoWhiteAndTwoBlackPiecesExistInItsPath_ThenCanMoveToWhiteAndCaptureBlack()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "4"), new ChessPiece(PieceType.Queen, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "8"), new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new RookMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
        }

        [TestMethod]
        public void GivenBlackRook_WhenTwoWhiteAndTwoBlackPiecesExistInItsPath_ThenCanMoveToWhiteAndCaptureBlack()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "4"), new ChessPiece(PieceType.Queen, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "8"), new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new RookMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "2")));
        }
    }
}
