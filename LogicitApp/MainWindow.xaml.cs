using LogicitApp.Shared.Extensions;
using LogicitApp.Views.Shared;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicitApp
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        public static void ChangeView(AvailableViews view)
        {
            var mw = (MainWindow)App.Current.MainWindow;
            mw.MainLayout.Children.Clear();
            mw.MainLayout.Children.Add(view.View());
        }
    }
}