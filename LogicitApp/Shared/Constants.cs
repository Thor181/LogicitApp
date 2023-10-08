using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared
{
    public class Constants
    {
        public static List<string> Statuses { get; set; } = new List<string>()
        {
            "Выполнен", "В процессе", "Ожидание выполнения"
        };
    }
}
