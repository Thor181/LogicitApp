using LogicitApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace LogicitApp.Data.Models;

public partial class Transport : IDbEntity
{
    public long Id { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? PlateNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
