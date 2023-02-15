namespace SodaChess.Pieces
{
    public class ChessCoordinate
    {
        public string File {get; private set;}
        public string Rank { get; private set;}

        public ChessCoordinate(string file, string rank)
        {
            File = file;
            Rank = rank;
        }

        public override bool Equals(object? obj)
        {
            return obj is ChessCoordinate coordinate &&
                   File == coordinate.File &&
                   Rank == coordinate.Rank;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(File, Rank);
        }

        public override string? ToString()
        {
            return $"{File}{Rank}";
        }

        public static bool operator ==(ChessCoordinate? left, ChessCoordinate? right)
        {
            return EqualityComparer<ChessCoordinate>.Default.Equals(left, right);
        }

        public static bool operator !=(ChessCoordinate? left, ChessCoordinate? right)
        {
            return !(left == right);
        }
    }
}
