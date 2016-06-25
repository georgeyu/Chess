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
            Selected = false;
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

        public bool Selected { get; set; }

        public Brush Color
        {
            get
            {
                if ((Rank + File) % 2 == 0)
                {
                    return Selected ? Brushes.DarkGreen : Brushes.Green;
                }
                else
                {
                    return Selected ? Brushes.Gray : Brushes.White;
                }
            }
        }
    }
}
