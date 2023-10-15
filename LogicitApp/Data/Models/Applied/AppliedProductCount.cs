using System.ComponentModel;

namespace LogicitApp.Data.Models.Applied
{
    public class AppliedProductCount : INotifyPropertyChanged
    {
        public Product Product { get; set; }

        private int count;
        public int Count { get => count; set { count = value; PropertyChanged?.Invoke(this, new(nameof(Count))); } }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
