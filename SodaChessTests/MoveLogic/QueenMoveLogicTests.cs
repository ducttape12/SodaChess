using SodaChess;
using SodaChess.MoveLogic;
using SodaChess.Pieces;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class QueenMoveLogicTests
    {
        [TestMethod]
        public void GivenQueenInCenterOfBoard_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Queen, SideType.White));
            var logic = new QueenMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(27, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "4")));
        }

        [TestMethod]
        public void GivenQueenInTopLeft_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("A", "8");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Queen, SideType.White));
            var logic = new QueenMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(21, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "1")));
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
        public void GivenQueenInBottomLeft_WhenNoOtherPieces_ThenCanMoveInAllDirections()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("A", "1");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Queen, SideType.White));
            var logic = new QueenMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(21, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("A", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "1")));
        }

        [TestMethod]
        public void GivenWhiteQueen_WhenOtherPiecesExistInHerPath_ThenCanMoveToWhiteAndCaptureBlack()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Queen, SideType.White));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "8"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new QueenMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(17, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("H", "8")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
        }

        [TestMethod]
        public void GivenBlackQueen_WhenOtherPiecesExistInHerPath_ThenCanMoveToBlackAndCaptureWhite()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("D", "4");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "8"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "1"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new QueenMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(17, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("C", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "5")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "7")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "4")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("E", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("F", "2")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("G", "1")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("D", "2")));
        }
    }
}
