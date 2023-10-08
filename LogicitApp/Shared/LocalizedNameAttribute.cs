using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Shared
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class LocalizedNameAttribute : Attribute
    {
        public string LocalizedName { get; set; }

        public LocalizedNameAttribute(string localizedName)
        {
            LocalizedName = localizedName;
        }
    }
}
