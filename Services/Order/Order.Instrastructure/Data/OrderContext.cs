using Microsoft.EntityFrameworkCore;

namespace Order.Instrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Domain.Entities.Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Order>().Property(r => r.CargoPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Domain.Entities.Order>().Property(r => r.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Domain.Entities.Order>().Property(r => r.TotalDiscountCargoPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Domain.Entities.Order>().Property(r => r.TotalDiscountPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Domain.Entities.OrderItem>().Property(r => r.Price).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

    }
}
