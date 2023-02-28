using SodaChess;
using SodaChess.Pieces;

namespace SodaAI.AI
{
    public class BaseAI
    {
        private readonly ChessBoardArbitrator arbitrator;

        public BaseAI(ChessBoardArbitrator arbitrator)
        {
            this.arbitrator = arbitrator;
        }

        protected MoveResult OpponentCheckmate => arbitrator.CurrentPlayerSide == SideType.White ?
            MoveResult.ValidBlackInCheckmate : MoveResult.ValidWhiteInCheckmate;

        protected int CurrentPlayerDelta(AIMoveWithBoardState move)
        {
            if(arbitrator.CurrentPlayerSide == SideType.White)
            {
                return move.WhiteToBlackDelta;
            }

            return move.BlackToWhiteDelta;
        }

        protected IList<AIMoveWithBoardState> FindAllValidMoves()
        {
            var pieceCoordinates = GetCoordinatesOfCurrentPlayerPieces();

            var validMoves = FindAllNonCastlingValidMoves(pieceCoordinates);

            var possibleKingCoordinate = arbitrator.CurrentPlayerSide == SideType.White ?
                new ChessCoordinate("E", "1") : new ChessCoordinate("E", "8");
            var validCastlingMoves = FindAllValidCastlingMoves(possibleKingCoordinate);

            validMoves.AddRange(validCastlingMoves);

            return validMoves;
        }

        private List<AIMoveWithBoardState> FindAllNonCastlingValidMoves(IEnumerable<ChessCoordinate> pieceCoordinates)
        {
            var possibleMoves = new List<AIMoveWithBoardState>();

            foreach (var source in pieceCoordinates)
            {
                var destinations = arbitrator.ValidMoves(source);

                foreach (var destination in destinations)
                {
                    var arbitratorCopy = new ChessBoardArbitrator(arbitrator);

                    var result = arbitratorCopy.MakeMove(source, destination);

                    if (result == MoveResult.Valid ||
                        result == MoveResult.ValidBlackInCheck ||
                        result == MoveResult.ValidWhiteInCheck ||
                        result == MoveResult.ValidBlackInCheckmate ||
                        result == MoveResult.ValidWhiteInCheckmate ||
                        result == MoveResult.ValidStalemate)
                    {
                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, arbitratorCopy, result));
                    }

                    if (result == MoveResult.PromotionInputNeeded)
                    {
                        // TODO: Promote to something other than queen?
                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, arbitratorCopy, result, PieceType.Queen));
                    }
                }
            }

            return possibleMoves;
        }

        private IEnumerable<ChessCoordinate> GetCoordinatesOfCurrentPlayerPieces()
        {
            foreach (var file in Coordinates.Files)
            {
                foreach (var rank in Coordinates.Ranks)
                {
                    var coordinate = new ChessCoordinate(file, rank);

                    var piece = arbitrator.GetPiece(coordinate);

                    if (piece != null && piece.SideType == arbitrator.CurrentPlayerSide)
                    {
                        yield return coordinate;
                    }
                }
            }
        }

        private IList<AIMoveWithBoardState> FindAllValidCastlingMoves(ChessCoordinate kingCoordinate)
        {
            var validMoves = new List<AIMoveWithBoardState>();

            var kingPiece = arbitrator.GetPiece(kingCoordinate);

            if (kingPiece == null ||
                (kingPiece.PieceType != PieceType.King && kingPiece.SideType != SideType.White))
            {
                return validMoves;
            }

            var queenSideCoordinate = new ChessCoordinate("C", kingCoordinate.Rank);
            var queenSideCastleMove = AttemptCastle(kingCoordinate, queenSideCoordinate);

            if (queenSideCastleMove != null)
            {
                validMoves.Add(queenSideCastleMove);
            }

            var kingSideCoordinate = new ChessCoordinate("G", kingCoordinate.Rank);
            var kingSideCastleMove = AttemptCastle(kingCoordinate, kingSideCoordinate);

            if(kingSideCastleMove != null)
            {
                validMoves.Add(kingSideCastleMove);
            }

            return validMoves;
        }

        private AIMoveWithBoardState? AttemptCastle(ChessCoordinate kingSource, ChessCoordinate kingDestination)
        {
            var arbitratorCopy = new ChessBoardArbitrator(arbitrator);
            var result = arbitratorCopy.MakeMove(kingSource, kingDestination);

            // Will never get any other invalid results
            if (result == MoveResult.InvalidNoMoveMade)
            {
                return null;
            }

            return new AIMoveWithBoardState(kingSource, kingDestination, arbitratorCopy, result);
        }

        protected AIMove GetRandomMove(IList<AIMoveWithBoardState> possibleMoves)
        {
            var random = new Random();
            var randomIndex = random.Next(0, possibleMoves.Count);
            var randomMove = possibleMoves[randomIndex];

            return randomMove;
        }
    }
}
