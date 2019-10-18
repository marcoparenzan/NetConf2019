using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NonNullableLib;

namespace EFGetStarted
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(b =>
            {
                b.Property<int>(nameof(Order.Id))
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>(nameof(Order.TrackingNumber))
                    .HasColumnType("nvarchar(32)");

                b.HasKey(nameof(Order.Id));

                b.OwnsOne(nameof(Order.ShippingAddress), nameof(Order.ShippingAddress), bb =>
                {
                    bb.Property<string>(nameof(StreetAddress.City))
                        .HasColumnType("nvarchar(32)").HasColumnName("City");
                    bb.Property<string>(nameof(StreetAddress.Street))
                        .HasColumnType("nvarchar(32)").HasColumnName("Street");
                });

                b.ToTable("Orders");
            });

            modelBuilder.Entity<Order>().OwnsOne(p => p.ShippingAddress);
        }

    }
}
