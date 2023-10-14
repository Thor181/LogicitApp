using LogicitApp.Shared.Commands;
using LogicitApp.Shared.Extensions;
using LogicitApp.Views.Shared;
using System.ComponentModel;
using System.Windows;

namespace LogicitApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _menuIsEnabled = false;
        public bool MenuIsEnabled
        {
            get => _menuIsEnabled; set
            {
                _menuIsEnabled = value;
                PropertyChanged?.Invoke(this, new(nameof(MenuIsEnabled)));
            }
        }

        public SimpleCommand ChangeViewCommand { get; set; }

        public static MainWindow Instance { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            ChangeViewCommand = new SimpleCommand(ChangeView);

            InitializeComponent();
            Instance = this;
        }

        public void ChangeView(object? view)
        {
            ChangeView((AvailableViews)view);
        }

        public static void ChangeView(AvailableViews view)
        {
            var mw = (MainWindow)App.Current.MainWindow;
            mw.MainLayout.Children.Clear();
            mw.MainLayout.Children.Add(view.View());

            mw.MenuIsEnabled = view != AvailableViews.LoginView;
        }
    }
}