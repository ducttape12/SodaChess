using SodaChess.MoveLogic.BaseMoveLogic;
using SodaChess.Pieces;

namespace SodaChess.MoveLogic
{
    internal class KnightMoveLogic : BasePieceLogic, IPieceMoveLogic
    {
        public KnightMoveLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
            base(board, startingCoordinate)
        {
        }

        public IList<ChessCoordinate> GetMoveList()
        {
            return FindValidMoves(
                GetOffsetCoordinate(+1, +2),
                GetOffsetCoordinate(+2, +1),
                GetOffsetCoordinate(+2, -1),
                GetOffsetCoordinate(+1, -2),
                GetOffsetCoordinate(-1, -2),
                GetOffsetCoordinate(-2, -1),
                GetOffsetCoordinate(-2, +1),
                GetOffsetCoordinate(-1, +2)
            );
        }
    }
}
