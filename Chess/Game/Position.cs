﻿using Chess.Game.Pieces;
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
        private const int BlackPawnRank = Constants.BoardDimension - 2;
        private const int WhitePieceRank = 0;
        private const int BlackPieceRank = Constants.BoardDimension - 1;
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
            Board = new Square[Constants.BoardDimension, Constants.BoardDimension];
            SetupStartPosition();
        }

        public Position(bool isWhiteTurn, int moveNumber)
        {
            IsWhiteTurn = isWhiteTurn;
            TurnNumber = moveNumber;
            throw new NotImplementedException();
        }

        public bool IsWhiteTurn { get; private set; }

        public int TurnNumber { get; private set; }

        // The 0th dimension is the file. The 1st dimension is the rank.
        public Square[,] Board { get; private set; }

        /// <summary>
        /// Gets legal moves.
        /// </summary>
        public MoveAbsolute[] GetMoves()
        {
            List<MoveAbsolute> movesIgnoringLegality = GetMovesIgnoringLegality();
            List<MoveAbsolute> movesStayingOnBoard = GetMovesStayingOnBoard(movesIgnoringLegality);
            List<MoveAbsolute> movesWherePassingSquaresAreEmpty = GetMovesWherePassingSquaresAreEmpty(
                movesStayingOnBoard);
            var moves = movesWherePassingSquaresAreEmpty.ToArray();
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
            Board[capture.FinalSquare.File, capture.FinalSquare.Rank] = piece;
            IncrementTurn();
        }

        private void SetupStartPosition()
        {
            FillWithEmptySquares();
            FillWithPawns();
            FillDoublePieces();
            FillSinglePieces();
        }

        private void FillWithEmptySquares()
        {
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                FillFileWithEmptySquares(i);
            }
        }

        private void FillFileWithEmptySquares(int rank)
        {
            var emptySquare = new EmptySquare();
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                Board[rank, i] = emptySquare;
            }
        }

        private void FillWithPawns()
        {
            Piece whitePawn = new Pawn(true, false);
            Piece blackPawn = new Pawn(false, false);
            for (var i = 0; i < Constants.BoardDimension; i++)
            {
                Board[i, WhitePawnRank] = whitePawn;
                Board[i, BlackPawnRank] = blackPawn;
            }
        }

        /// <summary>
        /// Fill with pieces that have a copy with the same color (knight, bishop and rook).
        /// </summary>
        private void FillDoublePieces()
        {
            Piece whiteKnight = new Knight(true, false);
            Piece whiteBishop = new Bishop(true, false);
            Piece whiteRook = new Rook(true, false);
            Piece blackKnight = new Knight(false, false);
            Piece blackBishop = new Bishop(false, false);
            Piece blackRook = new Rook(false, false);
            FillPieceTwice(whiteKnight, KnightFileOffset);
            FillPieceTwice(whiteBishop, BishopFileOffset);
            FillPieceTwice(whiteRook, RookFileOffset);
            FillPieceTwice(blackKnight, KnightFileOffset);
            FillPieceTwice(blackBishop, BishopFileOffset);
            FillPieceTwice(blackRook, RookFileOffset);
        }

        private void FillPieceTwice(Piece piece, int offset)
        {
            int rank = piece.IsWhite ? WhitePieceRank : BlackPieceRank;
            Board[offset, rank] = piece;
            Board[Constants.BoardDimension - 1 - offset, rank] = piece;
        }

        /// <summary>
        /// Fill with pieces that do not have a copy (king and queen).
        /// </summary>
        private void FillSinglePieces()
        {
            Piece whiteQueen = new Queen(true, false);
            Piece whiteKing = new King(true, false);
            Piece blackQueen = new Queen(false, false);
            Piece blackKing = new King(false, false);
            Board[QueenFile, WhitePieceRank] = whiteQueen;
            Board[KingFile, WhitePieceRank] = whiteKing;
            Board[QueenFile, BlackPieceRank] = blackQueen;
            Board[KingFile, BlackPieceRank] = blackKing;
        }

        /// <summary>
        /// Gets all moves for the pieces on the board of the current color.
        /// </summary>
        private List<MoveAbsolute> GetMovesIgnoringLegality()
        {
            var moves = new List<MoveAbsolute>();
            int files = Board.GetLength(FileIndex);
            for (int i = 0; i < files; i++)
            {
                List<MoveAbsolute> movesOnFile = GetMovesOnFile(i);
                moves.AddRange(movesOnFile);
            }
            return moves;
        }

        /// <summary>
        /// Gets all moves for the pieces on the file of the current color.
        /// </summary>
        /// <param name="file"></param>
        private List<MoveAbsolute> GetMovesOnFile(int file)
        {
            int ranks = Board.GetLength(RankIndex);
            var moves = new List<MoveAbsolute>();
            for (int i = 0; i < ranks; i++)
            {
                List<MoveAbsolute> movesFromSquare = GetMovesFromSquare(Board[file, i], file, i);
                moves.AddRange(movesFromSquare);
            }
            return moves;
        }

        /// <summary>
        /// If a square has a piece, then get the moves for the current turn.
        /// </summary>
        /// <param name="square">Square to check for piece and current turn color.</param>
        private List<MoveAbsolute> GetMovesFromSquare(Square square, int file, int rank)
        {
            var movesAbsolute = new List<MoveAbsolute>();
            bool isEmpty = square is EmptySquare;
            if (isEmpty)
            {
                return movesAbsolute;
            }
            Piece piece = square as Piece;
            if (piece == null)
            {
                log.Error("Square not recognized");
            }
            if (piece.IsWhite != IsWhiteTurn)
            {
                return movesAbsolute;
            }
            SquareChange[][] movesRelative = piece.GetMoves();
            var startSquare = new SquareAbsolute(file, rank);
            foreach (var moveRelative in movesRelative)
            {
                var squaresAbsoluteEnumerable = moveRelative.Select(
                    x => new SquareAbsolute(file + x.FileChange, rank + x.RankChange));
                var squaresAbsoluteArray = squaresAbsoluteEnumerable.ToArray();
                var moveAbsolute = new MoveAbsolute(startSquare, squaresAbsoluteArray);
                movesAbsolute.Add(moveAbsolute);
            }
            return movesAbsolute;
        }

        private List<MoveAbsolute> GetMovesWherePassingSquaresAreEmpty(List<MoveAbsolute> moves)
        {
            var movesWherePassingSquaresAreEmpty = new List<MoveAbsolute>();
            foreach (MoveAbsolute move in moves)
            {
                List<MoveAbsolute> moveWherePassingSquaresAreEmpty = GetMoveWherePassingSquaresAreEmpty(move);
                movesWherePassingSquaresAreEmpty.AddRange(moveWherePassingSquaresAreEmpty);
            }
            return movesWherePassingSquaresAreEmpty;
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

        private List<MoveAbsolute> GetMoveWherePassingSquaresAreEmpty(MoveAbsolute move)
        {
            var moves = new List<MoveAbsolute>();
            var arePassingSquaresEmpty = move.PassingSquares.Select(x => Board[x.File, x.Rank] is EmptySquare);
            var areAllPassingSquaresEmpty = arePassingSquaresEmpty.Aggregate((x, y) => x && y);
            if (areAllPassingSquaresEmpty)
            {
                moves.Add(move);
            }
            return moves;
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

        private List<MoveAbsolute> GetMovesStayingOnBoard(List<MoveAbsolute> moves)
        {
            var movesStayingOnBoard = new List<MoveAbsolute>();
            foreach (MoveAbsolute move in moves)
            {
                List<MoveAbsolute> moveStayingOnBoard = GetMoveStayingOnBoard(move);
                movesStayingOnBoard.AddRange(moveStayingOnBoard);
            }
            return movesStayingOnBoard;
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

        private List<MoveAbsolute> GetMoveStayingOnBoard(MoveAbsolute move)
        {
            var moveStayingOnBoard = new List<MoveAbsolute>();
            int files = Board.GetLength(FileIndex);
            int ranks = Board.GetLength(RankIndex);
            var doPassingSquaresStayOnBoard = move.PassingSquares.Select(x => (
                (x.File < files) &&
                (x.Rank < ranks) &&
                (x.File >= 0) &&
                (x.Rank >= 0)));
            bool doAllPassingSquaresStayOnBoard = doPassingSquaresStayOnBoard.Aggregate((x, y) => x && y);
            if (doAllPassingSquaresStayOnBoard)
            {
                moveStayingOnBoard.Add(move);
            }
            return moveStayingOnBoard;
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
            bool doesFinalSquareStayOnBoard = (capture.FinalSquare.File < files) &&
                (capture.FinalSquare.File >= 0) &&
                (capture.FinalSquare.Rank < ranks) &&
                (capture.FinalSquare.Rank >= 0);
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
            CaptureRelative[] capturesRelative = piece.GetCaptures();
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
                var finalSquare = Board[capture.FinalSquare.File, capture.FinalSquare.Rank];
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
