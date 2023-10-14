using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class AppliedEntityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    


        public void GeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}