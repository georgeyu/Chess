namespace Chess.Game.Pieces
{
    internal class BoardVector
    {
        public BoardVector(int file, int rank)
        {
            File = file;
            Rank = rank;
        }

        public int File { get; private set; }
        
        public int Rank { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var boardVector = obj as BoardVector;
            if (boardVector == null)
            {
                return false;
            }
            return (boardVector.File == File) && (boardVector.Rank == Rank);
        }

        public override int GetHashCode()
        {
            return File + Rank * Board.Length * 2;
        }
    }
}
