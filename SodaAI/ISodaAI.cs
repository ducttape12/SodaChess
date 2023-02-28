using SodaChess;

namespace SodaAI
{
    public interface ISodaAI
    {
        public AIMove GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator);
    }
}
