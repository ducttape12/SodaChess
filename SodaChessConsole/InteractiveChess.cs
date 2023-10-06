using SodaAI.AI;
using SodaAI;
using SodaChess.Pieces;
using SodaChess;

namespace ConsoleChess
{
    public class InteractiveChess
    {
        private const ConsoleColor BlackSquareBackground = ConsoleColor.DarkGreen;
        private const ConsoleColor WhiteSquareBackground = ConsoleColor.DarkYellow;
        private const ConsoleColor BlackPieceForeground = ConsoleColor.Black;
        private const ConsoleColor WhitePieceForeground = ConsoleColor.White;
        private const ConsoleColor NormaTextBackground = ConsoleColor.Black;
        private const ConsoleColor NormalTextForeground = ConsoleColor.White;

        private const int MillisecondsSleepBetweenComputerMoves = 1000;

        private ISodaAI? whiteAI;
        private ISodaAI? blackAI;

        private ISodaAI? CurrentPlayerAI => arbitrator.CurrentPlayerSide == SideType.White ? whiteAI : blackAI;
        private bool CurrentPlayerIsAI => CurrentPlayerAI != null;

        private readonly ChessBoardArbitrator arbitrator;

        public InteractiveChess()
        {
            arbitrator = new ChessBoardArbitrator();
        }

        public void PlayChess()
        {
            ConfigurePlayers();

            var lastResult = MoveResult.Valid;
            AIMove previousAIMove = null!;

            do
            {
                if (lastResult == MoveResult.PromotionInputNeeded)
                {
                    PieceType promotionTarget;

                    if (CurrentPlayerAI == null)
                    {
                        promotionTarget = GetHumanPiecePromotion();
                    }
                    else
                    {
                        promotionTarget = previousAIMove.Promotion!.Value;
                    }

                    lastResult = arbitrator.PromotePiece(promotionTarget);

                }
                else
                {
                    DisplayBoard();
                    ChessCoordinate source, destination;

                    if (CurrentPlayerAI == null)
                    {
                        (source, destination) = GetHumanMove();
                    }
                    else
                    {
                        if (CurrentPlayerIsAI)
                        {
                            Console.WriteLine($"{arbitrator.CurrentPlayerSide} is thinking...");
                            if(!CurrentPlayerAI.RequiresThinkingTime)
                            {
                                Thread.Sleep(MillisecondsSleepBetweenComputerMoves);
                            }
                        }

                        previousAIMove = CurrentPlayerAI.GetMoveForCurrentPlayer(arbitrator);
                        source = previousAIMove.Source;
                        destination = previousAIMove.Destination;

                        Console.WriteLine($"{arbitrator.CurrentPlayerSide} moved {source} to {destination}");
                    }


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
            DisplayBoard();
        }

        private (ChessCoordinate source, ChessCoordinate destination) GetHumanMove()
        {
            Console.WriteLine($"{arbitrator.CurrentPlayerSide}, please enter your move.");
            var source = GetCoordinate("Source");
            var destination = GetCoordinate("Destination");

            return (source, destination);
        }

        private static ChessCoordinate GetCoordinate(string description)
        {
            ChessCoordinate? coordinate = null;

            do
            {
                Console.Write($"{description} File/Rank (eg 'A1'): ");
                var fileRank = Console.ReadLine();

                if (fileRank == null || fileRank.Length != 2)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                try
                {
                    var file = fileRank![..1].ToUpperInvariant();
                    var rank = fileRank.Substring(1, 1);
                    coordinate = new ChessCoordinate(file, rank);
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                }

            } while (coordinate == null);

            return coordinate;
        }

        private PieceType GetHumanPiecePromotion()
        {
            while (true)
            {
                Console.WriteLine($"{arbitrator.CurrentPlayerSide}, should the piece be promoted to (Q)ueen, " +
                                                    "(B)ishop, (K)night, or (R)ook? ");
                var promotionIdentifier = Console.ReadLine();

                promotionIdentifier ??= string.Empty;

                switch (promotionIdentifier.ToUpperInvariant())
                {
                    case "Q":
                        return PieceType.Queen;

                    case "B":
                        return PieceType.Bishop;

                    case "K":
                        return PieceType.Knight;

                    case "R":
                        return PieceType.Rook;

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                };
            }
        }

        private void DisplayBoard()
        {
            var squareIsWhite = true;

            Console.Write("Black captured: ");
            foreach (var piece in arbitrator.CapturedWhitePieces)
            {
                var pieceDisplay = GenerateChessPieceDisplay(piece);
                Console.Write($"{pieceDisplay} ");
            }
            Console.WriteLine();

            for (var rankIndex = Coordinates.Ranks.Length - 1; rankIndex >= 0; rankIndex--)
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

        private void ConfigurePlayers()
        {
            whiteAI = GetPlayerType(SideType.White);
            blackAI = GetPlayerType(SideType.Black);
        }

        private static ISodaAI? GetPlayerType(SideType sideType)
        {
            while (true)
            {
                Console.WriteLine($"Is {sideType} a:");
                Console.WriteLine("  1. Human");
                Console.WriteLine("  2. Random moving computer");
                Console.WriteLine("  3. Look ahead one move computer");
                Console.WriteLine("  4. Look ahead three moves computer (WARNING: Experimental and slow)");
                Console.Write($"{sideType} selection: ");

                var input = Console.ReadLine();

                Console.WriteLine();

                input = input == null ? string.Empty : input.ToUpperInvariant();

                switch (input)
                {
                    case "1":
                        return null;

                    case "2":
                        return new RandomAI();

                    case "3":
                        return new OneMoveAheadAI();

                    case "4":
                        return new NMovesAheadAI();

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                };

                Console.WriteLine();
            }
        }
    }
}
