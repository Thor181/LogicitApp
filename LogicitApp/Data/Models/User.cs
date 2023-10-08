using LogicitApp.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace LogicitApp.Data.Models;

public partial class User : IDbEntity
{
    public long Id { get; set; }

    public string? Login { get; set; }

    public string? PasswordHash { get; set; }
}
