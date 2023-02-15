using SodaChess;
using SodaChess.Pieces;

namespace SodaChessTests.BaseTestLogic
{
    public class BaseChessTests
    {
        /// <summary>
        /// Perform the given moves on the arbitrator and confirm that each move succeeded.
        /// </summary>
        /// <remarks>
        /// The purpose of this is to ensure that all moves are succeeding.  This is useful for negative tests to
        /// ensuring those tests are failing for the correct reason, not because a wrong coordinate was set in the
        /// test.
        /// </remarks>
        /// <param name="arbitrator">The target ChessBoardArbitrator to perform the moves again</param>
        /// <param name="moves">All moves to perform in the format "FileRank".  Odd coordinates are the
        /// source and even coordinates are the destination (e.g., source1, destination1, source1,
        /// destination2, etc.)</param>
        protected void MakeMoves(ChessBoardArbitrator arbitrator, params string[] moves)
        {
            if(moves.Length % 2 != 0)
            {
                throw new ArgumentException("Must have an equal number of moves (source, destination)");
            }

            for(var index = 0; index < moves.Length; index += 2)
            {
                var sourceRaw = moves[index];
                var destinationRaw = moves[index + 1];

                var source = new ChessCoordinate(sourceRaw.Substring(0, 1).ToUpperInvariant(),
                    sourceRaw.Substring(1, 1).ToUpperInvariant());
                var destination = new ChessCoordinate(destinationRaw.Substring(0, 1).ToUpperInvariant(),
                    destinationRaw.Substring(1, 1).ToUpperInvariant());

                var result = arbitrator.MakeMove(source, destination);

                Assert.AreEqual(MoveResult.Valid, result, $"Attempting to move from {source} to {destination} but failed with {result}");
            }
        }
    }
}
