using Chess.Game;
using Chess.Game.Pieces;
using Chess.Properties;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            GetSquares(position.Board);
        }

        public List<BoardSquare> Squares { get; private set; }

        private void GetSquares(Board board)
        {
            var squares = new List<BoardSquare>();
            for (var i = board.RankCount - 1; i >=  0; i--)
            {
                for (var j = 0; j < board.FileCount; j++)
                {
                    BoardSquare boardSquare = GetBoardSquare(board.Squares[j, i], i, j);
                    squares.Add(boardSquare);
                }
            }
            Squares = squares;
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
