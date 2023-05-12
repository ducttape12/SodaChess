using SodaChess;
using SodaChess.Pieces;

namespace SodaAI
{
    public class AIMoveWithBoardState : AIMove
    {
        public int BranchBlackScore { get; internal set; }
        public int BranchWhiteScore { get; internal set; }
        public MoveResult MoveResult { get; private set; }
        public ChessPiece SourcePiece { get; private set; }

        public int WhiteToBlackBranchDelta => BranchWhiteScore - BranchBlackScore;
        public int BlackToWhiteBranchDelta => BranchBlackScore - BranchWhiteScore;

        public AIMoveWithBoardState(ChessCoordinate source, ChessCoordinate destination, int branchBlackScore, int branchWhiteScore,
            MoveResult moveResult, ChessPiece sourcePiece, PieceType? promotion = null) : base(source, destination, promotion)
        {
            BranchBlackScore = branchBlackScore;
            BranchWhiteScore = branchWhiteScore;
            MoveResult = moveResult;
            SourcePiece = sourcePiece;
        }
    }
}
