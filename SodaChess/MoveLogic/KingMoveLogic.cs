using SodaChess.MoveLogic.BaseMoveLogic;
using SodaChess.Pieces;

namespace SodaChess.MoveLogic
{
    internal class KingMoveLogic : BasePieceLogic, IPieceMoveLogic
    {
        public KingMoveLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
            base(board, startingCoordinate)
        {
        }

        public IList<ChessCoordinate> GetMoveList()
        {
            return FindValidMoves(
                GetOffsetCoordinate(0, +1),  // north
                GetOffsetCoordinate(+1, +1), // northeast
                GetOffsetCoordinate(+1, 0),  // east
                GetOffsetCoordinate(+1, -1), // southeast
                GetOffsetCoordinate(0, -1),  // south
                GetOffsetCoordinate(-1, -1), // southwest
                GetOffsetCoordinate(-1, 0),  // west
                GetOffsetCoordinate(-1, +1)  // northwest
            );
        }
    }
}
