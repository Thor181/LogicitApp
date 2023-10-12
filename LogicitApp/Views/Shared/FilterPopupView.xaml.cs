using LogicitApp.Data.Models.Applied;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

