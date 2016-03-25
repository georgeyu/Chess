using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Position
{
    [DebuggerDisplay("FileChange = {FileChange}, RankChange = {RankChange}")]
    internal class SquareChange
    {
        public SquareChange(int fileChange, int rankChange)
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
            var squareChange = obj as SquareChange;
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
