using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Positions
{
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
