﻿using LogicitApp.ViewModels;
using System.Windows.Controls;

namespace LogicitApp.Views.AppliedEntities
{
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ((ProductsViewModel)this.DataContext).CellEdit(sender, e);
        }
    }
}
