using Chess.Game.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess.Gui
{
    /// <summary>
    /// Interaction logic for PromoteWindow.xaml
    /// </summary>
    public partial class PromoteWindow : Window
    {
        public PromoteWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<Type> Clicked = delegate { };

        private void QueenClickEventHandler(object sender, RoutedEventArgs e)
        {
            Clicked(this, typeof(Queen));
            Hide();
        }

        private void RookClickEventHandler(object sender, RoutedEventArgs e)
        {
            Clicked(this, typeof(Rook));
            Hide();
        }

        private void BishopClickEventHandler(object sender, RoutedEventArgs e)
        {
            Clicked(this, typeof(Bishop));
            Hide();
        }

        private void KnightClickEventHandler(object sender, RoutedEventArgs e)
        {
            Clicked(this, typeof(Knight));
            Hide();
        }
    }
}
