using SodaChess.Pieces;

namespace SodaChess.MoveLogic.BaseMoveLogic
{
    internal class BasePieceLogic
    {
        protected ChessBoard board;
        protected ChessCoordinate startingCoordinate;
        protected ChessPiece startingPiece;

        public BasePieceLogic(ChessBoard board, ChessCoordinate startingCoordinate)
        {
            this.board = board;
            this.startingCoordinate = startingCoordinate;
            startingPiece = board.GetPiece(startingCoordinate);
        }

        protected static int TranslateRankToArrayIndex(string rank)
        {
            return Array.IndexOf(Coordinates.Ranks, rank);
        }

        protected static int TranslateFileToArrayIndex(string file)
        {
            return Array.IndexOf(Coordinates.Files, file);
        }

        protected ChessCoordinate? GetOffsetCoordinate(int fileOffset, int rankOffset)
        {
            return GetOffsetCoordinate(startingCoordinate, fileOffset, rankOffset);
        }

        protected static ChessCoordinate? GetOffsetCoordinate(ChessCoordinate coordinate, int fileOffset, int rankOffset)
        {
            var startFileIndex = TranslateFileToArrayIndex(coordinate.File);
            var startRankIndex = TranslateRankToArrayIndex(coordinate.Rank);

            if (startFileIndex == -1 || startRankIndex == -1 ||
                    startFileIndex + fileOffset < 0 || startFileIndex + fileOffset > Coordinates.Files.Length - 1 ||
                    startRankIndex + rankOffset < 0 || startRankIndex + rankOffset > Coordinates.Ranks.Length - 1)
            {
                return null;
            }

            var newFile = Coordinates.Files[startFileIndex + fileOffset];
            var newRank = Coordinates.Ranks[startRankIndex + rankOffset];

            return new ChessCoordinate(newFile, newRank);
        }

        protected IList<ChessCoordinate> FindValidMoves(params ChessCoordinate?[] possibleMoves)
        {
            return possibleMoves.Where(ValidMove)
                                .Select(ConvertToNotNull)
                                .ToList();
        }
        private bool ValidMove(ChessCoordinate? move)
        {
            if (move == null)
            {
                return false;
            }

            var piece = board.GetPiece(move);

            if (piece == null || piece.SideType != startingPiece.SideType)
            {
                return true;
            }

            return false;
        }

        private ChessCoordinate ConvertToNotNull(ChessCoordinate? coordinate)
        {
            if (coordinate == null) { throw new ArgumentNullException(nameof(coordinate)); }

            return coordinate;
        }
    }
}
