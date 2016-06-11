using log4net;
using log4net.Config;
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

        public BoardWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            boardViewModel = new BoardViewModel();
            DataContext = boardViewModel;
        }

        private void ClickEventHandler(object sender, RoutedEventArgs e)
        {
            boardViewModel.ClickEventHandler(
                ((BoardSquare)((Button)sender).DataContext).File,
                ((BoardSquare)((Button)sender).DataContext).Rank);
        }
    }
}
