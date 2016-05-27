namespace Chess.Game.Moves
{
    internal abstract class Move
    {
        public void MakeMove(Position position)
        {
            Change(position);
            position.IncrementTurn();
        }

        public void UndoMove(Position position)
        {
            UndoChange(position);
            position.DecrementTurn();
        }

        public bool KingSafe(Position position)
        {
            MakeMove(position);
            bool kingSafeAfterMove = position.KingSafe();
            UndoMove(position);
            return kingSafeAfterMove;
        }

        public abstract void Change(Position position);

        public abstract void UndoChange(Position position);
    }
}
