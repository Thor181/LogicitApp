using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
