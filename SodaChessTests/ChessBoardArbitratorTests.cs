using SodaChess;
using SodaChess.Pieces;
using SodaChessTests.BaseTestLogic;
using System;

namespace SodaChessTests
{
    [TestClass]
    public class ChessBoardArbitratorTests : BaseChessTests
    {
        [TestMethod]
        public void GivenAChessBoard_WhenInitializedIsCalled_ThenTheBoardIsInitializedCorrectly()
        {
            // Arrange / Act
            var arbitrator = new ChessBoardArbitrator();

            // Assert all pieces set correctly
            Assert.AreEqual(new ChessPiece(PieceType.Rook, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("A", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Knight, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("B", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Bishop, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("C", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Queen, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("D", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.King, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("E", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Bishop, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("F", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Knight, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("G", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Rook, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("H", "8")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("A", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("B", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("C", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("D", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("E", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("F", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("G", "7")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.Black), arbitrator.GetPiece(new ChessCoordinate("H", "7")));

            Assert.AreEqual(new ChessPiece(PieceType.Rook, SideType.White), arbitrator.GetPiece(new ChessCoordinate("A", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Knight, SideType.White), arbitrator.GetPiece(new ChessCoordinate("B", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Bishop, SideType.White), arbitrator.GetPiece(new ChessCoordinate("C", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.King, SideType.White), arbitrator.GetPiece(new ChessCoordinate("E", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Queen, SideType.White), arbitrator.GetPiece(new ChessCoordinate("D", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Bishop, SideType.White), arbitrator.GetPiece(new ChessCoordinate("F", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Knight, SideType.White), arbitrator.GetPiece(new ChessCoordinate("G", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Rook, SideType.White), arbitrator.GetPiece(new ChessCoordinate("H", "1")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("A", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("B", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("C", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("D", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("E", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("F", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("G", "2")));
            Assert.AreEqual(new ChessPiece(PieceType.Pawn, SideType.White), arbitrator.GetPiece(new ChessCoordinate("H", "2")));

            // Assert remainder of board is empty
            foreach (var file in Coordinates.Files)
            {
                foreach (var rank in Coordinates.Ranks)
                {
                    if (rank == "8" || rank == "7" || rank == "2" || rank == "1")
                    {
                        continue;
                    }

                    Assert.IsNull(arbitrator.GetPiece(new ChessCoordinate(file, rank)));
                }
            }
        }

        [TestMethod]
        public void GivenBoard_WhenValidMoveIsMade_ThenValidMoveMadeAndSideSwitched()
        {
            var arbitrator = new ChessBoardArbitrator();
            var source = new ChessCoordinate("D", "2");
            var destination = new ChessCoordinate("D", "4");
            var piece = arbitrator.GetPiece(source);

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(source));
            Assert.IsNotNull(arbitrator.GetPiece((destination)));
            Assert.AreEqual(piece, arbitrator.GetPiece(destination));
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
        }

        [TestMethod]
        public void GivenBoard_WhenAPieceIsCaptured_ThenPieceIsRecordedAndMoveIsMade()
        {
            var board = new ChessBoard();
            var whiteQueen = new ChessPiece(PieceType.Queen, SideType.White);
            var blackPawn = new ChessPiece(PieceType.Pawn, SideType.Black);
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "4"), whiteQueen);
            board.SetPiece(new ChessCoordinate("E", "5"), blackPawn);
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("D", "4");
            var destination = new ChessCoordinate("E", "5");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(source));
            Assert.AreEqual(whiteQueen, arbitrator.GetPiece(destination));
            Assert.AreEqual(1, arbitrator.CapturedBlackPieces.Count);
            Assert.AreEqual(blackPawn, arbitrator.CapturedBlackPieces[0]);
            Assert.AreEqual(0, arbitrator.CapturedWhitePieces.Count);
        }

        [TestMethod]
        public void GivenBoard_WhenWhitePutsBlackIntoCheckmate_ThenCheckmateIsReturned()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("C", "1");
            var destination = new ChessCoordinate("C", "8");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.ValidBlackInCheckmate, result);
        }

        [TestMethod]
        public void GivenBoard_WhenWhitePutsBlackIntoCheckAndBlackCanMoveOut_ThenCheckIsReturned()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "6"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("C", "1");
            var destination = new ChessCoordinate("C", "8");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.ValidBlackInCheck, result);
        }

        [TestMethod]
        public void GivenBoard_WhenWhitePutsBlackIntoCheckAndBlackCanCapture_ThenCheckIsReturned()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "5"), new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("C", "1");
            var destination = new ChessCoordinate("C", "8");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.ValidBlackInCheck, result);
        }

        [TestMethod]
        public void GivenBoard_WhenWhitePutBlackIntoStalemate_ThenStalementIsReturned()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Queen, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("C", "1");
            var destination = new ChessCoordinate("C", "7");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.ValidStalemate, result);
        }

        [TestMethod]
        public void GivenBoard_WhenWhiteAttemptsToMoveIntoCheck_ThenMoveIsPrevented()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("B", "5"), new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var whitePawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(new ChessCoordinate("E", "2"), whitePawn);
            board.SetPiece(new ChessCoordinate("F", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("E", "2");
            var destination = new ChessCoordinate("E", "3");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(destination));
            var piece = arbitrator.GetPiece(source);
            Assert.AreEqual(whitePawn, piece);
            Assert.AreEqual(arbitrator.CurrentPlayerSide, SideType.White);
        }

        [TestMethod]
        public void GivenChessBoard_WhenWhiteAttemptsInvalidMove_ThenNoMoveIsMade()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var whitePawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(new ChessCoordinate("E", "2"), whitePawn);
            board.SetPiece(new ChessCoordinate("F", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("E", "2");
            var destination = new ChessCoordinate("E", "8");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(destination));
            var piece = arbitrator.GetPiece(source);
            Assert.AreEqual(whitePawn, piece);
            Assert.AreEqual(arbitrator.CurrentPlayerSide, SideType.White);
        }

        [TestMethod]
        public void GivenWhiteInCheck_WhenWhiteAttemptsToBlockCheck_ThenMoveIsMade()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var whitePawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(new ChessCoordinate("E", "2"), whitePawn);
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            var source = new ChessCoordinate("E", "2");
            var destination = new ChessCoordinate("E", "3");

            var result = arbitrator.MakeMove(source, destination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(source));
            var piece = arbitrator.GetPiece(destination);
            Assert.AreEqual(whitePawn, piece);
            Assert.AreEqual(arbitrator.CurrentPlayerSide, SideType.Black);
        }

        [TestMethod]
        public void GivenPawnOnRank7_WhenMovedToRank8_ThenReadyForPromotion()
        {
            var board = new ChessBoard();
            ChessCoordinate pawnSource = new ChessCoordinate("B", "7");
            ChessCoordinate pawnDestination = new ChessCoordinate("B", "8");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            var result = arbitrator.MakeMove(pawnSource, pawnDestination);

            Assert.AreEqual(MoveResult.PromotionInputNeeded, result);
            Assert.AreEqual(SideType.White, arbitrator.CurrentPlayerSide);
            Assert.IsNull(arbitrator.GetPiece(pawnSource));
            var destinationPiece = arbitrator.GetPiece(pawnDestination);
            Assert.IsNotNull(destinationPiece);
            Assert.AreEqual(pawn, destinationPiece);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToQueen_ThenPieceIsQueen()
        {
            PromotePieceTest(PieceType.Queen);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToBishop_ThenPieceIsBishop()
        {
            PromotePieceTest(PieceType.Bishop);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToKnight_ThenPieceIsKnight()
        {
            PromotePieceTest(PieceType.Knight);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToRook_ThenPieceIsRook()
        {
            PromotePieceTest(PieceType.Rook);
        }

        public void PromotePieceTest(PieceType destinationType)
        {
            var board = new ChessBoard();
            ChessCoordinate pawnSource = new ChessCoordinate("B", "7");
            ChessCoordinate pawnDestination = new ChessCoordinate("B", "8");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            arbitrator.MakeMove(pawnSource, pawnDestination);

            var result = arbitrator.PromotePiece(destinationType);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
            var destinationPiece = arbitrator.GetPiece(pawnDestination);
            Assert.IsNotNull(destinationPiece);
            Assert.AreNotEqual(pawn, destinationPiece);
            Assert.AreEqual(new ChessPiece(destinationType, SideType.White), destinationPiece);
            Assert.IsFalse(arbitrator.CapturedBlackPieces.Any());
            Assert.IsFalse(arbitrator.CapturedWhitePieces.Any());
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToKing_ThenPromotionPrevented()
        {
            var board = new ChessBoard();
            ChessCoordinate pawnSource = new ChessCoordinate("B", "7");
            ChessCoordinate pawnDestination = new ChessCoordinate("B", "8");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            arbitrator.MakeMove(pawnSource, pawnDestination);

            var result = arbitrator.PromotePiece(PieceType.King);

            Assert.AreEqual(MoveResult.InvalidCannotPromotePiece, result);
            Assert.AreEqual(SideType.White, arbitrator.CurrentPlayerSide);
            var destinationPiece = arbitrator.GetPiece(pawnDestination);
            Assert.IsNotNull(destinationPiece);
            Assert.AreEqual(pawn, destinationPiece);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedToPawn_ThenPromotionPrevented()
        {
            var board = new ChessBoard();
            ChessCoordinate pawnSource = new ChessCoordinate("B", "7");
            ChessCoordinate pawnDestination = new ChessCoordinate("B", "8");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            arbitrator.MakeMove(pawnSource, pawnDestination);

            var result = arbitrator.PromotePiece(PieceType.Pawn);

            Assert.AreEqual(MoveResult.InvalidCannotPromotePiece, result);
            Assert.AreEqual(SideType.White, arbitrator.CurrentPlayerSide);
            var destinationPiece = arbitrator.GetPiece(pawnDestination);
            Assert.IsNotNull(destinationPiece);
            Assert.AreEqual(pawn, destinationPiece);
        }

        [TestMethod]
        public void GivenPawnReadyForPromotion_WhenPromotedIntoCheckmate_ThenReturnsCheckmate()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("B", "7");
            var pawnDestination = new ChessCoordinate("B", "8");
            board.SetPiece(pawnSource, new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Rook, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            arbitrator.MakeMove(pawnSource, pawnDestination);

            var result = arbitrator.PromotePiece(PieceType.Queen);

            Assert.AreEqual(MoveResult.ValidBlackInCheckmate, result);
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
        }

        [TestMethod]
        public void GivenBoard_WhenNoPieceReadyForPromotion_ThenPromotionPrevented()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            var result = arbitrator.PromotePiece(PieceType.Queen);

            Assert.AreEqual(MoveResult.InvalidCannotPromotePiece, result);
        }

        [TestMethod]
        public void GivenPiecePromoted_WhenAttemptingToPromoteAgain_ThenPromotionPrevented()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("B", "7");
            var pawnDestination = new ChessCoordinate("B", "8");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);
            arbitrator.MakeMove(pawnSource, pawnDestination);
            arbitrator.PromotePiece(PieceType.Queen);

            var result = arbitrator.PromotePiece(PieceType.Rook);

            Assert.AreEqual(MoveResult.InvalidCannotPromotePiece, result);
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
            var destinationPiece = arbitrator.GetPiece(pawnDestination);
            Assert.AreEqual(new ChessPiece(PieceType.Queen, SideType.White), destinationPiece);
        }

        [TestMethod]
        public void GivenWhitePawnOnStart_WhenMovedOneFile_ThenNotAvailableForEnPassant()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("E", "2");
            var pawnDestination = new ChessCoordinate("E", "3");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            arbitrator.MakeMove(pawnSource, pawnDestination);

            Assert.IsNull(board.AvailableForEnPassant);
        }

        [TestMethod]
        public void GivenWhitePawnOnStart_WhenMovedTwoFiles_ThenAvailableForEnPassant()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("E", "2");
            var pawnDestination = new ChessCoordinate("E", "4");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.White);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            arbitrator.MakeMove(pawnSource, pawnDestination);

            Assert.IsNotNull(board.AvailableForEnPassant);
            Assert.AreEqual(new ChessCoordinate("E", "3"), board.AvailableForEnPassant);
        }

        [TestMethod]
        public void GivenBlackPawnOnStart_WhenMovedOneFile_ThenNotAvailableForEnPassant()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("E", "7");
            var pawnDestination = new ChessCoordinate("E", "6");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.Black);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.Black);

            arbitrator.MakeMove(pawnSource, pawnDestination);

            Assert.IsNull(board.AvailableForEnPassant);
        }

        [TestMethod]
        public void GivenBlackPawnOnStart_WhenMovedTwoFiles_ThenAvailableForEnPassant()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("E", "7");
            var pawnDestination = new ChessCoordinate("E", "5");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.Black);
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.Black);

            arbitrator.MakeMove(pawnSource, pawnDestination);

            Assert.IsNotNull(board.AvailableForEnPassant);
            Assert.AreEqual(new ChessCoordinate("E", "6"), board.AvailableForEnPassant);
        }

        [TestMethod]
        public void GivenAvailableForEnPassant_WhenPieceMoved_ThenNotAvailableForEnPassant()
        {
            var board = new ChessBoard();
            var pawnSource = new ChessCoordinate("E", "7");
            var pawnDestination = new ChessCoordinate("E", "5");
            var pawn = new ChessPiece(PieceType.Pawn, SideType.Black);
            var whiteKingSource = new ChessCoordinate("H", "1");
            board.SetPiece(pawnSource, pawn);
            board.SetPiece(whiteKingSource, new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.Black);
            
            arbitrator.MakeMove(pawnSource, pawnDestination);
            arbitrator.MakeMove(whiteKingSource, new ChessCoordinate("H", "2"));

            Assert.IsNull(board.AvailableForEnPassant);
        }

        [TestMethod]
        public void GivenKingCapturesLastOpponentPiece_WhenOnlyKingsRemain_ThenResultIsStalemate()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var blackRookCoordinate = new ChessCoordinate("F", "2");
            var whiteKingCoordinate = new ChessCoordinate("E", "1");
            board.SetPiece(blackRookCoordinate, new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(whiteKingCoordinate, new ChessPiece(PieceType.King, SideType.White));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            var result = arbitrator.MakeMove(whiteKingCoordinate, blackRookCoordinate);

            Assert.AreEqual(MoveResult.ValidStalemate, result);
        }

        [TestMethod]
        public void GivenNoPawnMovedOrPieceCapturedIn99Moves_WhenNoPawnMovedOrPieceCaptured_ThenResultIsStalemate()
        {
            var arbitrator = new ChessBoardArbitrator();
            for (var i = 0; i < 24; i++)
            {
                MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "G1", // White knight
                    "C6", "B8"  // Black knight
                );
            }
            MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "G1"  // White knight
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("C", "6"), new ChessCoordinate("B", "8"));

            Assert.AreEqual(MoveResult.ValidStalemate, result);
        }

        [TestMethod]
        public void Given98MovesWithNoPawnOrCaptureAnd99MovePawn_WhenNoPawnMovedOrPieceCaptured_ThenResultIsValid()
        {
            var arbitrator = new ChessBoardArbitrator();
            for (var i = 0; i < 24; i++)
            {
                MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "G1", // White knight
                    "C6", "B8"  // Black knight
                );
            }
            MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "G1", // White knight
                    "A7", "A6"  // Black pawn
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("G", "1"), new ChessCoordinate("F", "3"));

            Assert.AreEqual(MoveResult.Valid, result);
        }

        [TestMethod]
        public void Given98MovesWithNoPawnOrCaptureAnd99Capture_WhenNoPawnMovedOrPieceCaptured_ThenResultIsValid()
        {
            var arbitrator = new ChessBoardArbitrator();
            for (var i = 0; i < 24; i++)
            {
                MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "G1", // White knight
                    "C6", "B8"  // Black knight
                );
            }
            MakeMoves(arbitrator,
                    "G1", "F3", // White knight
                    "B8", "C6", // Black knight
                    "F3", "E5", // White knight
                    "C6", "E5"  // Black knight captures white knight
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("B", "1"), new ChessCoordinate("C", "3"));

            Assert.AreEqual(MoveResult.Valid, result);
        }

        [TestMethod]
        public void GivenArbitrator_WhenNewArbitratorCreated_ThenSideValuesAreCorrect()
        {
            var arbitrator = new ChessBoardArbitrator();

            Assert.AreEqual(39, arbitrator.BlackScore);
            Assert.AreEqual(39, arbitrator.WhiteScore);
        }

        [TestMethod]
        public void GivenArbitrator_WhenMoveWithNoCapture_ThenSideValuesRemainUnchanged()
        {
            var arbitrator = new ChessBoardArbitrator();

            MakeMoves(arbitrator, "E2", "E4");

            Assert.AreEqual(39, arbitrator.BlackScore);
            Assert.AreEqual(39, arbitrator.WhiteScore);
        }

        [TestMethod]
        public void GivenArbitrator_WhenCaptureOccurs_ThenSideValuesUpdateAccording()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "4"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "5"), new ChessPiece(PieceType.Pawn, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            MakeMoves(arbitrator, "E4", "D5");

            Assert.AreEqual(0, arbitrator.BlackScore);
            Assert.AreEqual(1, arbitrator.WhiteScore);
        }

        [TestMethod]
        public void GivenArbitrator_WhenPawnPromotionOccurs_ThenSideValueIs9()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            var arbitrator = new ChessBoardArbitrator(board, SideType.White);

            arbitrator.MakeMove(new ChessCoordinate("A", "7"), new ChessCoordinate("A", "8"));
            arbitrator.PromotePiece(PieceType.Queen);

            Assert.AreEqual(0, arbitrator.BlackScore);
            Assert.AreEqual(9, arbitrator.WhiteScore);
        }
    }
}
