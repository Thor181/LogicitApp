using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Data.Models.Applied;
using LogicitApp.Shared;
using LogicitApp.Shared.Commands;
using LogicitApp.Views.Shared;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class MainViewModel : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AppliedOrder> Orders { get; set; } = new();

        private Filter _filter = new();
        public Filter Filter { get { return _filter; } set { _filter = value; OnPropertyChanged(); } }

        #region Commands

        public SimpleCommand CreateCommand { get; set; }
        public SimpleCommand DeleteCommand { get; set; }
        public SimpleCommand EditCommand { get; set; }
        public SimpleCommand GenerateReportCommand { get; set; }
        public SimpleCommand FilterCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            CreateCommand = new SimpleCommand(CreateHandler);
            DeleteCommand = new SimpleCommand(DeleteHandler);
            EditCommand = new SimpleCommand(EditHandler);
            GenerateReportCommand = new SimpleCommand((e) =>
            {
                using var orderLogic = new OrderLogic();
                var order = orderLogic.Get<Order>((long)e);
                Report.Generate(order);
            });

            FilterCommand = new SimpleCommand(FilterHandler);

            Orders.CollectionChanged += CollectionChanged;


            LoadOrders();
        }

        public void LoadOrders()
        {
            using var orderLogic = new OrderLogic();
            var orders = orderLogic.GetAll<Order>().ToList();

            var appliedOrders = orders.Select(x => new AppliedOrder
            {
                Id = x.Id,
                Products = string.Join(", ", x.OrderProducts.Select(y => y.Product.Name)),
                Client = x.Client.OrganizationName,
                CreatedDatetime = x.CreatedDatetime,
                DeliveryDatetime = x.DeliveryDatetime,
                DeliveryAddress = x.DeliveryAddress,
                Driver = x.Driver.FullName,
                Status = x.Status,
                Transport = $"{x.Transport.Brand} {x.Transport.Model} ({x.Transport.PlateNumber})",
                Sum = x.Sum ?? -1
            });

            Orders.Clear();
            foreach (var item in appliedOrders)
            {
                Orders.Add(item);
            }
        }

        public void OnColumnGenerating(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
        }

        private void CreateHandler(object? parameter)
        {
            MainWindow.ChangeView(Views.Shared.AvailableViews.CreateOrderView);
        }

        private void DeleteHandler(object? parameter)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var id = Convert.ToInt64(parameter);
                using var orderLogic = new OrderLogic();
                var removeResult = orderLogic.Remove<Order>(id);

                MessageBox.Show(removeResult.Message);
                LoadOrders();
            }
        }

        private void EditHandler(object? parameter)
        {
            var id = Convert.ToInt64(parameter);
            using var orderLogic = new OrderLogic();
            var order = orderLogic.Get<Order>(id);

            Messenger.AddMessage(nameof(CreateOrderViewModel), order);

            MainWindow.ChangeView(Views.Shared.AvailableViews.CreateOrderView);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new(prop));
        }

        private void FilterHandler(object parameter)
        {
            var availableValues = new FilterAvailableValues()
            {
                Products = Orders.SelectMany(x => x.Products.Split(", ")).Distinct().ToList(),
                Statuses = Constants.Statuses.ToList()
            };

            var filterView = new FilterPopupView(availableValues);
            var filter = filterView.ShowDialog(true);
            if (filter != null)
                Filter = filter;
        }
    }
}
