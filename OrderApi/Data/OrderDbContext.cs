using OrderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Data
{
    public class OrderDbContext : DbContext
    {
        internal DbSet<Order> Orders { get; set; } = null!;

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(b => b.Id)
                      .HasDefaultValueSql("NEWSEQUENTIALID()");
            });

            DataSeeder.Seed(modelBuilder);
        }
    }
}
