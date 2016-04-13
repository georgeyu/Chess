using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    internal class EmptySquare : Square
    {
        public string GetFen()
        {
            return Constants.EmptySquare;
        }
    }
}
