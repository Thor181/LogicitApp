using HandyControl.Properties.Langs;
using HandyControl.Tools;
using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Shared;
using LogicitApp.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.ViewModels
{
    public class CreateOrderViewModel : INotifyCollectionChanged, INotifyPropertyChanged
    {
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

        #endregion

        #region Selected values
        public Product SelectedProduct { get; set; }

        public Client SelectedClient { get; set; }
        public Driver SelectedDriver { get; set; }
        public Transport SelectedTransport { get; set; }
        public ObservableCollection<Product> SelectedProducts { get; set; } = new();
        public string SelectedStatus { get; set; }
        public DateTime SelectedCreatedDatetime { get; set; } = DateTime.Now;
        public DateTime SelectedDeliveryDatetime { get; set; } = DateTime.Now;
        public string SelectedAddress { get; set; }
        public double SelectedSum => SelectedProducts.Sum(x => x.Price) ?? 0d;
        #endregion

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public CreateOrderViewModel()
        {
            SelectedProducts.CollectionChanged += SelectedProducts_CollectionChanged;

            AddProductCommand = new SimpleCommand(x => {

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

            InitializePersistentValues();
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
    }
}
