using HandyControl.Tools;
using System.Globalization;
using System.Windows.Controls;

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
