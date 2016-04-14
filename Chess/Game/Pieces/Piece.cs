using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Pieces
{
    internal interface Piece : Square
    {
        /// <summary>
        /// The color.
        /// </summary>
        bool IsWhite { get; }

        /// <summary>
        /// Whether it has moved.
        /// </summary>
        bool HasMoved { get; set; }

        /// <summary>
        /// Generate the moves.
        /// </summary>
        /// <returns>The moves.</returns>
        SquareChange[][] GenerateMoves();

        /// <summary>
        /// Generate the captures.
        /// </summary>
        /// <returns>The captures.</returns>
        CaptureRelative[] GenerateCaptures();
    }
}
