using Chess.Game;
using Chess.Game.Moves;
using Chess.Game.Pieces;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Chess.Gui
{
    internal class BoardViewModel
    {
        private const string ImageDirectory = "Images";
        private const string White = "white";
        private const string Black = "black";
        private const string Pawn = "pawn";
        private const string Knight = "knight";
        private const string Bishop = "bishop";
        private const string Rook = "rook";
        private const string Queen = "queen";
        private const string King = "king";
        private const string PngExtension = "png";
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Position position;
        private BoardVector startSquare;
        private IEnumerable<Promote> promoteMoves;

        public BoardViewModel()
        {
            position = new Position();
            Squares = new ObservableCollection<BoardSquare>();
            UpdateSquares();
        }

        public Action Promoting = delegate { };

        public ObservableCollection<BoardSquare> Squares { get; private set; }

        public void SquareClickedEventHandler(int file, int rank)
        {
            var clickedSquareVector = new BoardVector(file, rank);
            if (startSquare == null)
            {
                startSquare = clickedSquareVector;
                return;
            }
            List<Move> moves = position.GetMoves();
            IEnumerable<Move> matchingMoves = moves
                .Where(x => x.StartSquareVector.Equals(startSquare))
                .Where(x => x.EndSquareVector.Equals(clickedSquareVector));
            Move clickedMove = matchingMoves.FirstOrDefault();
            if (clickedMove == null)
            {
                startSquare = null;
                return;
            }
            else if (clickedMove is Promote)
            {
                promoteMoves = matchingMoves.Select(x => (Promote)x);
                Promoting();
                return;
            }
            else
            {
                MakeMove(clickedMove);
            }
        }

        public void PromoteClickedEventHandler(Type pieceType)
        {
            Move move = promoteMoves.Where(x => x.PromotedPiece.GetType() == pieceType).First();
            MakeMove(move);
        }

        private void UpdateSquares()
        {
            Squares.Clear();
            for (var i = position.Board.RankCount - 1; i >= 0; i--)
            {
                for (var j = 0; j < position.Board.FileCount; j++)
                {
                    BoardSquare boardSquare = GetBoardSquare(position.Board.Squares[j, i], i, j);
                    Squares.Add(boardSquare);
                }
            }
        }

        private BoardSquare GetBoardSquare(ISquare square, int rank, int file)
        {
            if (square is EmptySquare)
            {
                return new BoardSquare("", rank, file);
            }
            var piece = (Piece)square;
            var pieceString = "";
            if (piece is Pawn)
            {
                pieceString = Pawn;
            }
            else if (piece is Knight)
            {
                pieceString = Knight;
            }
            else if (piece is Bishop)
            {
                pieceString = Bishop;
            }
            else if (piece is Rook)
            {
                pieceString = Rook;
            }
            else if (piece is Queen)
            {
                pieceString = Queen;
            }
            else if (piece is King)
            {
                pieceString = King;
            }
            var fullPath = Path.GetFullPath(
                ImageDirectory + "/" + (piece.White ? White : Black) + pieceString + "." + PngExtension);
            return new BoardSquare(fullPath, rank, file);
        }

        private void MakeMove(Move move)
        {
            move.MakeMove(position);
            var random = new Random();
            List<Move> enemyMoves = position.GetMoves();
            var index = random.Next(enemyMoves.Count);
            Move enemyMove = enemyMoves.ElementAt(index);
            enemyMove.MakeMove(position);
            startSquare = null;
            UpdateSquares();
        }
    }
}
