using HandyControl.Properties.Langs;
using HandyControl.Tools;
using LogicitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogicitApp.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            
            ConfigHelper.Instance.SetLang("RU");

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
        }
    }
}
