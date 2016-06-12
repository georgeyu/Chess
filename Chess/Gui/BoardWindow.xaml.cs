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
        private readonly BoardViewModel boardViewModel;
        private readonly PromoteWindow promoteWindow;

        public BoardWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            boardViewModel = new BoardViewModel();
            DataContext = boardViewModel;
            boardViewModel.Promoting += () => PromoteEventHandler();
            promoteWindow = new PromoteWindow();
            promoteWindow.Clicked += PromoteClickedEventHandler;
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
    }
}
