using SodaChess.MoveLogic;
using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests.MoveLogic
{
    [TestClass]
    public class PawnMoveLogicTests
    {
        [TestMethod]
        public void GivenWhitePawnOnStartingRank_WhenNoPiecesInFrontOfIt_ThenCanMoveOneOrTwoRanks()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "3")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "4")));
        }

        [TestMethod]
        public void GivenWhitePawnOnStartingRank_WhenAPieceTwoSpacesInFrontOfIt_ThenCanMoveOneRank()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "3")));
        }

        [TestMethod]
        public void GivenWhitePawnOnStartingRank_WhenAPieceOneSpaceInFrontOfIt_ThenNoValidMoves()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GivenWhitePawnOnSomeRank_WhenNoPieceInFrontOfIt_ThenCanMoveOneRank()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "8")));
        }

        [TestMethod]
        public void GivenWhitePawnOnLastRank_ThenNoValidMoves()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "8");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GivenWhitePawnWithPieceInFront_WhenBlackPieceDiagonallyRight_ThenCanCapture()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            var blackPieceCoordinates = new ChessCoordinate("C", "3");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(blackPieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(blackPieceCoordinates));
        }

        [TestMethod]
        public void GivenWhitePawnWithPieceInFront_WhenBlackPieceDiagonallyLeft_ThenCanCapture()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            var blackPieceCoordinates = new ChessCoordinate("A", "3");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(blackPieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(blackPieceCoordinates));
        }

        [TestMethod]
        public void GivenWhitePawnWithPieceInFront_WhenBlackPieceDiagonallyLeftAndRight_ThenCanCaptureBoth()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            var blackPiece1Coordinates = new ChessCoordinate("A", "3");
            var blackPiece2Coordinates = new ChessCoordinate("C", "3");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(blackPiece1Coordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(blackPiece2Coordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(blackPiece1Coordinates));
            Assert.IsTrue(moves.Contains(blackPiece2Coordinates));
        }



        [TestMethod]
        public void GivenBlackPawnOnStartingRank_WhenNoPiecesInFrontOfIt_ThenCanMoveOneOrTwoRanks()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "5")));
        }

        [TestMethod]
        public void GivenBlackPawnOnStartingRank_WhenAPieceTwoSpacesInFrontOfIt_ThenCanMoveOneRank()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "6")));
        }

        [TestMethod]
        public void GivenBlackPawnOnStartingRank_WhenAPieceOneSpaceInFrontOfIt_ThenNoValidMoves()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GivenBlackPawnOnSomeRank_WhenNoPieceInFrontOfIt_ThenCanMoveOneRank()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "2");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(new ChessCoordinate("B", "1")));
        }

        [TestMethod]
        public void GivenBlackPawnOnFirstRank_ThenNoValidMoves()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "1");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GivenBlackPawnWithPieceInFront_WhenWhitePieceDiagonallyRight_ThenCanCapture()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            var whitePieceCoordinates = new ChessCoordinate("C", "6");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(whitePieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(whitePieceCoordinates));
        }

        [TestMethod]
        public void GivenBlackPawnWithPieceInFront_WhenWhitePieceDiagonallyLeft_ThenCanCapture()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            var whitePieceCoordinates = new ChessCoordinate("A", "6");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(whitePieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(whitePieceCoordinates));
        }

        [TestMethod]
        public void GivenBlackPawnWithPieceInFront_WhenWhitePieceDiagonallyLeftAndRight_ThenCanCaptureBoth()
        {
            var board = new ChessBoard();
            var pieceCoordinates = new ChessCoordinate("B", "7");
            var whitePiece1Coordinates = new ChessCoordinate("A", "6");
            var whitePiece2Coordinates = new ChessCoordinate("C", "6");
            board.SetPiece(pieceCoordinates, new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(whitePiece1Coordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(whitePiece2Coordinates, new ChessPiece(PieceType.Pawn, SideType.White));
            var logic = new PawnMoveLogic(board, pieceCoordinates);

            var moves = logic.GetMoveList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(whitePiece1Coordinates));
            Assert.IsTrue(moves.Contains(whitePiece2Coordinates));
        }
    }
}
