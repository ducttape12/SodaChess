using SodaChess.MoveLogic;
using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class KingMoveLogicTests
    {
        [TestMethod]
        public void GivenKingInCenterOfBoard_WhenNoPieces_ThenCanMoveOneInEveryDirection()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.King, SideType.White));
            var logic = new KingMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
        }

        [TestMethod]
        public void GivenKingInTopLeftOfBoard_WhenNoPieces_ThenCanMoveOneInEveryDirection()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("A", "8");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.King, SideType.White));
            var logic = new KingMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "7")));
        }

        [TestMethod]
        public void GivenKingInBottomRightOfBoard_WhenNoPieces_ThenCanMoveOneInEveryDirection()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("H", "1");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.King, SideType.White));
            var logic = new KingMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "2")));
        }

        [TestMethod]
        public void GivenBlackKing_WhenSurroundedByPieces_ThenCaptureWhitePieces()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("C", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new KingMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
        }

        [TestMethod]
        public void GivenWhiteKing_WhenSurroundedByPieces_ThenCaptureBlackPieces()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("C", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new KingMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
        }
    }
}
