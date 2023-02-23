using BatchChess;
using SodaChess;

public class Program
{
    private const int GamesToSimulate = 100;

    public static void Main(string[] args)
    {
        var stalemate = 0;
        var blackInCheckmate = 0;
        var whiteInCheckmate = 0;
        var blackQueenSideCastlesPerformed = 0;
        var blackKingSideCastlesPerformed = 0;
        var whiteQueenSideCastlesPerformed = 0;
        var whiteKingSideCastlesPerformed = 0;

        var gamesToPlay = Enumerable.Range(0, GamesToSimulate);

        gamesToPlay.AsParallel().ForAll(gameNumber =>
        {
            var runner = new ChessRunner();
            Console.WriteLine($"Playing chess game {gameNumber + 1}/{GamesToSimulate}...");
            runner.Run();
            Console.WriteLine($"Result of chess game {gameNumber + 1}/{GamesToSimulate}: {runner.Result}");

            switch(runner.Result)
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
                    throw new NotImplementedException($"Unknown result {runner.Result}");
            }

            if(runner.BlackQueenSideCastlePerformed)
            {
                Interlocked.Increment(ref blackQueenSideCastlesPerformed);
            }
            if(runner.BlackKingSideCastlePerformed)
            {
                Interlocked.Increment(ref blackKingSideCastlesPerformed);
            }
            if(runner.WhiteQueenSideCastlePerformed)
            {
                Interlocked.Increment(ref whiteQueenSideCastlesPerformed);
            }
            if(runner.WhiteKingSideCastlePerformed)
            {
                Interlocked.Increment(ref whiteKingSideCastlesPerformed);
            }
        });

        Console.WriteLine($"Result of {GamesToSimulate} games:");
        Console.WriteLine($"  Stalemates: {stalemate} ({PercentOfGames(stalemate)}%)");
        Console.WriteLine($"  Black In Checkmate: {blackInCheckmate} ({PercentOfGames(blackInCheckmate)}%)");
        Console.WriteLine($"  White In Checkmate: {whiteInCheckmate} ({PercentOfGames(whiteInCheckmate)}%)");
        Console.WriteLine($"  Black Queen Side Castles Performed: {blackQueenSideCastlesPerformed}");
        Console.WriteLine($"  Black King Side Castles Performed: {blackKingSideCastlesPerformed}");
        Console.WriteLine($"  White Queen Side Castles Performed: {whiteQueenSideCastlesPerformed}");
        Console.WriteLine($"  White King Side Castles Performed: {whiteKingSideCastlesPerformed}");
    }

    private static decimal PercentOfGames(int games)
    {
        return ((decimal)games / (decimal)GamesToSimulate) * 100m;
    }
}