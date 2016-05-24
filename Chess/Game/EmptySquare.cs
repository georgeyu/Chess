namespace Chess.Game
{
    /// <summary>
    /// Represents an empty square.
    /// </summary>
    internal class EmptySquare : ISquare
    {
        public string GetFen()
        {
            return Constants.EmptySquare;
        }
    }
}
