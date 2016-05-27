using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Game.Pieces
{
    internal abstract class Piece : ISquare
    {
        private readonly string whiteFen;

        public Piece(string whiteFen, bool white, bool moved = false)
        {
            this.whiteFen = whiteFen;
            White = white;
            Moved = moved;
        }

        public bool White { get; private set; }

        public bool Moved { get; set; }

        public static Piece GetPiece(Type pieceType, bool white)
        {
            var pieceByType = new Dictionary<Type, Piece>
            {
                {typeof(Pawn), new Pawn(white) },
                {typeof(Knight), new Knight(white) },
                {typeof(Bishop), new Bishop(white) },
                {typeof(Rook), new Rook(white) },
                {typeof(Queen), new Queen(white) },
                {typeof(King), new King(white) }
            };
            return pieceByType[pieceType];
        }

        public string GetFen()
        {
            var blackFen = whiteFen.ToLower();
            return White ? whiteFen : blackFen;
        }

        public List<List<BoardVector>> GenerateCaptures()
        {
            List<List<BoardVector>> capturesWithoutOrigin = GenerateCapturesWithoutOrigin();
            return capturesWithoutOrigin.Select(x => PrependOrigin(x)).ToList();
        }

        public List<List<BoardVector>> GenerateEmptyMoves()
        {
            List<List<BoardVector>> emptyMovesWithoutOrigin = GenerateEmptyMovesWithoutOrigin();
            return emptyMovesWithoutOrigin.Select(x => PrependOrigin(x)).ToList();
        }

        public abstract List<List<BoardVector>> GenerateCapturesWithoutOrigin();

        public abstract List<List<BoardVector>> GenerateEmptyMovesWithoutOrigin();

        private List<BoardVector> PrependOrigin(List<BoardVector> squaresWithoutOrigin)
        {
            var squares = new List<BoardVector>() { new BoardVector(0, 0) };
            squares.AddRange(squaresWithoutOrigin);
            return squares;
        }
    }
}
