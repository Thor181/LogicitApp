using HandyControl.Controls;
using LogicitApp.Data.Models;
using LogicitApp.Views;
using LogicitApp.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LogicitApp.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static UIElement View(this AvailableViews view)
        {
            return view switch
            {
                AvailableViews.LoginView => new LoginView(),
                AvailableViews.MainView => new MainView(),
                AvailableViews.CreateOrderView => new CreateOrderView(),
                _ => throw new NotImplementedException()
            };
        }

        public static string AsString(this Statuses status)
        {
            return status switch
            {
                Statuses.Completed => "Выполнен",
                Statuses.InProcess => "В работе",
                Statuses.AwaitComplete => "Ожидание выполнения",
                _ => throw new NotImplementedException()
            };
        }
    }
}
