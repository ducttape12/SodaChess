using SodaChess;

namespace SodaAI
{
    public interface ISodaAI
    {
        public AIMoveWithBoardState GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator);
    }
}
