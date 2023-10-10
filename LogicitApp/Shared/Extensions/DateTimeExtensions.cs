using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateTimeString(this DateTime d)
        {
            return $"{d:G}";
        }
    }
}
