using SodaChess.MoveLogic.BaseMoveLogic;
using SodaChess.Pieces;

namespace SodaChess.MoveLogic
{
    internal class QueenMoveLogic : LinePieceLogic, IPieceMoveLogic
    {
        public QueenMoveLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
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

            // Search from the starting location to the lowest file (left)
            moves.AddRange(CrawlValidMoves(-1, 0));

            // Search from the starting location to the highest file (right)
            moves.AddRange(CrawlValidMoves(+1, 0));

            // Search from the starting location to the highest rank (up)
            moves.AddRange(CrawlValidMoves(0, +1));

            // Search from the starting location to the lowest rank (down)
            moves.AddRange(CrawlValidMoves(0, -1));

            return moves;
        }
    }
}
