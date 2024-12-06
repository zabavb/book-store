using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data
{
    public class BookDbContext : DbContext
    {
        internal DbSet<Book> Books { get; set; } = null!;
        internal DbSet<Category> Categories { get; set; } = null!;
        internal DbSet<Publisher> Publishers { get; set; } = null!;
        internal DbSet<Feedback> Feedbacks { get; set; } = null!;
        internal DbSet<Author> Authors { get; set; } = null!; 


        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Id)
                      .HasDefaultValueSql("NEWSEQUENTIALID()");
                 });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Id)
                      .HasDefaultValueSql("NEWID()");
                
                
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(p => p.Id)
                      .HasDefaultValueSql("NEWID()");
                
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(f => f.Id)
                      .HasDefaultValueSql("NEWID()");
               
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(a => a.Id)
                      .HasDefaultValueSql("NEWID()");
            });

            DataSeeder.Seed(modelBuilder);


        }


    }
}
