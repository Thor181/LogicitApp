using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Shared;
using LogicitApp.Shared.Attributes;
using LogicitApp.Shared.Commands;
using LogicitApp.Shared.Extensions;
using LogicitApp.Shared.Results;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LogicitApp.ViewModels
{
    public class CreateOrderViewModel : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public long? ExistedOrderId { get; set; }

        #region Persistent values

        public List<string> Statuses { get; set; } = Constants.Statuses;

        public List<Client> Clients { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<Transport> Transports { get; set; }
        public List<Product> Products { get; set; }

        #endregion

        #region Commands

        public SimpleCommand ExitCommand { get; set; }
        public SimpleCommand AddProductCommand { get; set; }
        public SimpleCommand RemoveProductCommand { get; set; }
        public SimpleCommand SaveCommand { get; set; }

        #endregion

        #region Selected values
        public Product SelectedProduct { get; set; }

        [Validation("Клиент", typeof(Client))]
        public Client SelectedClient { get; set; }

        [Validation("Водитель", typeof(Driver))]
        public Driver SelectedDriver { get; set; }

        [Validation("Транспорт", typeof(Transport))]
        public Transport SelectedTransport { get; set; }

        [Validation("Товары", typeof(ObservableCollection<>))]
        public ObservableCollection<Product> SelectedProducts { get; set; } = new();

        [Validation("Статус", typeof(string))]
        public string SelectedStatus { get; set; }

        public DateTime SelectedCreatedDatetime { get; set; } = DateTime.Now;
        public DateTime SelectedDeliveryDatetime { get; set; } = DateTime.Now;

        [Validation("Адрес доставки", typeof(string))]
        public string SelectedAddress { get; set; }

        public double SelectedSum => SelectedProducts.Sum(x => x.Price) ?? 0d;
        #endregion

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public CreateOrderViewModel()
        {
            SelectedProducts.CollectionChanged += SelectedProducts_CollectionChanged;

            AddProductCommand = new SimpleCommand(x =>
            {

                if (SelectedProduct == null)
                    return;

                SelectedProducts.Add(SelectedProduct);
            });

            RemoveProductCommand = new SimpleCommand(x =>
            {
                var p = SelectedProducts.FirstOrDefault(y => y.Id == (long)x);
                if (p != null)
                    SelectedProducts.Remove(p);
            });

            ExitCommand = new SimpleCommand(x => MainWindow.ChangeView(Views.Shared.AvailableViews.MainView));

            SaveCommand = new SimpleCommand(SaveHandler);

            InitializePersistentValues();

            InitializeExistedOrder();
        }

        private void InitializeExistedOrder()
        {
            var existedOrder = Messenger.GetMessages(nameof(CreateOrderViewModel)).ElementAtOrDefault(0)?.MessageEntity as Order;

            if (existedOrder != null)
            {
                SelectedClient = Clients.FirstOrDefault(x => x.Id == existedOrder.ClientId)!;
                SelectedDriver = Drivers.FirstOrDefault(x => x.Id == existedOrder.DriverId)!;
                SelectedTransport = Transports.FirstOrDefault(x => x.Id == existedOrder.TransportId)!;
                SelectedStatus = existedOrder.Status ?? "";
                SelectedCreatedDatetime = Convert.ToDateTime(existedOrder.CreatedDatetime);
                SelectedDeliveryDatetime = Convert.ToDateTime(existedOrder.DeliveryDatetime);
                SelectedAddress = existedOrder.DeliveryAddress ?? "";

                var products = existedOrder.OrderProducts.Select(x => x.Product).ToList();
                foreach (var item in products)
                {
                    var persistentProduct = Products.FirstOrDefault(x => x.Id == item.Id);
                    if (persistentProduct != null)
                        SelectedProducts.Add(persistentProduct);
                }

                ExistedOrderId = existedOrder.Id;
            }
        }

        private void SelectedProducts_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedSum)));
        }

        private void InitializePersistentValues()
        {
            using var clientLogic = new ClientLogic();
            Clients = new List<Client>(clientLogic.GetAll<Client>());

            using var driverLogic = new DriverLogic();
            Drivers = new List<Driver>(driverLogic.GetAll<Driver>());

            using var transportLogic = new TransportLogic();
            Transports = new List<Transport>(transportLogic.GetAll<Transport>());

            using var productLogic = new ProductLogic();
            Products = new List<Product>(productLogic.GetAll<Product>());
        }

        private void SaveHandler(object? parameter)
        {
            var emptyFields = ValidationAttribute.Validate(this);

            if (emptyFields.Count > 0)
            {
                MessageBox.Show(MainWindow.Instance, $"Следующие поля должны быть заполнены:\n{string.Join('\n', emptyFields)}");
                return;
            }

            using var orderLogic = new OrderLogic();

            BaseResult result;
            Order order;

            if (ExistedOrderId != null)
            {
                order = orderLogic.Get<Order>((long)ExistedOrderId);

                order.ClientId = SelectedClient.Id;
                order.CreatedDatetime = SelectedCreatedDatetime.ToDateTimeString();
                order.DeliveryAddress = SelectedAddress;
                order.DeliveryDatetime = SelectedDeliveryDatetime.ToDateTimeString();
                order.DriverId = SelectedDriver.Id;
                order.Status = SelectedStatus;
                order.Sum = SelectedSum;
                order.TransportId = SelectedTransport.Id;

                order.OrderProducts.Clear();
                var orderProducts = new List<OrderProduct>();
                foreach (var item in SelectedProducts)
                {
                    var orderProduct = new OrderProduct()
                    {
                        OrderId = ExistedOrderId,
                        ProductId = item.Id
                    };

                    order.OrderProducts.Add(orderProduct);
                }

                result = orderLogic.Update<Order>(order);
            }
            else
            {
                order = new Order()
                {
                    ClientId = SelectedClient.Id,
                    CreatedDatetime = SelectedCreatedDatetime.ToDateTimeString(),
                    DeliveryAddress = SelectedAddress,
                    DeliveryDatetime = SelectedDeliveryDatetime.ToDateTimeString(),
                    DriverId = SelectedDriver.Id,
                    Status = SelectedStatus,
                    Sum = SelectedSum,
                    TransportId = SelectedTransport.Id,
                };
                result = orderLogic.AddOrder(order, SelectedProducts);
            }

            MessageBox.Show(result.Message);
            MainWindow.ChangeView(Views.Shared.AvailableViews.MainView);
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new(propName));
        }
    }
}
