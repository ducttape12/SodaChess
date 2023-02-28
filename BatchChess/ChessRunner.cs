using SodaAI;
using SodaChess;
using SodaChess.Pieces;

namespace BatchChess
{
    public class ChessRunner
    {
        private readonly ChessBoardArbitrator arbitrator;
        private readonly ISodaAI controlAI;
        private readonly SideType controlSideType;
        private readonly ISodaAI treatmentAI;

        public ChessRunner(ISodaAI control, ISodaAI treatment)
        {
            arbitrator = new ChessBoardArbitrator();
            controlAI = control;
            treatmentAI = treatment;

            var random = new Random();
            controlSideType = random.Next(0, 2) == 0 ? SideType.White : SideType.Black;
        }

        public ChessRunnerResults Run()
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
                return ChessRunnerResults.Stalemate;
            }
            else if(result == MoveResult.ValidWhiteInCheckmate)
            {
                return controlSideType == SideType.White ? ChessRunnerResults.TreatmentAIWon : ChessRunnerResults.ControlAIWon;
            }
            else // ValidBlackInCheckmate
            {
                return controlSideType == SideType.White ? ChessRunnerResults.ControlAIWon : ChessRunnerResults.TreatmentAIWon;
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
