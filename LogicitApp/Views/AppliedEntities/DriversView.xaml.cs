using LogicitApp.ViewModels;
using System.Windows.Controls;

namespace LogicitApp.Views.AppliedEntities
{
    public partial class DriversView : UserControl
    {
        public DriversView()
        {
            InitializeComponent();
        }

        private void MainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ((DriversViewModel)this.DataContext).CellEdit(sender, e);
        }
    }
}
