namespace Chess.Game
{
    internal class EmptySquare : ISquare
    {
        public const string Fen = "E";

        public string GetFen()
        {
            return Fen;
        }
    }
}
