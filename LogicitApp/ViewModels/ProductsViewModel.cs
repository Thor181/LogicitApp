using LogicitApp.Data.DataLogic;
using LogicitApp.Data.Models;
using LogicitApp.Shared.Commands;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableCollection<Product> Products { get; set; } = new();

        public SimpleCommand AddCommand { get; set; }
        public SimpleCommand RemoveCommand { get; set; }

        public ProductsViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            AddCommand = new SimpleCommand(AddHandler);
            RemoveCommand = new SimpleCommand(RemoveHandler);

            Products.CollectionChanged += CollectionChanged;

            LoadProducts();
        }

        private void LoadProducts()
        {
            using var productLogic = new ProductLogic();

            var products = productLogic.GetAll<Product>().ToList();

            Products.Clear();
            foreach (var item in products)
                Products.Add(item);
        }

        public void AddHandler(object? parameter)
        {

            var JUST_BREAKPOINT = string.Empty;
        }

        public void RemoveHandler(object? parameter)
        {
            var idParsed = long.TryParse(parameter?.ToString(), out long id);

            if (!idParsed)
            {
                MessageBox.Show("ID товара не распознан");
                return;
            }

            using var productLogic = new ProductLogic();
            if (productLogic.IsUsed(id))
            {
                MessageBox.Show("Товар не может быть удален, так как используется в заказах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var confirmation = MessageBox.Show("Действительно удалить данный товар?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.No)
                return;

            var result = productLogic.Remove<Product>(id);
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
                return;
            }

            LoadProducts();
        }

        public void CellEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            var a = (sender as DataGrid)?.Items;

            var updatedEntity = e.Row.Item as Product;

            if (updatedEntity == null)
                return;

            using var productLogic = new ProductLogic();

            var result = productLogic.Update(updatedEntity);

            var JUST_BREAKPOINT = string.Empty;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
