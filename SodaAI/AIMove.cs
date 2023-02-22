using SodaChess.Pieces;

namespace SodaAI
{
    public class AIMove
    {
        public ChessCoordinate Source { get; private set; }
        public ChessCoordinate Destination { get; private set; }
        public PieceType? Promotion { get; private set; }

        public AIMove(ChessCoordinate source, ChessCoordinate destination, PieceType? promotion = null)
        {
            Source = source;
            Destination = destination;
            Promotion = promotion;
        }
    }
}
