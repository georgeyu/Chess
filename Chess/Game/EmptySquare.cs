using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    /// <summary>
    /// Represents an empty square.
    /// </summary>
    internal class EmptySquare : Square
    {
        public string GetFen()
        {
            return Constants.EmptySquare;
        }
    }
}
