using HandyControl.Tools;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace LogicitApp
{
    public partial class App : Application
    {
        public App()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            ConfigHelper.Instance.SetLang("ru");
        }
    }
}
