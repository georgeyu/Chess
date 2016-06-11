using System.ComponentModel;
using System.Windows.Media;

namespace Chess.Gui
{
    internal class BoardSquare
    {
        private string path;

        public BoardSquare(string path, int rank, int file)
        {
            Path = path;
            Rank = rank;
            File = file;
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public int Rank { get; private set; }
        
        public int File { get; private set; }

        public Brush Color => ((Rank + File) % 2 == 0) ? Brushes.Green : Brushes.White;
    }
}
