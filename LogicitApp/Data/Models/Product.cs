using LogicitApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace LogicitApp.Data.Models;

public partial class Product : IDbEntity
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Unit { get; set; }

    public double? Weight { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
