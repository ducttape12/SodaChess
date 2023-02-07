using SodaChess.MoveLogic.BaseMoveLogic;
using SodaChess.Pieces;

namespace SodaChess.MoveLogic
{
    internal class BishopMoveLogic : LinePieceLogic, IPieceMoveLogic
    {
        public BishopMoveLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
            base(board, startingCoordinate)
        {
        }

        public IList<ChessCoordinate> GetMoveList()
        {
            var moves = new List<ChessCoordinate>();

            // Search from the starting location to the lowest file and the highest rank (top left)
            moves.AddRange(CrawlValidMoves(-1, +1));

            // Search from the starting location to the lowest file and the lowest rank (bottom left)
            moves.AddRange(CrawlValidMoves(-1, -1));

            // Search from the starting location to the highest file and the highest rank (top right)
            moves.AddRange(CrawlValidMoves(+1, +1));

            // Search from the starting location to the highest file and the lowest rank (bottom right)
            moves.AddRange(CrawlValidMoves(+1, -1));

            return moves;
        }
    }
}
