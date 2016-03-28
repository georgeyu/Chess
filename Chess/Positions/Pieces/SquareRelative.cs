using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions.Pieces
{
    [DebuggerDisplay("FileChange = {FileChange}, RankChange = {RankChange}")]
    internal class SquareRelative
    {
        public SquareRelative(int fileChange, int rankChange)
        {
            FileChange = fileChange;
            RankChange = rankChange;
        }

        public int FileChange { get; private set; }

        public int RankChange { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var squareChange = obj as SquareRelative;
            if ((object)squareChange == null)
            {
                return false;
            }
            return (squareChange.FileChange == this.FileChange) && (squareChange.RankChange == this.RankChange);
        }

        public override int GetHashCode()
        {
            return FileChange ^ RankChange;
        }
    }
}
