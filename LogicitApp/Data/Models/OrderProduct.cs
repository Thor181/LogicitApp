using LogicitApp.Data.Models.Interfaces;

namespace LogicitApp.Data.Models;

public partial class OrderProduct : IDbEntity
{
    public long Id { get; set; }

    public long? OrderId { get; set; }

    public long? ProductId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
