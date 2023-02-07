using SodaChess.MoveLogic;
using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class KnightMoveLogicTests
    {
        [TestMethod]
        public void GivenKnightInCenterOfBoard_WhenNoPieces_ThenCanMoveEverywhere()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.Knight, SideType.Black));
            var logic = new KnightMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(8, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "6")));
        }

        [TestMethod]
        public void givenKnightInTopLeft_whenNoPieces_thenCanMoveEverywhere()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("A", "8");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.Knight, SideType.Black));
            var logic = new KnightMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
        }

        [TestMethod]
        public void givenKnightInBottomRight_whenNoPieces_thenCanMoveEverywhere()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("H", "1");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.Knight, SideType.Black));
            var logic = new KnightMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
        }

        [TestMethod]
        public void givenBlackKnight_whenSurroundedByPieces_thenCanJumpPiecesAndCaptureWhite()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("F", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new KnightMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "6")));
        }

        [TestMethod]
        public void givenWhiteKnight_whenSurroundedByPieces_thenCanJumpPiecesAndCaptureBlack()
        {
            var board = new ChessBoard();
            var chessCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(chessCoordinates, new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "5"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("F", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new KnightMoveLogic(board, chessCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "5")));
        }
    }
}
