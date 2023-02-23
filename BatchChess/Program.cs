using BatchChess;
using SodaChess;
using System.Collections.Concurrent;

public class Program
{
    private const int GamesToSimulate = 1000;

    public static void Main(string[] args)
    {
        var stalemate = 0;
        var blackInCheckmate = 0;
        var whiteInCheckmate = 0;

        var gamesToPlay = Enumerable.Range(0, GamesToSimulate);

        var locker = new Object();

        gamesToPlay.AsParallel().ForAll(gameNumber =>
        {
            var runner = new ChessRunner();
            Console.WriteLine($"Playing chess game {gameNumber + 1}/{GamesToSimulate}...");
            var result = runner.Run();
            Console.WriteLine($"Result of chess game {gameNumber + 1}/{GamesToSimulate}: {result}");

            switch(result)
            {
                case MoveResult.ValidStalemate:
                    Interlocked.Increment(ref stalemate);
                    break;
                case MoveResult.ValidBlackInCheckmate:
                    Interlocked.Increment(ref blackInCheckmate);
                    break;
                case MoveResult.ValidWhiteInCheckmate:
                    Interlocked.Increment(ref whiteInCheckmate);
                    break;
                default:
                    throw new NotImplementedException($"Unknown result {result}");
            }
        });

        Console.WriteLine($"Result of {GamesToSimulate} games:");
        Console.WriteLine($"  Stalemates: {stalemate} ({PercentOfGames(stalemate)}%)");
        Console.WriteLine($"  Black In Checkmate: {blackInCheckmate} ({PercentOfGames(blackInCheckmate)}%)");
        Console.WriteLine($"  White In Checkmate: {whiteInCheckmate} ({PercentOfGames(whiteInCheckmate)}%)");
    }

    private static decimal PercentOfGames(int games)
    {
        return ((decimal)games / (decimal)GamesToSimulate) * 100m;
    }
}