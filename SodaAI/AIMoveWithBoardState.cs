using SodaChess;
using SodaChess.Pieces;
using System;

namespace SodaAI
{
    public class AIMoveWithBoardState : AIMove
    {
        public int BlackScore { get; internal set; }
        public int WhiteScore { get; internal set; }
        public MoveResult MoveResult { get; private set; }
        public ChessPiece SourcePiece { get; private set; }

        public int WhiteToBlackDelta => WhiteScore - BlackScore;
        public int BlackToWhiteDelta => BlackScore - WhiteScore;

        public AIMoveWithBoardState(ChessCoordinate source, ChessCoordinate destination, ChessBoardArbitrator arbitrator,
            MoveResult moveResult, ChessPiece sourcePiece, PieceType? promotion = null) :
            this(source, destination, arbitrator.BlackScore, arbitrator.WhiteScore, moveResult, sourcePiece, promotion)
        {
        }

        public AIMoveWithBoardState(ChessCoordinate source, ChessCoordinate destination, int blackScore, int whiteScore, MoveResult moveResult, ChessPiece sourcePiece,
            PieceType? promotion = null) : base(source, destination, promotion)
        {
            BlackScore = blackScore;
            WhiteScore = whiteScore;
            MoveResult = moveResult;
            SourcePiece = sourcePiece;
        }
    }
}
