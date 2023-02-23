using SodaAI.AI;
using SodaChess;

namespace BatchChess
{
    public class ChessRunner
    {
        private ChessBoardArbitrator arbitrator;

        public ChessRunner()
        {
            arbitrator = new ChessBoardArbitrator();
        }

        public MoveResult Run()
        {
            var lastResult = MoveResult.Valid;

            do
            {
                var randomSodaAI = new RandomSodaAI(arbitrator);
                var aiMove = randomSodaAI.FindMoveForCurrentPlayer();
                lastResult = arbitrator.MakeMove(aiMove.Source, aiMove.Destination);

                if(lastResult == MoveResult.PromotionInputNeeded)
                {
                    lastResult = arbitrator.PromotePiece(aiMove.Promotion.Value);
                }

            } while (lastResult != MoveResult.ValidStalemate &&
                     lastResult != MoveResult.ValidBlackInCheckmate &&
                     lastResult != MoveResult.ValidWhiteInCheckmate);

            return lastResult;
        }
    }
}
