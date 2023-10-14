using LogicitApp.Views.Shared;

namespace LogicitApp.Views.Shared
{
    public enum AvailableViews
    {
        LoginView,
        MainView,
        CreateOrderView,
        //AppliedEntityView
        ProductsView
    }
}

namespace LogicitApp.Shared
{
    public static class StaticAvailableViews
    {
        public static AvailableViews LoginViewEnum => AvailableViews.LoginView;
        public static AvailableViews MainViewEnum => AvailableViews.MainView;
        public static AvailableViews CreateOrderViewEnum => AvailableViews.CreateOrderView;
        public static AvailableViews ProductsView => AvailableViews.ProductsView;
        //public static AvailableViews AppliedEntityView => AvailableViews.AppliedEntityView;
    }
}
