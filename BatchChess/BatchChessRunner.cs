using SodaAI;
using SodaAI.AI;

namespace BatchChess
{
    public class BatchChessRunner
    {
        private const int GamesToSimulate = 50;

        private static ISodaAI ControlAI => new OneMoveAheadAI();
        private static ISodaAI TreatmentAI => new TwoMovesAheadAI();

        public static void Main(string[] args)
        {
            var stalemates = 0;
            var controlWins = 0;
            var treatmentWins = 0;

            var gamesToPlay = Enumerable.Range(0, GamesToSimulate);

            gamesToPlay.AsParallel().ForAll(gameNumber =>
            {
                var runner = new ChessGameRunner(ControlAI, TreatmentAI);
                Console.WriteLine($"Playing chess game {gameNumber + 1}/{GamesToSimulate}...");
                var result = runner.Run();
                Console.WriteLine($"Result of chess game {gameNumber + 1}/{GamesToSimulate}: {result}");

                switch (result)
                {
                    case ChessGameRunnerResult.Stalemate:
                        Interlocked.Increment(ref stalemates);
                        break;
                    case ChessGameRunnerResult.ControlAIWon:
                        Interlocked.Increment(ref controlWins);
                        break;
                    case ChessGameRunnerResult.TreatmentAIWon:
                        Interlocked.Increment(ref treatmentWins);
                        break;
                    default:
                        throw new NotImplementedException($"Unknown result {result}");
                }
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Result of {GamesToSimulate} games:");
            Console.WriteLine();
            Console.WriteLine("Result Type     | Count | Percent");
            Console.WriteLine("----------------|-------|--------");
            Console.WriteLine("Stalemates      | {0,5} | {1,7:N2}", stalemates, $"{PercentOfGames(stalemates)}%");
            Console.WriteLine("Control Wins    | {0,5} | {1,7:N2}", controlWins, $"{PercentOfGames(controlWins)}%");
            Console.WriteLine("Treatment Wins  | {0,5} | {1,7:N2}", treatmentWins, $"{PercentOfGames(treatmentWins)}%");
            Console.WriteLine();
            Console.WriteLine($"Control: {ControlAI}");
            Console.WriteLine($"Treatment: {TreatmentAI}");
        }

        private static decimal PercentOfGames(int games)
        {
            return games / (decimal)GamesToSimulate * 100m;
        }
    }
}