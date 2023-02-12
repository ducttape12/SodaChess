using SodaChess;
using SodaChess.Pieces;

namespace ConsoleChess
{

    internal class Program
    {
        private const ConsoleColor BlackSquareBackground = ConsoleColor.DarkGreen;
        private const ConsoleColor WhiteSquareBackground = ConsoleColor.DarkYellow;
        private const ConsoleColor BlackPieceForeground = ConsoleColor.Black;
        private const ConsoleColor WhitePieceForeground = ConsoleColor.White;
        private const ConsoleColor NormaTextBackground = ConsoleColor.Black;
        private const ConsoleColor NormalTextForeground = ConsoleColor.White;

        private static void Main(string[] args)
        {
            var arbitrator = new ChessBoardArbitrator();

            var lastResult = MoveResult.Valid;

            do
            {
                if (lastResult == MoveResult.PromotionInputNeeded)
                {
                    Console.WriteLine($"{arbitrator.CurrentPlayerSide}, should the piece be promoted to (Q)ueen, " +
                                    "(B)ishop, (K)night, or (R)ook? ");
                    var promotionIdentifier = Console.ReadLine();

                    promotionIdentifier ??= string.Empty;

                    switch (promotionIdentifier.ToUpperInvariant())
                    {
                        case "Q":
                            lastResult = arbitrator.PromotePiece(PieceType.Queen);
                            break;

                        case "B":
                            lastResult = arbitrator.PromotePiece(PieceType.Bishop);
                            break;

                        case "K":
                            lastResult = arbitrator.PromotePiece(PieceType.Knight);
                            break;

                        case "R":
                            lastResult = arbitrator.PromotePiece(PieceType.Rook);
                            break;

                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    };
                }
                else
                {
                    DisplayBoard(arbitrator);

                    Console.WriteLine($"{arbitrator.CurrentPlayerSide}, please enter your move.");
                    var sourceFileRank = string.Empty;

                    while(sourceFileRank == null || sourceFileRank.Length != 2)
                    {
                        Console.Write("Source File/Rank: ");
                        sourceFileRank = Console.ReadLine();
                    }

                    var sourceFile = sourceFileRank.Substring(0, 1).ToUpperInvariant();
                    var sourceRank = sourceFileRank.Substring(1, 1);
                    Console.WriteLine($"Source File: {sourceFile}, Source Rank: {sourceRank}");
                    var source = new ChessCoordinate(sourceFile, sourceRank);

                    var destinationFileRank = string.Empty;

                    while (destinationFileRank == null || destinationFileRank.Length != 2)
                    {
                        Console.Write("Destination File/Rank: ");
                        destinationFileRank = Console.ReadLine();
                    }

                    var destinationFile = destinationFileRank.Substring(0, 1).ToUpperInvariant();
                    var destinationRank = destinationFileRank.Substring(1, 1);
                    Console.WriteLine($"Source File: {destinationFile}, Source Rank: {destinationRank}");
                    var destination = new ChessCoordinate(destinationFile, destinationRank);

                    lastResult = arbitrator.MakeMove(source, destination);
                }

                Console.WriteLine();
                Console.WriteLine($"Move result: {lastResult}");
                Console.WriteLine();

            } while (lastResult != MoveResult.ValidStalemate &&
                     lastResult != MoveResult.ValidBlackInCheckmate &&
                     lastResult != MoveResult.ValidWhiteInCheckmate);

            Console.WriteLine();
            Console.WriteLine(lastResult switch
            {
                MoveResult.ValidStalemate => "Game ends in stalemate",
                MoveResult.ValidBlackInCheckmate => "White wins by checkmate!",
                MoveResult.ValidWhiteInCheckmate => "Black wins by checkmate!",
                _ => "Unknown end state"
            });
            Console.WriteLine("Final game board: ");
            DisplayBoard(arbitrator);
        }

        private static void DisplayBoard(ChessBoardArbitrator arbitrator)
        {
            var squareIsWhite = true;

            Console.Write("Black captured: ");
            foreach (var piece in arbitrator.CapturedWhitePieces)
            {
                var pieceDisplay = GenerateChessPieceDisplay(piece);
                Console.Write($"{pieceDisplay} ");
            }
            Console.WriteLine();

            for(var rankIndex = Coordinates.Ranks.Length - 1; rankIndex >= 0; rankIndex--)
            {
                var rank = Coordinates.Ranks[rankIndex];
                Console.Write($"{rank} ");

                foreach (var file in Coordinates.Files)
                {
                    var piece = arbitrator.GetPiece(new ChessCoordinate(file, rank));

                    var backgroundColor = squareIsWhite ?
                            WhiteSquareBackground : BlackSquareBackground;
                    var foregroundColor = piece != null && piece.SideType == SideType.White ?
                        WhitePieceForeground : BlackPieceForeground;

                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = foregroundColor;

                    var pieceDisplay = GenerateChessPieceDisplay(piece);

                    Console.Write($"{pieceDisplay} ");

                    squareIsWhite = !squareIsWhite;

                    Console.BackgroundColor = NormaTextBackground;
                    Console.ForegroundColor = NormalTextForeground;
                }
                squareIsWhite = !squareIsWhite;

                Console.WriteLine();
            }

            Console.Write("  ");

            foreach (var file in Coordinates.Files)
            {
                Console.Write($"{file} ");
            }
            Console.WriteLine();

            Console.Write("White captured: ");
            foreach (var piece in arbitrator.CapturedBlackPieces)
            {
                var pieceDisplay = GenerateChessPieceDisplay(piece);
                Console.Write($"{pieceDisplay} ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static string GenerateChessPieceDisplay(ChessPiece piece)
        {
            if (piece == null)
            {
                return " ";
            }

            // FUTURE: See https://www.alt-codes.net/chess-symbols.php for symbols

            return piece.PieceType switch
            {
                PieceType.Bishop => "B",
                PieceType.King => "K",
                PieceType.Knight => "N",
                PieceType.Pawn => "P",
                PieceType.Queen => "Q",
                PieceType.Rook => "R",
                _ => "?"
            };
        }
    }
}