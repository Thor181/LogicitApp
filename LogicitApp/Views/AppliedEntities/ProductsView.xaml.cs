﻿using LogicitApp.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void MainDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //((ProductsViewModel)this.DataContext).CellEdit(sender, e);
        }
    }
}
