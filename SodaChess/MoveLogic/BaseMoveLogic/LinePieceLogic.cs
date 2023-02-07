using SodaChess.Pieces;

namespace SodaChess.MoveLogic.BaseMoveLogic
{
    internal class LinePieceLogic : BasePieceLogic
    {
        public LinePieceLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
            base(board, startingCoordinate)
        {
        }

        protected IList<ChessCoordinate> CrawlValidMoves(int fileDirection, int rankDirection)
        {
            var moves = new List<ChessCoordinate>();

            for (var searchCoordinates = GetOffsetCoordinate(fileDirection, rankDirection);
                searchCoordinates != null;
                searchCoordinates = GetOffsetCoordinate(searchCoordinates, fileDirection, rankDirection))
            {
                var searchPiece = board.GetPiece(searchCoordinates);

                if (searchPiece == null)
                {
                    moves.Add(searchCoordinates);
                }
                else
                {
                    if (searchPiece.SideType != startingPiece.SideType)
                    {
                        moves.Add(searchCoordinates);
                    }

                    break;
                }
            }

            return moves;
        }
    }
}
