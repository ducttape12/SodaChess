using SodaChess;
using System;

namespace SodaAI.AI
{
    public class RandomAI : BaseAI, ISodaAI
    {
        public AIMove GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator)
        {
            Initialize(arbitrator);

            var validMoves = FindAllValidMoves();
            var randomMove = GetRandomMove(validMoves);

            return randomMove;
        }
    }
}