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

        public BoardViewModel()
        {
            position = new Position();
            Squares = new ObservableCollection<BoardSquare>();
            UpdateSquares();
        }

        public ObservableCollection<BoardSquare> Squares { get; private set; }

        public void ClickEventHandler(int file, int rank)
        {
            List<Move> moves = position.GetMoves();
            var random = new Random();
            var index = random.Next(moves.Count);
            Move move = moves.ElementAt(index);
            move.MakeMove(position);
            UpdateSquares();
        }

        private void OnSquareClick(BoardSquare square)
        {
            MessageBox.Show("aoeu");
            MessageBox.Show(string.Format("you clicked on file: {0} and rank: {1}", square.File, square.Rank));
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
    }
}
