using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicitApp.Data.Models.Applied
{
    public class AppliedOrder
    {
        public long Id { get; set; }

        public string Products { get; set; }

        public string Client { get; set; }

        public string Status { get; set; }

        public string CreatedDatetime { get; set; }

        public string DeliveryDatetime { get; set; }

        public string DeliveryAddress { get; set; }

        public string Driver { get; set; }

        public string Transport { get; set; }

        public double Sum { get; set; }

        //public List<OrderProduct> OrderProducts { get; set; }
    }
}
