using SodaChess;
using SodaChess.Pieces;

namespace SodaAI.AI
{
    public class BaseAI
    {
        private ChessBoardArbitrator Arbitrator { get; set; }

        protected bool IsControl { get; private set; }
        protected bool IsTreatment { get; private set; }

        public BaseAI(bool isControl)
        {
            IsControl = isControl;
            IsTreatment = !isControl;
        }

        public BaseAI()
        {
            IsControl = true;
            IsTreatment = false;
        }

        protected void Initialize(ChessBoardArbitrator arbitrator)
        {
            Arbitrator = arbitrator;
        }

        protected MoveResult OpponentCheckmate => Arbitrator.CurrentPlayerSide == SideType.White ?
            MoveResult.ValidBlackInCheckmate : MoveResult.ValidWhiteInCheckmate;

        protected int CurrentPlayerDelta(AIMoveWithBoardState move)
        {
            if(Arbitrator.CurrentPlayerSide == SideType.White)
            {
                return move.WhiteToBlackDelta;
            }

            return move.BlackToWhiteDelta;
        }

        protected IList<AIMoveWithBoardState> FindAllValidMoves()
        {
            var pieceCoordinates = GetCoordinatesOfCurrentPlayerPieces();

            var validMoves = FindAllNonCastlingValidMoves(pieceCoordinates);

            var possibleKingCoordinate = Arbitrator.CurrentPlayerSide == SideType.White ?
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
                var destinations = Arbitrator.ValidMoves(source);

                foreach (var destination in destinations)
                {
                    var arbitratorCopy = new ChessBoardArbitrator(Arbitrator);
                    var piece = arbitratorCopy.GetPiece(source); 

                    var result = arbitratorCopy.MakeMove(source, destination);

                    if (result == MoveResult.Valid ||
                        result == MoveResult.ValidBlackInCheck ||
                        result == MoveResult.ValidWhiteInCheck ||
                        result == MoveResult.ValidBlackInCheckmate ||
                        result == MoveResult.ValidWhiteInCheckmate ||
                        result == MoveResult.ValidStalemate)
                    {
                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, arbitratorCopy, result, piece!));
                    }

                    if (result == MoveResult.PromotionInputNeeded)
                    {
                        // TODO: Promote to something other than queen?
                        possibleMoves.Add(new AIMoveWithBoardState(source, destination, arbitratorCopy, result, piece!, PieceType.Queen));
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

                    var piece = Arbitrator.GetPiece(coordinate);

                    if (piece != null && piece.SideType == Arbitrator.CurrentPlayerSide)
                    {
                        yield return coordinate;
                    }
                }
            }
        }

        private IList<AIMoveWithBoardState> FindAllValidCastlingMoves(ChessCoordinate kingCoordinate)
        {
            var validMoves = new List<AIMoveWithBoardState>();

            var kingPiece = Arbitrator.GetPiece(kingCoordinate);

            if (kingPiece == null ||
                (kingPiece.PieceType != PieceType.King && kingPiece.SideType != SideType.White))
            {
                return validMoves;
            }

            var queenSideCoordinate = new ChessCoordinate("C", kingCoordinate.Rank);
            var queenSideCastleMove = AttemptCastle(kingPiece, kingCoordinate, queenSideCoordinate);

            if (queenSideCastleMove != null)
            {
                validMoves.Add(queenSideCastleMove);
            }

            var kingSideCoordinate = new ChessCoordinate("G", kingCoordinate.Rank);
            var kingSideCastleMove = AttemptCastle(kingPiece, kingCoordinate, kingSideCoordinate);

            if(kingSideCastleMove != null)
            {
                validMoves.Add(kingSideCastleMove);
            }

            return validMoves;
        }

        private AIMoveWithBoardState? AttemptCastle(ChessPiece king, ChessCoordinate kingSource, ChessCoordinate kingDestination)
        {
            var arbitratorCopy = new ChessBoardArbitrator(Arbitrator);
            var result = arbitratorCopy.MakeMove(kingSource, kingDestination);

            if (result == MoveResult.InvalidNoMoveMade)
            {
                return null;
            }

            return new AIMoveWithBoardState(kingSource, kingDestination, arbitratorCopy, result, king);
        }

        protected static AIMove GetRandomMove(IList<AIMoveWithBoardState> possibleMoves)
        {
            var randomIndex = Random.Shared.Next(0, possibleMoves.Count);
            var randomMove = possibleMoves[randomIndex];

            return randomMove;
        }

        public override string ToString()
        {
            return $"{this.GetType()}, IsControl? {IsControl}";
        }
    }
}
