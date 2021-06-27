using Microsoft.EntityFrameworkCore;
using Order.Infra.Data.Mapping;

namespace Order.Infra.Data.Context
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderMapping());
            modelBuilder.ApplyConfiguration(new OrderItemMapping());

            base.OnModelCreating(modelBuilder);
        }

        public void ApplyMigrations()
        {
            Database.Migrate();
        }
    }
}
