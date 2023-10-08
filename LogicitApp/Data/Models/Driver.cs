using LogicitApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace LogicitApp.Data.Models;

public partial class Driver : IDbEntity
{
    public long Id { get; set; }

    public string? FullName { get; set; }

    public string? DriverLicenseNumber { get; set; }

    public string? InsurancePolicyNumber { get; set; }

    public string? Tin { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
