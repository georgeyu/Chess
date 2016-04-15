using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    /// <summary>
    /// Represents a square.
    /// </summary>
    internal interface Square
    {
        string GetFen();
    }
}
