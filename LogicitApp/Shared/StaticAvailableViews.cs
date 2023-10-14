using LogicitApp.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared
{
    public static class StaticAvailableViews
    {
        public static AvailableViews LoginViewEnum => AvailableViews.LoginView;
        public static AvailableViews MainViewEnum => AvailableViews.MainView;
        public static AvailableViews CreateOrderViewEnum => AvailableViews.CreateOrderView;
    }
}
