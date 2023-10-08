using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared.Results
{
    public class BaseResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
