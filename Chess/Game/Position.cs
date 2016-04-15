using Chess.Game.Pieces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess.Game
{
    internal class Position
    {
        private const int EmptySquareOffset = 2;
        private const int WhitePawnRank = 1;
        private const int BlackPawnRank = Constants.BoardLength - 2;
        private const int WhitePieceRank = 0;
        private const int BlackPieceRank = Constants.BoardLength - 1;
        private const int RookFileOffset = 0;
        private const int KnightFileOffset = 1;
        private const int BishopFileOffset = 2;
        private const int QueenFile = 3;
        private const int KingFile = 4;
        private const int FileIndex = 0;
        private const int RankIndex = 1;
        private const string FenRankSeparator = "/";
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Position()
        {
            IsWhiteTurn = true;
            TurnNumber = 1;
            Board = new Square[Constants.BoardLength, Constants.BoardLength];
            SetupBoard();
        }

        public Position(Square[,] board, bool isWhiteTurn = true, int turnNumber = 1)
        {
            Board = board;
            IsWhiteTurn = isWhiteTurn;
            TurnNumber = turnNumber;
        }

        public bool IsWhiteTurn { get; private set; }

        public int TurnNumber { get; private set; }

        public Square[,] Board { get; private set; }

        /// <summary>
        /// Gets legal moves.
        /// </summary>
        public MoveAbsolute[] GetMoves()
        {
            var moves = MoveGetter.GetMoves(this);
            return moves;
        }

        /// <summary>
        /// Gets legal captures.
        /// </summary>
        public CaptureAbsolute[] GetCaptures()
        {
            List<CaptureAbsolute> capturesIgnoringLegality = GetCapturesIgnoringLegality();
            List<CaptureAbsolute> capturesStayingOnBoard = GetCapturesStayingOnBoard(capturesIgnoringLegality);
            List<CaptureAbsolute> capturesWherePassingSquaresAreEmpty = GetCapturesWherePassingSquaresAreEmpty(capturesStayingOnBoard);
            List<CaptureAbsolute> capturesWhereFinalSquareIsEnemyPiece = GetCapturesWhereFinalSquareIsEnemyPiece(capturesWherePassingSquaresAreEmpty);
            var captures = capturesWhereFinalSquareIsEnemyPiece.ToArray();
            return captures;
        }

        /// <summary>
        /// Changes the position to match the move made.
        /// </summary>
        /// <param name="move">Move to make.</param>
        public void MakeMove(MoveAbsolute move)
        {
            var startSquare = Board[move.StartSquare.File, move.StartSquare.Rank];
            Board[move.StartSquare.File, move.StartSquare.Rank] = new EmptySquare();
            SquareAbsolute finalSquare = move.PassingSquares.Last();
            var piece = startSquare as Piece;
            piece.HasMoved = true;
            Board[finalSquare.File, finalSquare.Rank] = piece;
            IncrementTurn();
        }

        /// <summary>
        /// Changes the position to match the capture made.
        /// </summary>
        /// <param name="capture">Capture to make.</param>
        public void MakeCapture(CaptureAbsolute capture)
        {
            var startSquare = Board[capture.StartSquare.File, capture.StartSquare.Rank];
            var piece = startSquare as Piece;
            Board[capture.StartSquare.File, capture.StartSquare.Rank] = new EmptySquare();
            piece.HasMoved = true;
            Board[capture.CaptureSquare.File, capture.CaptureSquare.Rank] = piece;
            IncrementTurn();
        }

        /// <summary>
        /// Get FEN for current position.
        /// </summary>
        public string GetFen()
        {
            int files = Board.GetLength(FileIndex);
            int ranks = Board.GetLength(RankIndex);
            var fileFens = new List<string>();
            for (var i = files - 1; i >= 0; i--)
            {
                string fileFen = GetRankFen(i);
                fileFens.Add(fileFen);
            }
            string boardFen = String.Join(FenRankSeparator, fileFens);
            string fen = ReplaceConsecutiveEmptySquaresWithIntegers(boardFen);
            return fen;
        }

        private void SetupBoard()
        {
            int files = Board.GetLength(FileIndex);
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                for (var j = 0; j < Constants.BoardLength; j++)
                {
                    Board[i, j] = new EmptySquare();
                }
            }
            for (var i = 0; i < Constants.BoardLength; i++)
            {
                Board[i, WhitePawnRank] = new Pawn(true);
                Board[i, BlackPawnRank] = new Pawn(false);
            }
            Board[RookFileOffset, WhitePieceRank] = new Rook(true);
            Board[KnightFileOffset, WhitePieceRank] = new Knight(true);
            Board[BishopFileOffset, WhitePieceRank] = new Bishop(true);
            Board[QueenFile, WhitePieceRank] = new Queen(true);
            Board[KingFile, WhitePieceRank] = new King(true);
            Board[Constants.BoardLength - BishopFileOffset - 1, WhitePieceRank] = new Bishop(true);
            Board[Constants.BoardLength - KnightFileOffset - 1, WhitePieceRank] = new Knight(true);
            Board[Constants.BoardLength - RookFileOffset - 1, WhitePieceRank] = new Rook(true);
            Board[RookFileOffset, BlackPieceRank] = new Rook(false);
            Board[KnightFileOffset, BlackPieceRank] = new Knight(false);
            Board[BishopFileOffset, BlackPieceRank] = new Bishop(false);
            Board[QueenFile, BlackPieceRank] = new Queen(false);
            Board[KingFile, BlackPieceRank] = new King(false);
            Board[Constants.BoardLength - BishopFileOffset - 1, BlackPieceRank] = new Bishop(false);
            Board[Constants.BoardLength - KnightFileOffset - 1, BlackPieceRank] = new Knight(false);
            Board[Constants.BoardLength - RookFileOffset - 1, BlackPieceRank] = new Rook(false);
        }

        private List<CaptureAbsolute> GetCapturesWherePassingSquaresAreEmpty(List<CaptureAbsolute> captures)
        {
            var capturesWherePassingSquaresAreEmpty = new List<CaptureAbsolute>();
            foreach (CaptureAbsolute capture in captures)
            {
                List<CaptureAbsolute> captureWherePassingSquaresAreEmpty = GetCaptureWherePassingSquaresAreEmpty(capture);
                capturesWherePassingSquaresAreEmpty.AddRange(captureWherePassingSquaresAreEmpty);
            }
            return capturesWherePassingSquaresAreEmpty;
        }

        private List<CaptureAbsolute> GetCaptureWherePassingSquaresAreEmpty(CaptureAbsolute capture)
        {
            var captures = new List<CaptureAbsolute>();
            var arePassingSquaresEmpty = capture.PassingSquares.Select(x => Board[x.File, x.Rank] is EmptySquare);
            if (arePassingSquaresEmpty.Count() != 0)
            {
                var areAllPassingSquaresEmpty = arePassingSquaresEmpty.Aggregate((x, y) => x && y);
                if (!areAllPassingSquaresEmpty)
                {
                    return captures;
                }
            }
            captures.Add(capture);
            return captures;
        }

        private List<CaptureAbsolute> GetCapturesStayingOnBoard(List<CaptureAbsolute> captures)
        {
            var capturesStayingOnBoard = new List<CaptureAbsolute>();
            foreach (CaptureAbsolute capture in captures)
            {
                List<CaptureAbsolute> captureStayingOnBoard = GetCaptureStayingOnBoard(capture);
                capturesStayingOnBoard.AddRange(captureStayingOnBoard);
            }
            return capturesStayingOnBoard;
        }

        private List<CaptureAbsolute> GetCaptureStayingOnBoard(CaptureAbsolute capture)
        {
            var captureStayingOnBoard = new List<CaptureAbsolute>();
            int files = Board.GetLength(FileIndex);
            int ranks = Board.GetLength(RankIndex);
            if (capture.PassingSquares.Length != 0)
            {
                var doPassingSquaresStayOnBoard = capture.PassingSquares.Select(x => (
                (x.File < files) &&
                (x.Rank < ranks) &&
                (x.File >= 0) &&
                (x.Rank >= 0)));
                bool doAllPassingSquaresStayOnBoard = doPassingSquaresStayOnBoard.Aggregate((x, y) => x && y);
                if (!doAllPassingSquaresStayOnBoard)
                {
                    return captureStayingOnBoard;
                }
            }
            bool doesFinalSquareStayOnBoard = (capture.CaptureSquare.File < files) &&
                (capture.CaptureSquare.File >= 0) &&
                (capture.CaptureSquare.Rank < ranks) &&
                (capture.CaptureSquare.Rank >= 0);
            if (doesFinalSquareStayOnBoard)
            {
                captureStayingOnBoard.Add(capture);
            }
            return captureStayingOnBoard;
        }

        private void IncrementTurn()
        {
            if (!IsWhiteTurn)
            {
                TurnNumber++;
            }
            IsWhiteTurn = !IsWhiteTurn;
        }

        private string GetRankFen(int rank)
        {
            int files = Board.GetLength(FileIndex);
            string fen = String.Empty;
            for (var i = 0; i < files; i++)
            {
                string squareFen = Board[i, rank].GetFen();
                fen += squareFen;
            }
            return fen;
        }

        private string ReplaceConsecutiveEmptySquaresWithIntegers(string fenWithEmptySquares)
        {
            int emptyCountInt = 0;
            string[] fenSplit = Regex.Split(fenWithEmptySquares, String.Empty);
            string fen = String.Empty;
            foreach (string square in fenSplit)
            {
                if (square == Constants.EmptySquare)
                {
                    emptyCountInt++;
                    continue;
                }
                if (emptyCountInt != 0)
                {
                    var emptyCountString = emptyCountInt.ToString();
                    emptyCountInt = 0;
                    fen += emptyCountString;
                }
                fen += square;
            }
            return fen;
        }

        /// <summary>
        /// Gets all captures for the pieces on the board of the current color.
        /// </summary>
        private List<CaptureAbsolute> GetCapturesIgnoringLegality()
        {
            var captures = new List<CaptureAbsolute>();
            int files = Board.GetLength(FileIndex);
            for (int i = 0; i < files; i++)
            {
                List<CaptureAbsolute> capturesOnFile = GetCapturesOnFile(i);
                captures.AddRange(capturesOnFile);
            }
            return captures;
        }

        /// <summary>
        /// Gets all captures for the pieces on the file of the current color.
        /// </summary>
        /// <param name="file">File to get all captures on.</param>
        private List<CaptureAbsolute> GetCapturesOnFile(int file)
        {
            int ranks = Board.GetLength(RankIndex);
            var captures = new List<CaptureAbsolute>();
            for (var i = 0; i < ranks; i++)
            {
                List<CaptureAbsolute> capturesFromSquare = GetCapturesFromSquare(Board[file, i], file, i);
                captures.AddRange(capturesFromSquare);
            }
            return captures;
        }

        /// <summary>
        /// If a square has a piece, then get the captures for the current turn.
        /// </summary>
        /// <param name="square">Square to check for piece and current turn color.</param>
        private List<CaptureAbsolute> GetCapturesFromSquare(Square square, int file, int rank)
        {
            var capturesAbsolute = new List<CaptureAbsolute>();
            bool isEmpty = square is EmptySquare;
            if (isEmpty)
            {
                return capturesAbsolute;
            }
            Piece piece = square as Piece;
            if (piece == null)
            {
                log.Error("Square not recognized");
            }
            if (piece.IsWhite != IsWhiteTurn)
            {
                return capturesAbsolute;
            }
            CaptureRelative[] capturesRelative = piece.GenerateCaptures();
            var startSquare = new SquareAbsolute(file, rank);
            foreach (var captureRelative in capturesRelative)
            {
                var passingSquaresEnumerable = captureRelative.PassingSquares.Select(
                    x => new SquareAbsolute(file + x.FileChange, rank + x.RankChange));
                var passingSquaresArray = passingSquaresEnumerable.ToArray();
                var finalSquare = new SquareAbsolute(
                    file + captureRelative.CaptureSquare.FileChange,
                    rank + captureRelative.CaptureSquare.RankChange);
                var captureAbsolute = new CaptureAbsolute(startSquare, finalSquare, passingSquaresArray);
                capturesAbsolute.Add(captureAbsolute);
            }
            return capturesAbsolute;
        }

        private List<CaptureAbsolute> GetCapturesWhereFinalSquareIsEnemyPiece(List<CaptureAbsolute> captures)
        {
            var capturesWhereFinalSquareIsEnemyPiece = new List<CaptureAbsolute>();
            foreach (CaptureAbsolute capture in captures)
            {
                var finalSquare = Board[capture.CaptureSquare.File, capture.CaptureSquare.Rank];
                var isEmptySquare = finalSquare is EmptySquare;
                if (isEmptySquare)
                {
                    continue;
                }
                Square startSquare = Board[capture.StartSquare.File, capture.StartSquare.Rank];
                var startPiece = startSquare as Piece;
                var finalPiece = finalSquare as Piece;
                if (startPiece.IsWhite != finalPiece.IsWhite)
                {
                    capturesWhereFinalSquareIsEnemyPiece.Add(capture);
                }
            }
            return capturesWhereFinalSquareIsEnemyPiece;
        }
    }
}
