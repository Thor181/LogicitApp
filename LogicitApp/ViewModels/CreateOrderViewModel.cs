using HandyControl.Properties.Langs;
using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Shared;
using LogicitApp.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<string> Statuses { get; set; } = Constants.Statuses;
        public List<Client> Clients { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<Transport> Transports { get; set; }
        
        public SimpleCommand ExitCommand { get; set; }

        public CreateOrderViewModel()
        {
            ExitCommand = new SimpleCommand((x) => { MainWindow.ChangeView(Views.Shared.AvailableViews.MainView); });
            InitializePersistentValues();
        }

        private void InitializePersistentValues()
        {
            using var clientLogic = new ClientLogic(); 
            Clients = new List<Client>(clientLogic.GetAll<Client>());

            using var driverLogic = new DriverLogic();
            Drivers = new List<Driver>(driverLogic.GetAll<Driver>());

            using var transportLogic = new TransportLogic();
            Transports = new List<Transport>(transportLogic.GetAll<Transport>());
        }
    }
}
