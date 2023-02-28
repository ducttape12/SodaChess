using SodaChess;

namespace SodaAI.AI
{
    public class RandomAI : BaseAI, ISodaAI
    {
        public RandomAI(ChessBoardArbitrator arbitrator) : base(arbitrator)
        {
        }

        public AIMove GetMoveForCurrentPlayer()
        {
            var validMoves = FindAllValidMoves();
            var randomMove = GetRandomMove(validMoves);

            return randomMove;
        }
    }
}