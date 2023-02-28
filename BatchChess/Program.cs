using BatchChess;
using SodaAI;
using SodaAI.AI;

public class Program
{
    private const int GamesToSimulate = 250;

    private static ISodaAI ControlAI => new RandomAI();
    private static ISodaAI TreatmentAI => new OneMoveAheadAI();

    public static void Main(string[] args)
    {
        var stalemates = 0;
        var controlWins = 0;
        var treatmentWins = 0;

        var gamesToPlay = Enumerable.Range(0, GamesToSimulate);

        gamesToPlay.AsParallel().ForAll(gameNumber =>
        {
            var runner = new ChessRunner(ControlAI, TreatmentAI);
            Console.WriteLine($"Playing chess game {gameNumber + 1}/{GamesToSimulate}...");
            var result = runner.Run();
            Console.WriteLine($"Result of chess game {gameNumber + 1}/{GamesToSimulate}: {result}");

            switch(result)
            {
                case ChessRunnerResults.Stalemate:
                    Interlocked.Increment(ref stalemates);
                    break;
                case ChessRunnerResults.ControlAIWon:
                    Interlocked.Increment(ref controlWins);
                    break;
                case ChessRunnerResults.TreatmentAIWon:
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
        Console.WriteLine($"  Stalemates: {stalemates} ({PercentOfGames(stalemates)}%)");
        Console.WriteLine($"  Control Wins: {controlWins} ({PercentOfGames(controlWins)}%)");
        Console.WriteLine($"  Treatment Wins: {treatmentWins} ({PercentOfGames(treatmentWins)}%)");
        Console.WriteLine();
        Console.WriteLine($"Control: {ControlAI.GetType()}");
        Console.WriteLine($"Treatment: {TreatmentAI.GetType()}");
    }

    private static decimal PercentOfGames(int games)
    {
        return ((decimal)games / (decimal)GamesToSimulate) * 100m;
    }
}