using LogicitApp.ViewModels;
using System.Windows.Controls;

namespace LogicitApp.Views.AppliedEntities
{
    public partial class TransportView : UserControl
    {
        public TransportView()
        {
            InitializeComponent();
        }

        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ((TransportViewModel)this.DataContext).CellEdit(sender, e);
        }
    }
}
