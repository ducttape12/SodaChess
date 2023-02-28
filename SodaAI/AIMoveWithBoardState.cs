using SodaChess;
using SodaChess.Pieces;

namespace SodaAI
{
    public class AIMoveWithBoardState : AIMove
    {
        public int BlackScore { get; private set; }
        public int WhiteScore { get; private set; }
        public MoveResult MoveResult { get; private set; }

        public int WhiteToBlackDelta => WhiteScore - BlackScore;
        public int BlackToWhiteDelta => BlackScore - WhiteScore;

        public AIMoveWithBoardState(ChessCoordinate source, ChessCoordinate destination, ChessBoardArbitrator arbitrator,
            MoveResult moveResult, PieceType? promotion = null) :
            base(source, destination, promotion)
        {
            BlackScore = arbitrator.BlackScore;
            WhiteScore = arbitrator.WhiteScore;
            MoveResult = moveResult;
        }
    }
}
