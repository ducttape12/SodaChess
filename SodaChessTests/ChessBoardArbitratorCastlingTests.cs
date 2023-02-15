using SodaChess;
using SodaChess.Pieces;
using SodaChessTests.BaseTestLogic;

namespace SodaChessTests
{
    [TestClass]
    public class ChessBoardArbitratorCastlingTests : BaseChessTests
    {
        private static ChessBoard InitializeKingAndRookBoard()
        {
            var board = new ChessBoard();
            board.SetPiece(new ChessCoordinate("A", "8"), new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.Rook, SideType.White));

            return board;
        }

        private static ChessBoard InitializeKingRookBishopBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Bishop, SideType.White));
            board.SetPiece(new ChessCoordinate("F", "1"), new ChessPiece(PieceType.Bishop, SideType.White));
            board.SetPiece(new ChessCoordinate("C", "8"), new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "8"), new ChessPiece(PieceType.Bishop, SideType.Black));

            return board;
        }

        private static ChessBoard InitializeKingRookKnightBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("B", "1"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "8"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.Knight, SideType.Black));

            return board;
        }

        private static ChessBoard InitializeKingRookKingInCheckBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("G", "3"), new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "6"), new ChessPiece(PieceType.Bishop, SideType.White));

            return board;
        }

        private static ChessBoard InitializeKingRookDFUnderAttackBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("E", "6"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "3"), new ChessPiece(PieceType.Knight, SideType.Black));

            return board;
        }

        private static ChessBoard InitializeKingRookCGUnderAttackBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "6"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "3"), new ChessPiece(PieceType.Knight, SideType.Black));

            return board;
        }

        private static ChessBoard InitializeKingRookWithRooksUnderAttackBoard()
        {
            var board = InitializeKingAndRookBoard();
            board.SetPiece(new ChessCoordinate("B", "3"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "3"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "6"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "6"), new ChessPiece(PieceType.Knight, SideType.White));

            return board;
        }

        private static ChessBoardArbitrator InitializeKingRookArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingAndRookBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookBishopArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookBishopBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookKnightArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookKnightBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookKingInCheckArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookKingInCheckBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookDFUnderAttackArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookDFUnderAttackBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookCGUnderAttackArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookCGUnderAttackBoard(), startingSide);
        }

        private static ChessBoardArbitrator InitializeKingRookWithRooksUnderAttackArbitrator(SideType startingSide)
        {
            return new ChessBoardArbitrator(InitializeKingRookWithRooksUnderAttackBoard(), startingSide);
        }

        private static void AssertExpectedLayout(ChessBoardArbitrator expected, ChessBoardArbitrator actual)
        {
            foreach(var rank in Coordinates.Ranks)
            {
                foreach(var file in Coordinates.Files)
                {
                    var expectedPiece = expected.GetPiece(new ChessCoordinate(file, rank));
                    var actualPiece = expected.GetPiece(new ChessCoordinate(file, rank));

                    Assert.AreEqual(expectedPiece, actualPiece, $"Pieces at {file}{rank} are not equal.");
                }
            }
        }

        [TestMethod]
        public void GivenWhiteKingHasMovedButNotWhiteRook_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            MakeMoves(arbitrator, 
                "E1", "E2", // White king
                "E8", "E7", // Black king
                "E2", "E1", // White king
                "E7", "E8"  // Black king
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingHasMovedButNotWhiteRook_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            MakeMoves(arbitrator,
                "E1", "E2", // White king
                "E8", "E7", // Black king
                "E2", "E1", // White king
                "E7", "E8"  // Black king
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenBlackKingHasMovedButNotBlackRook_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            MakeMoves(arbitrator,
                "E8", "E7", // Black king
                "E1", "E2", // White king
                "E7", "E8", // Black king
                "E2", "E1"  // White king
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenBlackKingHasMovedButNotBlackRook_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            MakeMoves(arbitrator,
                "E8", "E7", // Black king
                "E1", "E2", // White king
                "E7", "E8", // Black king
                "E2", "E1"  // White king
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingNotMovedButARookHas_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            MakeMoves(arbitrator,
                "A1", "A2", // Queen Side White Rook
                "A8", "A7", // Queen Side Black Rook
                "A2", "A1", // Queen Side White Rook
                "A7", "A8"  // Queen Side Black Rook
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingNotMovedButHRookHas_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            MakeMoves(arbitrator,
                "H1", "H2", // King Side White Rook
                "H8", "H7", // King Side Black Rook
                "H2", "H1", // King Side White Rook
                "H7", "H8"  // King Side Black Rook
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenBlackKingNotMovedButARookHas_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            MakeMoves(arbitrator,
                "A8", "A7", // Queen Side Black Rook
                "A1", "A2", // Queen Side White Rook
                "A7", "A8", // Queen Side Black Rook
                "A2", "A1"  // Queen Side White Rook
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenBlackKingNotMovedButHRookHas_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            MakeMoves(arbitrator,
                "H8", "H7", // King Side Black Rook
                "H1", "H2", // King Side White Rook
                "H7", "H8", // King Side Black Rook
                "H2", "H1"  // King Side White Rook
            );

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenKnightBetweenWhiteKingAndRook_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKnightArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKnightArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenKnightBetweenBlackKingAndRook_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKnightArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKnightArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenBishopBetweenWhiteKingAndRook_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookBishopArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookBishopArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenBishopBetweenBlackKingAndRook_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookBishopArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookBishopArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenKnightBetweenWhiteKingAndRook_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKnightArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKnightArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenKnightBetweenBlackKingAndRook_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKnightArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKnightArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenBishopBetweenWhiteKingAndRook_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookBishopArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookBishopArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenBishopBetweenBlackKingAndRook_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookBishopArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookBishopArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingInCheck_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKingInCheckArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKingInCheckArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingInCheck_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKingInCheckArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKingInCheckArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingInCheck_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKingInCheckArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKingInCheckArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenWhiteKingInCheck_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookKingInCheckArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookKingInCheckArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateBetweenKingAndRookAreUnderAttack_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookDFUnderAttackArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookDFUnderAttackArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateBetweenKingAndRookAreUnderAttack_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookDFUnderAttackArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookDFUnderAttackArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateBetweenKingAndRookAreUnderAttack_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookDFUnderAttackArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookDFUnderAttackArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateBetweenKingAndRookAreUnderAttack_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookDFUnderAttackArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookDFUnderAttackArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateAtKingDestinationIsUnderAttack_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookCGUnderAttackArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("C", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookCGUnderAttackArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateAtKingDestinationIsUnderAttack_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookCGUnderAttackArbitrator(SideType.White);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "1"), new ChessCoordinate("G", "1"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookCGUnderAttackArbitrator(SideType.White), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateAtKingDestinationIsUnderAttack_WhenAttemptingBlack000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookCGUnderAttackArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("C", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookCGUnderAttackArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenCoordinateAtKingDestinationIsUnderAttack_WhenAttemptingBlack00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookCGUnderAttackArbitrator(SideType.Black);

            var result = arbitrator.MakeMove(new ChessCoordinate("E", "8"), new ChessCoordinate("G", "8"));

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            AssertExpectedLayout(InitializeKingRookCGUnderAttackArbitrator(SideType.Black), arbitrator);
        }

        [TestMethod]
        public void GivenNoRookAtCastlingDestination_WhenAttemptingWhite000Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookWithRooksUnderAttackArbitrator(SideType.Black);
            MakeMoves(arbitrator, "B3", "A1"); // Black knight captures rook
            var kingSource = new ChessCoordinate("E", "1");
            var kingDestination = new ChessCoordinate("C", "1");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(king, arbitrator.GetPiece(kingSource));
        }

        [TestMethod]
        public void GivenNoRookAtCastlingDestination_WhenAttemptingWhite00Castle_ThenCannot()
        {
            var arbitrator = InitializeKingRookWithRooksUnderAttackArbitrator(SideType.Black);
            MakeMoves(arbitrator, "G3", "H1"); // Black knight captures rook
            var kingSource = new ChessCoordinate("E", "1");
            var kingDestination = new ChessCoordinate("G", "1");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(king, arbitrator.GetPiece(kingSource));
        }

        [TestMethod]
        public void GivenNoRookAtCastlingDestination_WhenAttemptingBlack000Castle_ThenCannot()
        {
            // TODO: Confirm if this test is actually working
            var arbitrator = InitializeKingRookWithRooksUnderAttackArbitrator(SideType.White);
            MakeMoves(arbitrator, "B6", "A8"); // White knight captures rook
            var kingSource = new ChessCoordinate("E", "8");
            var kingDestination = new ChessCoordinate("C", "8");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(king, arbitrator.GetPiece(kingSource));
        }

        [TestMethod]
        public void GivenNoRookAtCastlingDestination_WhenAttemptingBlack00Castle_ThenCannot()
        {
            // TODO: Confirm if this test is actually working
            var arbitrator = InitializeKingRookWithRooksUnderAttackArbitrator(SideType.White);
            MakeMoves(arbitrator, "G6", "H8"); // White knight captures rook
            var kingSource = new ChessCoordinate("E", "8");
            var kingDestination = new ChessCoordinate("G", "8");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.InvalidNoMoveMade, result);
            Assert.IsNull(arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(king, arbitrator.GetPiece(kingSource));
        }

        [TestMethod]
        public void GivenAvailableForCastling_WhenAttemptingWhite000Castle_ThenValid()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            var rookSource = new ChessCoordinate("A", "1");
            var rookDestination = new ChessCoordinate("D", "1");
            var rook = arbitrator.GetPiece(rookSource);
            var kingSource = new ChessCoordinate("E", "1");
            var kingDestination = new ChessCoordinate("C", "1");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(rookSource));
            Assert.IsNotNull(arbitrator.GetPiece(kingDestination));
            Assert.AreEqual(king, arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(rookDestination));
            Assert.AreEqual(rook, arbitrator.GetPiece(rookDestination));
            Assert.IsNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
        }

        [TestMethod]
        public void GivenAvailableForCastling_WhenAttemptingWhite00Castle_ThenValid()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.White);
            var rookSource = new ChessCoordinate("H", "1");
            var rookDestination = new ChessCoordinate("F", "1");
            var rook = arbitrator.GetPiece(rookSource);
            var kingSource = new ChessCoordinate("E", "1");
            var kingDestination = new ChessCoordinate("G", "1");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(rookSource));
            Assert.IsNotNull(arbitrator.GetPiece(kingDestination));
            Assert.AreEqual(king, arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(rookDestination));
            Assert.AreEqual(rook, arbitrator.GetPiece(rookDestination));
            Assert.IsNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(SideType.Black, arbitrator.CurrentPlayerSide);
        }

        [TestMethod]
        public void GivenAvailableForCastling_WhenAttemptingBlack000Castle_ThenValid()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            var rookSource = new ChessCoordinate("A", "8");
            var rookDestination = new ChessCoordinate("D", "8");
            var rook = arbitrator.GetPiece(rookSource);
            var kingSource = new ChessCoordinate("E", "8");
            var kingDestination = new ChessCoordinate("C", "8");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(rookSource));
            Assert.IsNotNull(arbitrator.GetPiece(kingDestination));
            Assert.AreEqual(king, arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(rookDestination));
            Assert.AreEqual(rook, arbitrator.GetPiece(rookDestination));
            Assert.IsNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(SideType.White, arbitrator.CurrentPlayerSide);
        }

        [TestMethod]
        public void GivenAvailableForCastling_WhenAttemptingBlack00Castle_ThenValid()
        {
            var arbitrator = InitializeKingRookArbitrator(SideType.Black);
            var rookSource = new ChessCoordinate("H", "8");
            var rookDestination = new ChessCoordinate("F", "8");
            var rook = arbitrator.GetPiece(rookSource);
            var kingSource = new ChessCoordinate("E", "8");
            var kingDestination = new ChessCoordinate("G", "8");
            var king = arbitrator.GetPiece(kingSource);

            var result = arbitrator.MakeMove(kingSource, kingDestination);

            Assert.AreEqual(MoveResult.Valid, result);
            Assert.IsNull(arbitrator.GetPiece(rookSource));
            Assert.IsNotNull(arbitrator.GetPiece(kingDestination));
            Assert.AreEqual(king, arbitrator.GetPiece(kingDestination));
            Assert.IsNotNull(arbitrator.GetPiece(rookDestination));
            Assert.AreEqual(rook, arbitrator.GetPiece(rookDestination));
            Assert.IsNull(arbitrator.GetPiece(kingSource));
            Assert.AreEqual(SideType.White, arbitrator.CurrentPlayerSide);
        }

        // TODO: Migrate other ChessBoardArbitrator tests to use the new MakeMoves helper function
    }
}
