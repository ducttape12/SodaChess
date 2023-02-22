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
            var pieceCoordinates = GetCoordinatesOfCurrentPlayerPieces(arbitrator);

            var validMoves = FindAllValidMoves(pieceCoordinates);
            // TODO: Adding castling support

            var random = new Random();
            var randomIndex = random.Next(0, validMoves.Count);
            var randomMove = validMoves[randomIndex];

            return randomMove;
        }

        private IList<AIMove> FindAllValidMoves(IEnumerable<ChessCoordinate> pieceCoordinates)
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

                // Castling check
                //var sourcePiece = arbitrator.GetPiece(source);

                //if(arbitrator.CurrentPlayerSide == SideType.White &&
                //    source.Rank == "E" && source.File == "1" &&
                //    sourcePiece != null &&
                //    sourcePiece.PieceType == PieceType.King)
                //{
                //    var arbitratorCopy = new ChessBoardArbitrator(arbitrator);


                //}
            }

            return possibleMoves;
        }

        private static IEnumerable<ChessCoordinate> GetCoordinatesOfCurrentPlayerPieces(ChessBoardArbitrator arbitrator)
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
    }
}