using LogicitApp.Data.Models.Interfaces;

namespace LogicitApp.Data.Models;

public partial class Order : IDbEntity
{
    public long Id { get; set; }

    public long? ClientId { get; set; }

    public string? Status { get; set; }

    public string? CreatedDatetime { get; set; }

    public string? DeliveryDatetime { get; set; }

    public string? DeliveryAddress { get; set; }

    public long? DriverId { get; set; }

    public long? TransportId { get; set; }

    public double? Sum { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Transport? Transport { get; set; }
}
