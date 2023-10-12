using LogicitApp.Data.Models.Applied;
using System.Globalization;
using System.Windows.Data;

namespace LogicitApp.Shared.Converters
{
    [ValueConversion(typeof(Filter), typeof(bool))]
    public class FilterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var filter = (Filter)values[0];
            IEnumerable<AppliedOrder> ao = new List<AppliedOrder>() { (AppliedOrder)values[1] };

            if (filter.Products.Count > 0)
            {
                foreach (var item in filter.Products)
                    ao = ao.Where(x => x.Products.Contains(item, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.Clients))
                ao = ao.Where(x => x.Client.Contains(filter.Clients, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.Status))
                ao = ao.Where(x => x.Status == filter.Status);

            if (filter.CreatedDateTimeFrom > DateTime.MinValue)
                ao = ao.Where(x => DateTime.Parse(x.CreatedDatetime) >= filter.CreatedDateTimeFrom);

            if (filter.CreatedDateTimeTo > DateTime.MinValue)
                ao = ao.Where(x => DateTime.Parse(x.CreatedDatetime) >= filter.CreatedDateTimeTo);

            if (filter.DeliveryDateTimeFrom > DateTime.MinValue)
                ao = ao.Where(x => DateTime.Parse(x.DeliveryDatetime) >= filter.DeliveryDateTimeFrom);

            if (filter.DeliveryDateTimeTo > DateTime.MinValue)
                ao = ao.Where(x => DateTime.Parse(x.DeliveryDatetime) >= filter.DeliveryDateTimeTo);

            if (!string.IsNullOrEmpty(filter.Address))
                ao = ao.Where(x => x.DeliveryAddress.Contains(filter.Address, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.Driver))
                ao = ao.Where(x => x.Driver.Contains(filter.Driver, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(filter.Transport))
                ao = ao.Where(x => x.Transport.Contains(filter.Transport, StringComparison.InvariantCultureIgnoreCase));

            if (filter.SumFrom > 0)
                ao = ao.Where(x => x.Sum >= filter.SumFrom);

            if (filter.SumTo > 0)
                ao = ao.Where(x => x.Sum <= filter.SumTo);

            return ao.Any();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
