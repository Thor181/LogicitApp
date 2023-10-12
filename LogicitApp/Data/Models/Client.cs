using LogicitApp.Data.Models.Interfaces;

namespace LogicitApp.Data.Models;

public partial class Client : IDbEntity
{
    public long Id { get; set; }

    public string? OrganizationName { get; set; }

    public string? Address { get; set; }

    public string? PhoneFax { get; set; }

    public string? CheckingAccount { get; set; }

    public string? Tin { get; set; }

    public string? Bic { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
