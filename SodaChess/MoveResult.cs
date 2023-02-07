namespace SodaChess
{
    public enum MoveResult
    {
        InvalidNoMoveMade,
        Valid,
        ValidBlackInCheck,
        ValidWhiteInCheck,
        ValidBlackInCheckmate,
        ValidWhiteInCheckmate,
        ValidStalemate,
        PromotionInputNeeded,
        InvalidCannotPromotePiece
    }
}
