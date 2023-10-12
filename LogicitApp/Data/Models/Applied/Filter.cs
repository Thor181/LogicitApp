namespace LogicitApp.Data.Models.Applied
{
    public class Filter
    {
        public List<string> Products { get; set; } = new();

        public string Clients { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDateTimeFrom { get; set; }
        public DateTime CreatedDateTimeTo { get; set; }

        public DateTime DeliveryDateTimeFrom { get; set; }
        public DateTime DeliveryDateTimeTo { get; set; }

        public string Address { get; set; }

        public string Driver { get; set; }

        public string Transport { get; set; }

        public double SumFrom { get; set; }
        public double SumTo { get; set; }
    }
}
