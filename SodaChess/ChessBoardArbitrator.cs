using SodaChess.MoveLogic;
using SodaChess.Pieces;

namespace SodaChess
{
    public class ChessBoardArbitrator
    {
        private const int MaximumMovesWithoutPawnOrCapture = 100;

        private readonly ChessBoard board;

        public SideType CurrentPlayerSide { get; private set; }

        public IList<ChessPiece> CapturedBlackPieces { get; private set; }
        public IList<ChessPiece> CapturedWhitePieces { get; private set; }

        public int CapturedBlackPiecesValue => CapturedBlackPieces.Sum(p => p.Value);
        public int CapturedWhitePiecesValue => CapturedWhitePieces.Sum(p => p.Value);

        private ChessCoordinate? coordinateReadyForPromotion = null;

        private bool a1RookMoved = false;
        private bool whiteKingMoved = false;
        private bool h1RookMoved = false;
        private bool a8RookMoved = false;
        private bool blackKingMoved = false;
        private bool h8RookMoved = false;

        private int movesSincePawnOrCapture = 0;

        public ChessBoardArbitrator()
        {
            board = new ChessBoard();
            InitializeBoard();
            CurrentPlayerSide = SideType.White;
            CapturedBlackPieces = new List<ChessPiece>();
            CapturedWhitePieces = new List<ChessPiece>();
        }

        public ChessBoardArbitrator(ChessBoard board, SideType currentPlayerSide)
        {
            this.board = board;
            CurrentPlayerSide = currentPlayerSide;
            CapturedBlackPieces = new List<ChessPiece>();
            CapturedWhitePieces = new List<ChessPiece>();
        }
        
        public ChessBoardArbitrator(ChessBoardArbitrator source)
        {
            board = new ChessBoard(source.board);
            CurrentPlayerSide = source.CurrentPlayerSide;
            CapturedBlackPieces = source.CapturedBlackPieces.Select(p => new ChessPiece(p)).ToList();
            CapturedWhitePieces = source.CapturedWhitePieces.Select(p => new ChessPiece(p)).ToList();
            coordinateReadyForPromotion = source.coordinateReadyForPromotion == null ?
                null :
                new ChessCoordinate(source.coordinateReadyForPromotion.File,
                                    source.coordinateReadyForPromotion.Rank);
            a1RookMoved = source.a1RookMoved;
            whiteKingMoved = source.whiteKingMoved;
            h1RookMoved = source.h1RookMoved;
            a8RookMoved = source.a8RookMoved;
            blackKingMoved = source.blackKingMoved;
            h8RookMoved = source.h8RookMoved;
            movesSincePawnOrCapture = source.movesSincePawnOrCapture;
        }

        public void InitializeBoard()
        {
            board.SetPiece(new ChessCoordinate("A", "8"), new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "8"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "8"), new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "8"), new ChessPiece(PieceType.Queen, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "8"), new ChessPiece(PieceType.King, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "8"), new ChessPiece(PieceType.Bishop, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "8"), new ChessPiece(PieceType.Knight, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "8"), new ChessPiece(PieceType.Rook, SideType.Black));
            board.SetPiece(new ChessCoordinate("A", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("B", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("C", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("D", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("E", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("F", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("G", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));
            board.SetPiece(new ChessCoordinate("H", "7"), new ChessPiece(PieceType.Pawn, SideType.Black));

            board.SetPiece(new ChessCoordinate("A", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("C", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("F", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "2"), new ChessPiece(PieceType.Pawn, SideType.White));
            board.SetPiece(new ChessCoordinate("A", "1"), new ChessPiece(PieceType.Rook, SideType.White));
            board.SetPiece(new ChessCoordinate("B", "1"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("C", "1"), new ChessPiece(PieceType.Bishop, SideType.White));
            board.SetPiece(new ChessCoordinate("E", "1"), new ChessPiece(PieceType.King, SideType.White));
            board.SetPiece(new ChessCoordinate("D", "1"), new ChessPiece(PieceType.Queen, SideType.White));
            board.SetPiece(new ChessCoordinate("F", "1"), new ChessPiece(PieceType.Bishop, SideType.White));
            board.SetPiece(new ChessCoordinate("G", "1"), new ChessPiece(PieceType.Knight, SideType.White));
            board.SetPiece(new ChessCoordinate("H", "1"), new ChessPiece(PieceType.Rook, SideType.White));
        }

        public MoveResult MakeMove(ChessCoordinate source, ChessCoordinate destination)
        {
            if (coordinateReadyForPromotion != null)
            {
                return MoveResult.PromotionInputNeeded;
            }

            var sourcePiece = GetPiece(source);
            var destinationPiece = GetPiece(destination);

            // Must select a piece and the piece must be the current player's piece
            if (sourcePiece == null || sourcePiece.SideType != CurrentPlayerSide)
            {
                return MoveResult.InvalidNoMoveMade;
            }

            // Perform a castle if the player is attempting
            var castlePerformed = PerformCastle(source, destination, sourcePiece);

            // If a castle was performed, then no further action is needed
            if (castlePerformed)
            {
                movesSincePawnOrCapture = 0;
                return SwitchSidesAndCalculateBoardState();
            }

            // Determine if this is a valid move for the piece
            var possiblePieceMoves = ValidMoves(source);

            if (!possiblePieceMoves.Contains(destination))
            {
                return MoveResult.InvalidNoMoveMade;
            }

            // Simulate the move to ensure the current player isn't going into check
            if (MoveWillResultInPlayerCheck(source, destination, CurrentPlayerSide))
            {
                return MoveResult.InvalidNoMoveMade;
            }

            // If the move captures a piece, save the piece
            if (destinationPiece != null)
            {
                switch (destinationPiece.SideType)
                {
                    case SideType.White:
                        CapturedWhitePieces.Add(destinationPiece);
                        break;

                    case SideType.Black:
                        CapturedBlackPieces.Add(destinationPiece);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            // Update move counter
            if (sourcePiece.PieceType == PieceType.Pawn || destinationPiece != null)
            {
                movesSincePawnOrCapture = 0;
            }
            else
            {
                movesSincePawnOrCapture++;
            }

            // Perform the move
            board.SetPiece(destination, sourcePiece);
            board.SetPiece(source, null);

            // Update if rooks or kings have moved
            UpdateCastlingAvailability(source, sourcePiece);

            // If a pawn moved two ranks, the middle rank is available for an en passant
            UpdateAvailableForEnPassant(source, destination, sourcePiece);

            // If this makes a piece ready for promotion, then request additional information
            coordinateReadyForPromotion = CoordinateReadyForPromotion();

            if (coordinateReadyForPromotion != null)
            {
                return MoveResult.PromotionInputNeeded;
            }

            return SwitchSidesAndCalculateBoardState();
        }

        private void UpdateCastlingAvailability(ChessCoordinate source, ChessPiece piece)
        {
            // Don't need to check if it's rook since only rooks can move off these coordinates.
            if (!a1RookMoved && source == new ChessCoordinate("A", "1"))
            {
                a1RookMoved = true;
            }
            else if (!h1RookMoved && source == new ChessCoordinate("H", "1"))
            {
                h1RookMoved = true;
            }
            else if (!a8RookMoved && source == new ChessCoordinate("A", "8"))
            {
                a8RookMoved = true;
            }
            else if (!h8RookMoved && source == new ChessCoordinate("H", "8"))
            {
                h8RookMoved = true;
            }
            else if (piece.PieceType == PieceType.King && piece.SideType == SideType.White)
            {
                whiteKingMoved = true;
            }
            else if (piece.PieceType == PieceType.King && piece.SideType == SideType.Black)
            {
                blackKingMoved = true;
            }
        }

        private bool PerformCastle(ChessCoordinate kingSource, ChessCoordinate kingDestination, ChessPiece kingPiece)
        {
            if (kingPiece.PieceType != PieceType.King)
            {
                return false;
            }

            // Determine if a king is attempting to move two squares towards a rook and neither the king nor
            // the rook have moved this game
            var whiteCastling00 =
                kingPiece.SideType == SideType.White &&
                !whiteKingMoved &&
                !h1RookMoved &&
                kingSource == new ChessCoordinate("E", "1") &&
                kingDestination == new ChessCoordinate("G", "1");
            var whiteCastling000 =
                kingPiece.SideType == SideType.White &&
                !whiteKingMoved &&
                !a1RookMoved &&
                kingSource == new ChessCoordinate("E", "1") &&
                kingDestination == new ChessCoordinate("C", "1");
            var blackCastling00 =
                kingPiece.SideType == SideType.Black &&
                !blackKingMoved &&
                !h8RookMoved &&
                kingSource == new ChessCoordinate("E", "8") &&
                kingDestination == new ChessCoordinate("G", "8");
            var blackCastling000 =
                kingPiece.SideType == SideType.Black &&
                !blackKingMoved &&
                !a8RookMoved &&
                kingSource == new ChessCoordinate("E", "8") &&
                kingDestination == new ChessCoordinate("C", "8");

            if (!whiteCastling00 && !whiteCastling000 &&
                !blackCastling00 && !blackCastling000)
            {
                return false;
            }

            // Coordinates needed for this action
            List<ChessCoordinate> emptyCoordinates;
            ChessCoordinate kingPassThroughCoordinate;
            ChessCoordinate rookSource;
            ChessCoordinate rookDestination;

            if (whiteCastling00)
            {
                emptyCoordinates = new List<ChessCoordinate>()
                {
                    new ChessCoordinate("F", "1"),
                    new ChessCoordinate("G", "1")
                };
                kingPassThroughCoordinate = new ChessCoordinate("F", "1");
                rookSource = new ChessCoordinate("H", "1");
                rookDestination = new ChessCoordinate("F", "1");
            }
            else if (whiteCastling000)
            {
                emptyCoordinates = new List<ChessCoordinate>()
                {
                    new ChessCoordinate("B", "1"),
                    new ChessCoordinate("C", "1"),
                    new ChessCoordinate("D", "1"),
                };
                kingPassThroughCoordinate = new ChessCoordinate("D", "1");
                rookSource = new ChessCoordinate("A", "1");
                rookDestination = new ChessCoordinate("D", "1");
            }
            else if (blackCastling00)
            {
                emptyCoordinates = new List<ChessCoordinate>()
                {
                    new ChessCoordinate("F", "8"),
                    new ChessCoordinate("G", "8"),
                };
                kingPassThroughCoordinate = new ChessCoordinate("F", "8");
                rookSource = new ChessCoordinate("H", "8");
                rookDestination = new ChessCoordinate("F", "8");
            }
            else // blackAttemptingCastle000
            {
                emptyCoordinates = new List<ChessCoordinate>()
                {
                    new ChessCoordinate("B", "8"),
                    new ChessCoordinate("C", "8"),
                    new ChessCoordinate("D", "8"),
                };
                kingPassThroughCoordinate = new ChessCoordinate("D", "8");
                rookSource = new ChessCoordinate("A", "8");
                rookDestination = new ChessCoordinate("D", "8");
            }

            // All coordinates between the king and the rook must be empty
            if (emptyCoordinates.Any(coordinate => GetPiece(coordinate) != null))
            {
                return false;
            }

            // Rook that's castling must exist
            var rook = board.GetPiece(rookSource);
            if (rook == null ||
                rook.PieceType != PieceType.Rook ||
                rook.SideType != CurrentPlayerSide)
            {
                return false;
            }

            // The king cannot be in check, pass through check, or end up in check
            if (IsPlayerInCheck(board, CurrentPlayerSide) ||
                MoveWillResultInPlayerCheck(kingSource, kingPassThroughCoordinate, CurrentPlayerSide) ||
                MoveWillResultInPlayerCheck(kingSource, kingDestination, CurrentPlayerSide))
            {
                return false;
            }

            // Castling is allowed; perform the castle
            board.SetPiece(rookSource, null);
            board.SetPiece(rookDestination, rook);
            board.SetPiece(kingSource, null);
            board.SetPiece(kingDestination, kingPiece);

            switch (CurrentPlayerSide)
            {
                case SideType.White:
                    whiteKingMoved = true;
                    a1RookMoved = true;
                    h1RookMoved = true;
                    break;
                case SideType.Black:
                    blackKingMoved = true;
                    a8RookMoved = true;
                    h8RookMoved = true;
                    break;
            }

            return true;
        }

        private void UpdateAvailableForEnPassant(ChessCoordinate source, ChessCoordinate destination, ChessPiece piece)
        {
            if (piece.PieceType == PieceType.Pawn && source.Rank == "2" && destination.Rank == "4")
            {
                board.AvailableForEnPassant = new ChessCoordinate(source.File, "3");
            }
            else if (piece.PieceType == PieceType.Pawn && source.Rank == "7" && destination.Rank == "5")
            {
                board.AvailableForEnPassant = new ChessCoordinate(source.File, "6");
            }
            else
            {
                board.AvailableForEnPassant = null;
            }
        }

        private MoveResult SwitchSidesAndCalculateBoardState()
        {
            // Switch sides
            CurrentPlayerSide = CurrentPlayerSide switch
            {
                SideType.White => SideType.Black,
                SideType.Black => SideType.White,
                _ => throw new NotImplementedException()
            };

            // Calculate board state
            var inCheck = IsPlayerInCheck(board, CurrentPlayerSide);
            var playerHasValidMoves = AnyValidMovesNotResultingInCheck(CurrentPlayerSide);
            var onlyKingsRemaining = AreOnlyKingsRemaining();
            var pastMoveLimit = movesSincePawnOrCapture >= MaximumMovesWithoutPawnOrCapture;

            var checkmate = inCheck && !playerHasValidMoves;
            var stalemate = (!inCheck && !playerHasValidMoves) ||
                            onlyKingsRemaining ||
                            pastMoveLimit;

            if (checkmate)
            {
                return CurrentPlayerSide switch
                {
                    SideType.White => MoveResult.ValidWhiteInCheckmate,
                    SideType.Black => MoveResult.ValidBlackInCheckmate,
                    _ => throw new NotImplementedException()
                };
            }

            if (stalemate)
            {
                return MoveResult.ValidStalemate;
            }

            if (inCheck)
            {
                return CurrentPlayerSide switch
                {
                    SideType.White => MoveResult.ValidWhiteInCheck,
                    SideType.Black => MoveResult.ValidBlackInCheck,
                    _ => throw new NotImplementedException()
                };
            }

            return MoveResult.Valid;
        }

        public MoveResult PromotePiece(PieceType promotionTarget)
        {
            if (coordinateReadyForPromotion == null)
            {
                return MoveResult.InvalidCannotPromotePiece;
            }

            if (promotionTarget == PieceType.King || promotionTarget == PieceType.Pawn)
            {
                return MoveResult.InvalidCannotPromotePiece;
            }

            var pieceReadyForPromotion = GetPiece(coordinateReadyForPromotion);

            if (pieceReadyForPromotion == null)
            {
                throw new ApplicationException($"Coordinate {coordinateReadyForPromotion} was marked for " +
                    "promotion, but no piece exists there");
            }

            var promotedPiece = new ChessPiece(promotionTarget, pieceReadyForPromotion.SideType);
            board.SetPiece(coordinateReadyForPromotion, promotedPiece);
            coordinateReadyForPromotion = null;

            return SwitchSidesAndCalculateBoardState();
        }

        private ChessCoordinate? CoordinateReadyForPromotion()
        {
            string[] ranksForPromotion = new string[] {
                Coordinates.Ranks[0],
                Coordinates.Ranks[^1]
            };

            foreach (var rank in ranksForPromotion)
            {
                foreach (string file in Coordinates.Files)
                {
                    var coordinate = new ChessCoordinate(file, rank);
                    var piece = GetPiece(coordinate);

                    if (piece != null && piece.PieceType == PieceType.Pawn)
                    {
                        return coordinate;
                    }
                }
            }

            return null;
        }

        private bool AreOnlyKingsRemaining()
        {
            foreach (string file in Coordinates.Files)
            {
                foreach (string rank in Coordinates.Ranks)
                {
                    var piece = GetPiece(new ChessCoordinate(file, rank));

                    if (piece != null && piece.PieceType != PieceType.King)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool AnyValidMovesNotResultingInCheck(SideType playerSide)
        {
            // For every piece belonging to this player, find all moves, simulate the move, and test if that move results in
            // the player staying in check or entering check.
            foreach (string file in Coordinates.Files)
            {
                foreach (string rank in Coordinates.Ranks)
                {
                    var searchCoordinate = new ChessCoordinate(file, rank);
                    var searchPiece = GetPiece(searchCoordinate);

                    if (searchPiece == null)
                    {
                        continue;
                    }

                    if (searchPiece.SideType != playerSide)
                    {
                        continue;
                    }

                    var allMovesForCurrentPiece = ValidMoves(searchCoordinate);

                    foreach (var move in allMovesForCurrentPiece)
                    {
                        if (!MoveWillResultInPlayerCheck(searchCoordinate, move, playerSide))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool MoveWillResultInPlayerCheck(ChessCoordinate source, ChessCoordinate destination, SideType playerSide)
        {
            var simulationBoard = new ChessBoard(board);
            var simulationPiece = simulationBoard.GetPiece(source);

            // Simulate the move
            simulationBoard.SetPiece(source, null);
            simulationBoard.SetPiece(destination, simulationPiece);

            return IsPlayerInCheck(simulationBoard, playerSide);
        }

        private static bool IsPlayerInCheck(ChessBoard customBoard, SideType playerSide)
        {
            // Find all possible moves from the opponent (and make note of the current player's king location)
            ChessCoordinate? currentPlayerKingCoordinates = null;
            var allOpponentMoves = new List<ChessCoordinate>();

            foreach (string file in Coordinates.Files)
            {
                foreach (string rank in Coordinates.Ranks)
                {
                    var searchCoordinate = new ChessCoordinate(file, rank);
                    var searchPiece = customBoard.GetPiece(searchCoordinate);

                    if (searchPiece == null)
                    {
                        continue;
                    }

                    if (searchPiece.PieceType == PieceType.King && searchPiece.SideType == playerSide)
                    {
                        currentPlayerKingCoordinates = searchCoordinate;
                    }

                    if (searchPiece.SideType == playerSide)
                    {
                        continue;
                    }

                    allOpponentMoves.AddRange(ValidMoves(customBoard, searchCoordinate));
                }
            }

            if (currentPlayerKingCoordinates == null)
            {
                throw new ApplicationException($"No {playerSide} king found");
            }

            // If any of the opponent moves include the current player's king coordinate, then this move would put the
            // current player in check and therefore is invalid
            return allOpponentMoves.Contains(currentPlayerKingCoordinates);
        }

        public IList<ChessCoordinate> ValidMoves(ChessCoordinate coordinate)
        {
            return ValidMoves(board, coordinate);
        }

        public static IList<ChessCoordinate> ValidMoves(ChessBoard customBoard, ChessCoordinate coordinate)
        {
            var piece = customBoard.GetPiece(coordinate);

            if (piece == null)
            {
                return new List<ChessCoordinate>();
            }

            return piece.PieceType switch
            {
                PieceType.Pawn => new PawnMoveLogic(customBoard, coordinate).GetMoveList(),
                PieceType.Rook => new RookMoveLogic(customBoard, coordinate).GetMoveList(),
                PieceType.Knight => new KnightMoveLogic(customBoard, coordinate).GetMoveList(),
                PieceType.Bishop => new BishopMoveLogic(customBoard, coordinate).GetMoveList(),
                PieceType.Queen => new QueenMoveLogic(customBoard, coordinate).GetMoveList(),
                PieceType.King => new KingMoveLogic(customBoard, coordinate).GetMoveList(),
                _ => throw new NotImplementedException()
            };
        }

        public ChessPiece? GetPiece(ChessCoordinate coordinate)
        {
            return board.GetPiece(coordinate);
        }
    }
}
