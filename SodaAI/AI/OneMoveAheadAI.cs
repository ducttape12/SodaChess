using SodaChess;

namespace SodaAI.AI
{
    public class OneMoveAheadAI : BaseAI, ISodaAI
    {
        public OneMoveAheadAI() { }

        public OneMoveAheadAI(bool isControl) : base(isControl)
        {
        }

        public AIMoveWithBoardState GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator)
        {
            Initialize(arbitrator);

            /*
             * The goal of this AI is as follows:
             *  1. Only look one move ahead
             *  2. Always take the win if one is found
             *  3. Make a move that results in the biggest delta
             *  4. Use the cheapest piece to get that delta
             *  5. Randomly select if multiple moves will get that same result
             */

            var validMoves = FindAllValidMoves();

            // 2. Always choose a move that results in a win
            var checkmateMoves = validMoves.Where(m => m.MoveResult == OpponentCheckmate);

            if (checkmateMoves.Any())
            {
                return checkmateMoves.First();
            }

            // 3. Find the biggest delta possible
            var maximumDelta = validMoves.Max(CurrentPlayerDelta);

            // 4. Find the cheapest piece required to get that delta
            var cheapestPiece = validMoves.Where(m => CurrentPlayerDelta(m) == maximumDelta)
                                            .OrderBy(m => m.SourcePiece.TrueValue)
                                            .First();

            // 5. Randomly select from the possible moves
            var moves = validMoves.Where(m => CurrentPlayerDelta(m) == maximumDelta &&
                                                m.SourcePiece.PieceType == cheapestPiece.SourcePiece.PieceType)
                                    .ToList();
            var randomMove = GetRandomMove(moves);

            return randomMove;
        }
    }
}
