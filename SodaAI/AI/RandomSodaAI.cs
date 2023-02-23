using SodaChess;
using SodaChess.Pieces;

namespace SodaAI.AI
{
    public class RandomSodaAI
    {
        private readonly ChessBoardArbitrator arbitrator;

        public RandomSodaAI(ChessBoardArbitrator arbitrator)
        {
            this.arbitrator = arbitrator;
        }

        public AIMove FindMoveForCurrentPlayer()
        {
            var pieceCoordinates = GetCoordinatesOfCurrentPlayerPieces();

            var validMoves = FindAllNonCastlingValidMoves(pieceCoordinates);

            var possibleKingCoordinate = arbitrator.CurrentPlayerSide == SideType.White ?
                new ChessCoordinate("E", "1") : new ChessCoordinate("E", "8");
            var validCastlingMoves = FindAllValidCastlingMoves(possibleKingCoordinate);

            validMoves.AddRange(validCastlingMoves);            

            var random = new Random();
            var randomIndex = random.Next(0, validMoves.Count);
            var randomMove = validMoves[randomIndex];

            return randomMove;
        }

        private List<AIMove> FindAllNonCastlingValidMoves(IEnumerable<ChessCoordinate> pieceCoordinates)
        {
            var possibleMoves = new List<AIMove>();

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
                        possibleMoves.Add(new AIMove(source, destination));
                    }

                    if (result == MoveResult.PromotionInputNeeded)
                    {
                        possibleMoves.Add(new AIMove(source, destination, PieceType.Queen));
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

        private IList<AIMove> FindAllValidCastlingMoves(ChessCoordinate kingCoordinate)
        {
            var validMoves = new List<AIMove>();

            var kingPiece = arbitrator.GetPiece(kingCoordinate);

            if (kingPiece == null ||
                (kingPiece.PieceType != PieceType.King && kingPiece.SideType != SideType.White))
            {
                return validMoves;
            }

            var queenSideCoordinate = new ChessCoordinate("C", kingCoordinate.Rank);
            if (AttemptCastle(kingCoordinate, queenSideCoordinate))
            {
                validMoves.Add(new AIMove(kingCoordinate, queenSideCoordinate));
            }

            var kingSideCoordinate = new ChessCoordinate("G", kingCoordinate.Rank);
            if (AttemptCastle(kingCoordinate, kingSideCoordinate))
            {
                validMoves.Add(new AIMove(kingCoordinate, kingSideCoordinate));
            }

            return validMoves;
        }

        private bool AttemptCastle(ChessCoordinate kingSource, ChessCoordinate kingDestination)
        {
            var arbitratorCopy = new ChessBoardArbitrator(arbitrator);
            var result = arbitratorCopy.MakeMove(kingSource, kingDestination);

            // Don't need to check side type since a player cannot put themselves into check or checkmate,
            // so their side result will never happen from a castle
            return result == MoveResult.Valid ||
                   result == MoveResult.ValidWhiteInCheck ||
                   result == MoveResult.ValidWhiteInCheckmate ||
                   result == MoveResult.ValidBlackInCheck ||
                   result == MoveResult.ValidBlackInCheckmate;
        }
    }
}