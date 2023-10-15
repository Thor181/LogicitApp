using LogicitApp.Views;
using LogicitApp.Views.AppliedEntities;
using LogicitApp.Views.Shared;
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
                AvailableViews.ProductsView => new ProductsView(),
                AvailableViews.DriversView => new DriversView(),
                AvailableViews.TransportView => new TransportView(),
                AvailableViews.ClientsView => new ClientsView(),

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
