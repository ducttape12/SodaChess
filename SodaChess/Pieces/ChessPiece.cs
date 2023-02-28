namespace SodaChess.Pieces
{
    public class ChessPiece
    {
        public PieceType PieceType { get; set; }
        public SideType SideType { get; set; }

        public int Value => PieceType switch
        {
            PieceType.Pawn => 1,
            PieceType.Knight => 3,
            PieceType.Bishop => 3,
            PieceType.Rook => 5,
            PieceType.Queen => 9,
            _ => 0
        };

        public int TrueValue => PieceType switch
        {
            PieceType.Pawn => 1,
            PieceType.Knight => 3,
            PieceType.Bishop => 3,
            PieceType.Rook => 5,
            PieceType.Queen => 9,
            PieceType.King => int.MaxValue,
            _ => int.MaxValue
        };

        public ChessPiece(PieceType pieceType, SideType sideType)
        {
            PieceType = pieceType;
            SideType = sideType;
        }

        public ChessPiece(ChessPiece original) : this(original.PieceType, original.SideType)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is ChessPiece piece &&
                   PieceType == piece.PieceType &&
                   SideType == piece.SideType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PieceType, SideType);
        }

        public static bool operator ==(ChessPiece? left, ChessPiece? right)
        {
            return EqualityComparer<ChessPiece>.Default.Equals(left, right);
        }

        public static bool operator !=(ChessPiece? left, ChessPiece? right)
        {
            return !(left == right);
        }
    }
}
