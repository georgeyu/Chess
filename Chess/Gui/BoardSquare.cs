using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chess.Gui
{
    internal class BoardSquare
    {
        public BoardSquare(string path, int rank, int file)
        {
            Path = path;
            Rank = rank;
            File = file;
        }

        public string Path { get; private set; }

        public int Rank { get; private set; }
        
        public int File { get; private set; }

        public Brush Color => ((Rank + File) % 2 == 0) ? Brushes.Green : Brushes.White;
    }
}
