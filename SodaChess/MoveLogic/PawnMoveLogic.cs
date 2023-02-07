using SodaChess.MoveLogic.BaseMoveLogic;
using SodaChess.Pieces;

namespace SodaChess.MoveLogic
{
    internal class PawnMoveLogic : BasePieceLogic, IPieceMoveLogic
    {
        public PawnMoveLogic(ChessBoard board, ChessCoordinate startingCoordinate) :
            base(board, startingCoordinate)
        {
        }

        private ChessCoordinate? OneRankForward()
        {
            return startingPiece.SideType switch
            {
                SideType.White => GetOffsetCoordinate(0, +1),
                SideType.Black => GetOffsetCoordinate(0, -1),
                _ => throw new NotImplementedException()
            };
        }

        private ChessCoordinate? TwoRanksForward()
        {
            return startingPiece.SideType switch
            {
                SideType.White => GetOffsetCoordinate(0, +2),
                SideType.Black => GetOffsetCoordinate(0, -2),
                _ => throw new NotImplementedException()
            };
        }

        private ChessCoordinate? DiagonallyLeft()
        {
            return startingPiece.SideType switch
            {
                SideType.White => GetOffsetCoordinate(-1, +1),
                SideType.Black => GetOffsetCoordinate(-1, -1),
                _ => throw new NotImplementedException()
            };
        }

        private ChessCoordinate? DiagonallyRight()
        {
            return startingPiece.SideType switch
            {
                SideType.White => GetOffsetCoordinate(+1, +1),
                SideType.Black => GetOffsetCoordinate(+1, -1),
                _ => throw new NotImplementedException()
            };
        }

        private string StartingRank()
        {
            return startingPiece.SideType switch
            {
                SideType.White => "2",
                SideType.Black => "7",
                _ => throw new NotImplementedException()
            };
        }

        public IList<ChessCoordinate> GetMoveList()
        {
            // TODO: Need to implement en passant
            var moves = new List<ChessCoordinate>();

            var oneRankForward = OneRankForward();
            var twoRanksForward = TwoRanksForward();
            var diagonallyLeft = DiagonallyLeft();
            var diagonallyRight = DiagonallyRight();
            var startRank = StartingRank();

            // Move one rank forward if no piece exists there
            if (oneRankForward != null && board.GetPiece(oneRankForward) == null)
            {
                moves.Add(oneRankForward);
            }

            // Move two ranks forward if no piece exists there or between there and the pawn is on the starting rank
            if (startingCoordinate.Rank == startRank && oneRankForward != null && twoRanksForward != null &&
                    board.GetPiece(oneRankForward) == null &&
                    board.GetPiece(twoRanksForward) == null)
            {
                moves.Add(twoRanksForward);
            }

            // Capture a piece by moving diagonally left if an opponent piece exists there
            if (diagonallyLeft != null &&
                    board.GetPiece(diagonallyLeft) != null &&
                    board.GetPiece(diagonallyLeft).SideType != startingPiece.SideType)
            {
                moves.Add(diagonallyLeft);
            }

            // Capture a piece by moving diagonally right if an opponent piece exists there
            if (diagonallyRight != null &&
                    board.GetPiece(diagonallyRight) != null &&
                    board.GetPiece(diagonallyRight).SideType != startingPiece.SideType)
            {
                moves.Add(diagonallyRight);
            }

            return moves;
        }
    }
}
