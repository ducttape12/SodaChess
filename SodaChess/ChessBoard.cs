using SodaChess.Pieces;

namespace SodaChess
{
    public class ChessBoard
    {
        /*
         * Holds the chess board.  Chess notation is as follows:
         *
         * 8 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 7 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 6 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 5 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 4 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 3 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 2 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 1 ▓▓░░▓▓░░▓▓░░▓▓░░
         *   A B C D E F G H
         *
         * This is mapped to this array as such:
         * 0 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 1 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 2 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 3 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 4 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 5 ▓▓░░▓▓░░▓▓░░▓▓░░
         * 6 ░░▓▓░░▓▓░░▓▓░░▓▓
         * 7 ▓▓░░▓▓░░▓▓░░▓▓░░
         *   0 1 2 3 4 5 6 7
         *
         * Rank 1-8 maps to 7-0 in the first element
         * File A-H maps to 0-7 in the second element
         *
         * Accessing it looks like this: board[rank][file]
         *
         * For example, D3 is board[5][3]
         */
        private readonly ChessPiece?[,] board;

        public ChessBoard() {
            board = new ChessPiece[8, 8];
        }

        public ChessBoard(ChessBoard original) : this()
        {
            for (var rank = 0; rank < original.board.GetLength(0); rank++)
            {
                for (var file = 0; file < original.board.GetLength(1); file++)
                {
                    var sourcePiece = original.board[rank, file];

                    board[rank, file] = sourcePiece == null ? null : new ChessPiece(sourcePiece);
                }
            }
        }

        public ChessPiece? GetPiece(ChessCoordinate coordinate)
        {
            var arrayRank = TranslateRankToArrayIndex(coordinate.Rank);
            var arrayFile = TranslateFileToArrayIndex(coordinate.File);

            return board[arrayRank, arrayFile];
        }

        public void SetPiece(ChessCoordinate coordinate, ChessPiece? piece)
        {
            var arrayRank = TranslateRankToArrayIndex(coordinate.Rank);
            var arrayFile = TranslateFileToArrayIndex(coordinate.File);

            board[arrayRank, arrayFile] = piece;
        }

        private static int TranslateFileToArrayIndex(string file)
        {
            return file.ToUpperInvariant() switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                "D" => 3,
                "E" => 4,
                "F" => 5,
                "G" => 6,
                "H" => 7,
                _ => throw new IndexOutOfRangeException($"Unknown file {file}")
            };
        }

        private static int TranslateRankToArrayIndex(string rank)
        {
            return rank.ToUpperInvariant() switch
            {
                "1" => 7,
                "2" => 6,
                "3" => 5,
                "4" => 4,
                "5" => 3,
                "6" => 2,
                "7" => 1,
                "8" => 0,
                _ => throw new IndexOutOfRangeException($"Unknown rank {rank}")
            };
        }
    }
}
