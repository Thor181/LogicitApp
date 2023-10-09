using LogicitApp.Shared.Extensions;
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
            Shared.Statuses.Completed.AsString(), 
            Shared.Statuses.InProcess.AsString(), 
            Shared.Statuses.AwaitComplete.AsString()
        };
    }
}
