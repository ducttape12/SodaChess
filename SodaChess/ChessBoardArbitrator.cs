﻿using SodaChess.MoveLogic;
using SodaChess.Pieces;

namespace SodaChess
{
    public class ChessBoardArbitrator
    {
        private readonly ChessBoard board;

        public SideType CurrentPlayerSide { get; private set; }

        public IList<ChessPiece> CapturedBlackPieces { get; private set; }
        public IList<ChessPiece> CapturedWhitePieces { get; private set; }

        private ChessCoordinate? coordinateReadyForPromotion = null;

        public ChessBoardArbitrator()
        {
            board = new ChessBoard();
            initializeBoard();
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

        public void initializeBoard()
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

            // Is move even a valid move for this piece?
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

            // Perform the move
            board.SetPiece(destination, sourcePiece);
            board.SetPiece(source, null);

            // If this makes a piece ready for promotion, then request additional information
            coordinateReadyForPromotion = CoordinateReadyForPromotion();

            if (coordinateReadyForPromotion != null)
            {
                return MoveResult.PromotionInputNeeded;
            }

            return SwitchSidesAndCalculateBoardState();
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

            var checkmate = inCheck && !playerHasValidMoves;
            var stalemate = !inCheck && !playerHasValidMoves;

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

            if(pieceReadyForPromotion == null)
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

            if(currentPlayerKingCoordinates == null)
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