using Microsoft.EntityFrameworkCore;
using NonNullableLib;

namespace Cosmos
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        #region Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                    "https://<host>.documents.azure.com:443/",
                    "",
                    databaseName: "Points");
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DefaultContainer
            modelBuilder.HasDefaultContainer("Orders");
            #endregion

            #region Container
            modelBuilder.Entity<Order>()
                .ToContainer("Orders");
            #endregion

            #region PartitionKey
            //modelBuilder.Entity<Order>()
            //    .HasPartitionKey(o => o.PartitionKey);
            #endregion

            #region PropertyNames
            modelBuilder.Entity<Order>().OwnsOne(
                o => o.ShippingAddress,
                sa =>
                {
                    sa.ToJsonProperty("Address");
                    sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
                    sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
                });
            #endregion
        }
    }
}
