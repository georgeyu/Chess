using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Chess.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private BoardViewModel boardViewModel;
        private readonly PromoteWindow promoteWindow;

        public BoardWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            promoteWindow = new PromoteWindow();
            promoteWindow.Clicked += PromoteClickedEventHandler;
            GetNewBoard();
        }

        private void PromoteClickedEventHandler(object sender, Type e)
        {
            boardViewModel.PromoteClickedEventHandler(e);
        }

        private void SquareClickedEventHandler(object sender, RoutedEventArgs e)
        {
            boardViewModel.SquareClickedEventHandler(
                ((BoardSquare)((Button)sender).DataContext).File,
                ((BoardSquare)((Button)sender).DataContext).Rank);
        }

        private void PromoteEventHandler()
        {
            promoteWindow.Show();
        }

        private void WonEventHandler()
        {
            MessageBox.Show("You won.");
            GetNewBoard();
        }

        private void StalemateEventHandler()
        {
            MessageBox.Show("Stalemate");
        }

        private void LostEventHandler()
        {
            MessageBox.Show("You lost.");
        }

        private void GetNewBoard()
        {
            boardViewModel = new BoardViewModel();
            DataContext = boardViewModel;
            boardViewModel.Promoting += () => PromoteEventHandler();
            boardViewModel.Won += () => WonEventHandler();
            boardViewModel.Stalemate += () => StalemateEventHandler();
            boardViewModel.Lost += () => LostEventHandler();
        }
    }
}
