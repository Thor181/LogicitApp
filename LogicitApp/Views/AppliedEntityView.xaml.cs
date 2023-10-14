using LogicitApp.ViewModels;
using System.Windows.Controls;

namespace LogicitApp.Views
{
    public partial class AppliedEntityView : UserControl
    {
        public AppliedEntityView()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            ((AppliedEntityViewModel)this.DataContext).GeneratingColumn(sender, e);
        }
    }
}
