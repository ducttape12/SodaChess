using SodaAI;
using SodaChess;
using SodaChess.Pieces;

namespace BatchChess
{
    public class ChessGameRunner
    {
        private readonly ChessBoardArbitrator arbitrator;
        private readonly ISodaAI controlAI;
        private readonly SideType controlSideType;
        private readonly ISodaAI treatmentAI;

        public ChessGameRunner(ISodaAI control, ISodaAI treatment)
        {
            arbitrator = new ChessBoardArbitrator();
            controlAI = control;
            treatmentAI = treatment;

            var random = new Random();
            controlSideType = random.Next(0, 2) == 0 ? SideType.White : SideType.Black;
        }

        public ChessGameRunnerResult Run()
        {
            MoveResult result;
            do
            {
                var ai = GetCurrentPlayerAI();
                var aiMove = ai.GetMoveForCurrentPlayer(arbitrator);
                result = arbitrator.MakeMove(aiMove.Source, aiMove.Destination);

                if (result == MoveResult.PromotionInputNeeded)
                {
                    result = arbitrator.PromotePiece(aiMove.Promotion!.Value);
                }

            } while (result != MoveResult.ValidStalemate &&
                     result != MoveResult.ValidBlackInCheckmate &&
                     result != MoveResult.ValidWhiteInCheckmate);

            if(result == MoveResult.ValidStalemate)
            {
                return ChessGameRunnerResult.Stalemate;
            }
            else if(result == MoveResult.ValidWhiteInCheckmate)
            {
                return controlSideType == SideType.White ? ChessGameRunnerResult.TreatmentAIWon : ChessGameRunnerResult.ControlAIWon;
            }
            else // ValidBlackInCheckmate
            {
                return controlSideType == SideType.White ? ChessGameRunnerResult.ControlAIWon : ChessGameRunnerResult.TreatmentAIWon;
            }
        }

        private ISodaAI GetCurrentPlayerAI()
        {
            if(arbitrator.CurrentPlayerSide == controlSideType)
            {
                return controlAI;
            }

            return treatmentAI;
        }
    }
}
