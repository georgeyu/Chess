namespace Chess.Game.Moves
{
    internal interface IMove
    {
        void MakeMove(Position position);

        void UndoMove(Position position);
    }
}
