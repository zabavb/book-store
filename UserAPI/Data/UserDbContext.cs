using Library.UserEntities;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data.Configurations;

namespace UserAPI.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; private set; } = null!;
        public DbSet<Password> Passwords { get; private set; } = null!;
        public DbSet<Subscription> Subscriptions { get; private set; } = null!;

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        }
    }
}
