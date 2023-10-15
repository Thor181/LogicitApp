using LogicitApp.Data.Models.Applied;
using System.Windows;

namespace LogicitApp.Views.Shared
{

    public partial class FilterPopupView : Window
    {
        public Filter Filter { get; set; } = new();
        public FilterAvailableValues AvailableValues { get; set; }

        public FilterPopupView(FilterAvailableValues availableValues)
        {
            AvailableValues = availableValues;

            InitializeComponent();
        }

        public Filter? ShowDialog(bool filtering)
        {
            var result = this.ShowDialog();

            if (result == true)
                return Filter;

            return null;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            foreach (var product in this.ProductsCheckCombobox.SelectedItems)
                Filter.Products.Add(product as string);

            Close();
        }
    }
}

