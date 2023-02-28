using SodaAI;
using SodaAI.AI;
using SodaChess;
using SodaChess.Pieces;

namespace BatchChess
{
    public class ChessRunner
    {
        private ChessBoardArbitrator arbitrator;
        public MoveResult Result { get; private set; } = MoveResult.InvalidNoMoveMade;
        public bool BlackQueenSideCastlePerformed { get; private set; }
        public bool BlackKingSideCastlePerformed { get; private set; }
        public bool WhiteQueenSideCastlePerformed { get; private set; }
        public bool WhiteKingSideCastlePerformed { get; private set; }

        public ChessRunner()
        {
            arbitrator = new ChessBoardArbitrator();
        }

        public void Run()
        {
            do
            {
                var ai = GetAI();
                var aiMove = ai.GetMoveForCurrentPlayer();
                Result = arbitrator.MakeMove(aiMove.Source, aiMove.Destination);

                if (aiMove.Source.File == "E" && aiMove.Source.Rank == "1" &&
                    aiMove.Destination.File == "C" && aiMove.Source.Rank == "1")
                {
                    WhiteQueenSideCastlePerformed = true;
                }
                if (aiMove.Source.File == "E" && aiMove.Source.Rank == "1" &&
                    aiMove.Destination.File == "G" && aiMove.Source.Rank == "1")
                {
                    WhiteKingSideCastlePerformed = true;
                }
                if (aiMove.Source.File == "E" && aiMove.Source.Rank == "8" &&
                    aiMove.Destination.File == "C" && aiMove.Source.Rank == "8")
                {
                    BlackQueenSideCastlePerformed = true;
                }
                if (aiMove.Source.File == "E" && aiMove.Source.Rank == "8" &&
                    aiMove.Destination.File == "G" && aiMove.Source.Rank == "8")
                {
                    BlackKingSideCastlePerformed = true;
                }

                if (Result == MoveResult.PromotionInputNeeded)
                {
                    Result = arbitrator.PromotePiece(aiMove.Promotion.Value);
                }

            } while (Result != MoveResult.ValidStalemate &&
                     Result != MoveResult.ValidBlackInCheckmate &&
                     Result != MoveResult.ValidWhiteInCheckmate);
        }

        private ISodaAI GetAI()
        {
            return arbitrator.CurrentPlayerSide == SideType.Black ? new OneMoveAheadAI(arbitrator) : new RandomAI(arbitrator);
        }
    }
}
