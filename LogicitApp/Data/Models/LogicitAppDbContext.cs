using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LogicitApp.Data.Models;

public partial class LogicitAppDbContext : DbContext
{
    public LogicitAppDbContext()
    {
    }

    public LogicitAppDbContext(DbContextOptions<LogicitAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies().UseSqlite("Filename=Data/LogicitAppDb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Bic).HasColumnName("BIC");
            entity.Property(e => e.Tin).HasColumnName("TIN");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("Driver");

            entity.Property(e => e.Tin).HasColumnName("TIN");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders).HasForeignKey(d => d.DriverId);

            entity.HasOne(d => d.Transport).WithMany(p => p.Orders).HasForeignKey(d => d.TransportId);
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.ToTable("OrderProduct");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.ToTable("Transport");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
