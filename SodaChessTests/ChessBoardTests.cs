using SodaChess.Pieces;
using SodaChess;

namespace SodaChessTests
{
    [TestClass]
    public class ChessBoardTests
    {
        [TestMethod]
        public void GivenAChessBoard_WhenCreated_ThenAllSpacesReturnNull()
        {
            var board = new ChessBoard();

            foreach (var file in Coordinates.Files)
            {
                foreach(var rank in Coordinates.Ranks)
                {
                    var piece = board.GetPiece(new ChessCoordinate(file, rank));

                    Assert.IsNull(piece);
                }
            }
        }

        [TestMethod]
        public void GivenAChessBoard_WhenSetPieceForEveryLocationIsCalled_ThenGetPieceReturnsThatPieceForEveryLocation()
        {
            var piece = new ChessPiece(PieceType.Rook, SideType.Black);

            foreach (var file in Coordinates.Files)
            {
                foreach (var rank in Coordinates.Ranks)
                {
                    var board = new ChessBoard();

                    board.SetPiece(new ChessCoordinate(file, rank), piece);

                    Assert.AreEqual(piece, board.GetPiece(new ChessCoordinate(file, rank)));

                    // Confirm all other spaces are empty
                    foreach (string fileNullCheck in Coordinates.Files)
                    {
                        foreach (string rankNullCheck in Coordinates.Ranks)
                        {
                            if (fileNullCheck == file && rankNullCheck == rank)
                            {
                                continue;
                            }

                            Assert.IsNull(board.GetPiece(new ChessCoordinate(fileNullCheck, rankNullCheck)));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GivenAFilledChessBoard_WhenCopiedToANewBoard_ThenAnExactReplicaIsMade()
        {
            // Arrange
            // Cycle through five pieces to populate the board.  Five pieces ensures each row is different from the last.
            var types = new PieceType[] { PieceType.Pawn, PieceType.Rook, PieceType.Knight, PieceType.Bishop,
                PieceType.Queen };
            var currentTypeIndex = 0;
            var currentSide = SideType.White;

            var originalBoard = new ChessBoard();
            foreach (string file in Coordinates.Files)
            {
                foreach (var rank in Coordinates.Ranks)
                {
                    var piece = new ChessPiece(types[currentTypeIndex], currentSide);

                    originalBoard.SetPiece(new ChessCoordinate(file, rank), piece);

                    currentTypeIndex++;
                    if (currentTypeIndex >= types.Length)
                    {
                        currentTypeIndex = 0;
                    }
                    currentSide = currentSide == SideType.White ? SideType.Black: SideType.White;
                }
            }

            // Act
            var copyBoard = new ChessBoard(originalBoard);

            // Assert
            AssertBoardsAreEqual(originalBoard, copyBoard);
        }

        [TestMethod]
        public void GivenAnEmptyBoard_WhenCopiedToANewBoard_ThenNewBoardIsEmpty()
        {
            var original = new ChessBoard();
            var copy = new ChessBoard(original);

            AssertBoardsAreEqual(original, copy);
        }

        static void AssertBoardsAreEqual(ChessBoard expected, ChessBoard actual)
        {
            foreach (string file in Coordinates.Files)
            {
                foreach (string rank in Coordinates.Ranks)
                {
                    var spaceToInspect = new ChessCoordinate(file, rank);
                    var expectedPiece = expected.GetPiece(spaceToInspect);
                    var actualPiece = actual.GetPiece(spaceToInspect);

                    Assert.AreEqual(expectedPiece, actualPiece);
                }
            }
        }
    }
}
