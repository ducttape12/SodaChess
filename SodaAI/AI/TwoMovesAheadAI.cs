using SodaChess;

namespace SodaAI.AI
{
    public class TwoMovesAheadAI : BaseAI, ISodaAI
    {
        public TwoMovesAheadAI() { }

        public TwoMovesAheadAI(bool isControl) : base(isControl)
        {
        }

        public AIMove GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator)
        {
            Initialize(arbitrator);

            var oneMoveAheadAI = new OneMoveAheadAI();

            var deltaMoves = new List<AIMoveWithBoardState>();

            // Find all valid moves
            var currentPlayerValidMoves = FindAllValidMoves();

            // Always choose a move that results in a win
            var checkmateMoves = currentPlayerValidMoves.Where(m => m.MoveResult == OpponentCheckmate);

            if (checkmateMoves.Any())
            {
                return checkmateMoves.First();
            }
            
            // Stalemate moves should be added as is since there's no additional moves to make
            foreach(var stalemateMove in currentPlayerValidMoves.Where(m => m.MoveResult == MoveResult.ValidStalemate))
            {
                deltaMoves.Add(stalemateMove);
            }

            // For each move, simulate what the opponent might do by looking ahead a single move
            foreach (var move in currentPlayerValidMoves.Where(m => m.MoveResult != MoveResult.ValidStalemate))
            {
                var possibleBranchArbitrator = new ChessBoardArbitrator(arbitrator);

                var aiResult = possibleBranchArbitrator.MakeMove(move.Source, move.Destination);
                if (aiResult == MoveResult.PromotionInputNeeded)
                {
                    aiResult = possibleBranchArbitrator.PromotePiece(move.Promotion!.Value);
                }
                
                var opponentMove = oneMoveAheadAI.GetMoveForCurrentPlayer(possibleBranchArbitrator);

                var opponentResult = possibleBranchArbitrator.MakeMove(opponentMove.Source, opponentMove.Destination);
                if(opponentResult == MoveResult.PromotionInputNeeded)
                {
                    opponentResult = possibleBranchArbitrator.PromotePiece(opponentMove.Promotion!.Value);
                }

                // Account for checkmates as ultimate good and bad for each player
                var whiteScore = possibleBranchArbitrator.WhiteScore;
                var blackScore = possibleBranchArbitrator.BlackScore;

                if(opponentResult == MoveResult.ValidBlackInCheckmate)
                {
                    whiteScore = Int32.MaxValue;
                    blackScore = Int32.MinValue;
                }

                if(opponentResult == MoveResult.ValidWhiteInCheck)
                {
                    whiteScore = Int32.MaxValue;
                    blackScore = Int32.MinValue;
                }

                var deltaMove = new AIMoveWithBoardState(move.Source, move.Destination, blackScore, whiteScore, opponentResult, move.SourcePiece, move.Promotion);
                deltaMoves.Add(deltaMove);
            }

            // Find the biggest delta possible
            var maximumDelta = deltaMoves.Max(CurrentPlayerDelta);

            // Find the cheapest piece required to get that delta
            var cheapestPiece = deltaMoves.Where(m => CurrentPlayerDelta(m) == maximumDelta)
                                            .OrderBy(m => m.SourcePiece.TrueValue)
                                            .First();

            // Randomly select from the possible moves
            var moves = deltaMoves.Where(m => CurrentPlayerDelta(m) == maximumDelta &&
                                                m.SourcePiece.PieceType == cheapestPiece.SourcePiece.PieceType)
                                    .ToList();
            var randomMove = GetRandomMove(moves);

            return randomMove;
        }
    }
}
