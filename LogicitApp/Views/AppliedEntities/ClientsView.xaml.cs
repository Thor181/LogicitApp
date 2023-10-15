using LogicitApp.ViewModels;
using System.Windows.Controls;

namespace LogicitApp.Views.AppliedEntities
{
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
        }

        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ((ClientsViewModel)this.DataContext).CellEdit(sender, e);
        }
    }
}
