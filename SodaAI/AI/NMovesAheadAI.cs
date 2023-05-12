using SodaChess;
using SodaChess.Pieces;

namespace SodaAI.AI
{
    public class NMovesAheadAI : BaseAI, ISodaAI
    {
        public NMovesAheadAI() { }

        public NMovesAheadAI(bool isControl) : base(isControl)
        {
        }

        public AIMoveWithBoardState GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator, int lookAheadsRemaining)
        {
            if (lookAheadsRemaining <= 0)
            {
                // TODO: How to surface checkmates?
                var oneMoveAheadAI = new OneMoveAheadAI();
                return oneMoveAheadAI.GetMoveForCurrentPlayer(arbitrator);
            }

            var possibleMoves = new List<AIMoveWithBoardState>();
            var pieceCoordinates = GetCoordinatesOfCurrentPlayerPieces();

            // Find all non-castling moves
            Parallel.ForEach(pieceCoordinates, source =>
            {
                var destinations = arbitrator.ValidMoves(source);

                foreach (var destination in destinations)
                {
                    var arbitratorCopy = new ChessBoardArbitrator(arbitrator);
                    var piece = arbitratorCopy.GetPiece(source);

                    var myMoveResult = arbitratorCopy.MakeMove(source, destination);

                    var promotionNeeded = false;

                    if (myMoveResult == MoveResult.PromotionInputNeeded)
                    {
                        promotionNeeded = true;
                        myMoveResult = arbitratorCopy.PromotePiece(PieceType.Queen);
                    }

                    // Stop looking beyond end game states
                    if (myMoveResult == MoveResult.ValidStalemate ||
                        myMoveResult == MoveResult.ValidWhiteInCheckmate ||
                        myMoveResult == MoveResult.ValidBlackInCheckmate)
                    {
                        var blackScore = arbitratorCopy.BlackScore;
                        var whiteScore = arbitratorCopy.WhiteScore;

                        if (arbitratorCopy.CurrentPlayerSide == SideType.White && myMoveResult == MoveResult.ValidWhiteInCheckmate)
                        {
                            blackScore = int.MaxValue;
                            whiteScore = int.MinValue;
                        }

                        if (arbitratorCopy.CurrentPlayerSide == SideType.Black && myMoveResult == MoveResult.ValidBlackInCheckmate)
                        {
                            blackScore = int.MinValue;
                            whiteScore = int.MaxValue;
                        }

                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, blackScore, whiteScore, myMoveResult, piece!,
                            promotionNeeded ? PieceType.Queen : null));
                    }
                    // Game isn't over yet; look to the next move
                    else
                    {
                        var opponentMove = GetMoveForCurrentPlayer(arbitratorCopy, lookAheadsRemaining - 1);

                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, opponentMove.BranchBlackScore,
                            opponentMove.BranchWhiteScore, myMoveResult, piece!,
                            promotionNeeded ? PieceType.Queen : null));
                    }
                }
            });

            // Castling Moves
            var kingCoordinate = arbitrator.CurrentPlayerSide == SideType.White ?
                new ChessCoordinate("E", "1") : new ChessCoordinate("E", "8");
            var kingPiece = arbitrator.GetPiece(kingCoordinate);

            if (kingPiece != null &&
                kingPiece.PieceType == PieceType.King &&
                kingPiece.SideType == arbitrator.CurrentPlayerSide)
            {
                // Attempt queen side castle
                {
                    var queenSideDestination = new ChessCoordinate("C", kingCoordinate.Rank);

                    var arbitratorCopy = new ChessBoardArbitrator(arbitrator);
                    var myMoveResult = arbitratorCopy.MakeMove(kingCoordinate, queenSideDestination);

                    if (myMoveResult != MoveResult.InvalidNoMoveMade)
                    {
                        // Stop looking beyond end game states
                        if (myMoveResult == MoveResult.ValidStalemate ||
                            myMoveResult == MoveResult.ValidWhiteInCheckmate ||
                            myMoveResult == MoveResult.ValidBlackInCheckmate)
                        {
                            var blackScore = arbitratorCopy.BlackScore;
                            var whiteScore = arbitratorCopy.WhiteScore;

                            if (arbitratorCopy.CurrentPlayerSide == SideType.White && myMoveResult == MoveResult.ValidWhiteInCheckmate)
                            {
                                blackScore = int.MaxValue;
                                whiteScore = int.MinValue;
                            }

                            if (arbitratorCopy.CurrentPlayerSide == SideType.Black && myMoveResult == MoveResult.ValidBlackInCheckmate)
                            {
                                blackScore = int.MinValue;
                                whiteScore = int.MaxValue;
                            }

                            possibleMoves.Add(new AIMoveWithBoardState(kingCoordinate, queenSideDestination, blackScore, whiteScore,
                                myMoveResult, kingPiece!));
                        }
                        // Game isn't over yet; look to the next move
                        else
                        {
                            var opponentMove = GetMoveForCurrentPlayer(arbitratorCopy, lookAheadsRemaining - 1);

                            possibleMoves.Add(new AIMoveWithBoardState(kingCoordinate, queenSideDestination, opponentMove.BranchBlackScore,
                                opponentMove.BranchWhiteScore, myMoveResult, kingPiece!));
                        }
                    }
                }

                // Attempt king side castle
                {
                    var kingSideCoordinate = new ChessCoordinate("G", kingCoordinate.Rank);

                    var arbitratorCopy = new ChessBoardArbitrator(arbitrator);
                    var myMoveResult = arbitratorCopy.MakeMove(kingCoordinate, kingSideCoordinate);

                    if (myMoveResult != MoveResult.InvalidNoMoveMade)
                    {
                        // Stop looking beyond end game states
                        if (myMoveResult == MoveResult.ValidStalemate ||
                            myMoveResult == MoveResult.ValidWhiteInCheckmate ||
                            myMoveResult == MoveResult.ValidBlackInCheckmate)
                        {
                            var blackScore = arbitratorCopy.BlackScore;
                            var whiteScore = arbitratorCopy.WhiteScore;

                            if (arbitratorCopy.CurrentPlayerSide == SideType.White && myMoveResult == MoveResult.ValidWhiteInCheckmate)
                            {
                                blackScore = int.MaxValue;
                                whiteScore = int.MinValue;
                            }

                            if (arbitratorCopy.CurrentPlayerSide == SideType.Black && myMoveResult == MoveResult.ValidBlackInCheckmate)
                            {
                                blackScore = int.MinValue;
                                whiteScore = int.MaxValue;
                            }

                            possibleMoves.Add(new AIMoveWithBoardState(kingCoordinate, kingSideCoordinate, blackScore, whiteScore,
                                myMoveResult, kingPiece!));
                        }
                        // Game isn't over yet; look to the next move
                        else
                        {
                            var opponentMove = GetMoveForCurrentPlayer(arbitratorCopy, lookAheadsRemaining - 1);

                            possibleMoves.Add(new AIMoveWithBoardState(kingCoordinate, kingSideCoordinate, opponentMove.BranchBlackScore,
                                opponentMove.BranchWhiteScore, myMoveResult, kingPiece!));
                        }
                    }
                }
            }

            var maximumDelta = possibleMoves.Max(m => CurrentPlayerDelta(arbitrator, m));

            // 4. Find the cheapest piece required to get that delta
            var cheapestPiece = possibleMoves.Where(m => CurrentPlayerDelta(arbitrator, m) == maximumDelta)
                                            .OrderBy(m => m.SourcePiece.TrueValue)
                                            .First();

            // 5. Randomly select from the possible moves
            var moves = possibleMoves.Where(m => CurrentPlayerDelta(arbitrator, m) == maximumDelta &&
                                                m.SourcePiece.PieceType == cheapestPiece.SourcePiece.PieceType)
                                    .ToList();
            var randomMove = GetRandomMove(moves);

            Console.WriteLine($"Completed analysis at depth {lookAheadsRemaining}.");

            return randomMove;
        }

        private static int CurrentPlayerDelta(ChessBoardArbitrator arbitrator, AIMoveWithBoardState move)
        {
            if (arbitrator.CurrentPlayerSide == SideType.White)
            {
                return move.WhiteToBlackBranchDelta;
            }

            return move.BlackToWhiteBranchDelta;
        }

        public AIMoveWithBoardState GetMoveForCurrentPlayer(ChessBoardArbitrator arbitrator)
        {
            Initialize(arbitrator);

            return GetMoveForCurrentPlayer(arbitrator, 3);
        }
    }
}
