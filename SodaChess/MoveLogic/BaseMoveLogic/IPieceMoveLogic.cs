using SodaChess.Pieces;

namespace SodaChess.MoveLogic.BaseMoveLogic
{
    internal interface IPieceMoveLogic
    {
        IList<ChessCoordinate> GetMoveList();
    }
}
