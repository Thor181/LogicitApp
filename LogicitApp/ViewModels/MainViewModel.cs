using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Data.Models.Applied;
using LogicitApp.Shared;
using LogicitApp.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class MainViewModel : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableCollection<AppliedOrder> Orders { get; set; } = new();

        #region Commands

        public SimpleCommand CreateCommand { get; set; }
        public SimpleCommand DeleteCommand { get; set; }
        public SimpleCommand EditCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            CreateCommand = new SimpleCommand(CreateHandler);
            DeleteCommand = new SimpleCommand(DeleteHandler);
            EditCommand = new SimpleCommand(EditHandler);

            Orders.CollectionChanged += CollectionChanged;
            LoadOrders();
        }

        public void LoadOrders()
        {
            using var orderLogic = new OrderLogic();
            var orders = orderLogic.GetAll<Order>().ToList();

            Report.Generate(orders[0]);

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

            //var dataGrid = (DataGrid)sender;
            //Type collectionType = dataGrid.ItemsSource.GetType();
            //var itemType = collectionType.GetGenericArguments().Single();

            //if (e.PropertyType == typeof(System.DateTime))
            //    (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy HH:mm";

            //var name = MetadataInfo<IAppliedModel>.GetPropertyLocalizedName(itemType, e.PropertyName);

            //if (MetadataInfo<IAppliedModel>.PropertyIsVirtual(itemType, e.PropertyName) && e.PropertyType != typeof(DateTime))
            //    e.Cancel = true;

            //e.Column.Header = name;
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
    }
}
