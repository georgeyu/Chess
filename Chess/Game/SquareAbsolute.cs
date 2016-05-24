namespace Chess.Game
{
    /// <summary>
    /// Represents an absolute square.
    /// </summary>
    internal class SquareAbsolute
    {
        public SquareAbsolute(int file, int rank)
        {
            File = file;
            Rank = rank;
        }

        public int File { get; private set; }

        public int Rank { get; private set; }
    }
}
