using log4net;
using log4net.Config;
using System.Reflection;
using System.Windows;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
        }
    }
}
