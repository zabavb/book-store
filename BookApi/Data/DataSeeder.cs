using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var category1 = new Category { Id = Guid.NewGuid(), Name = "Фантастика" };
            var category2 = new Category { Id = Guid.NewGuid(), Name = "Детектив" };
            var category3 = new Category { Id = Guid.NewGuid(), Name = "Наукова література" };

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);

            var publisher1 = new Publisher { Id = Guid.NewGuid(), Name = "Видавництво А", Description = "Опис видавництва А" };
            var publisher2 = new Publisher { Id = Guid.NewGuid(), Name = "Видавництво Б", Description = "Опис видавництва Б" };

            modelBuilder.Entity<Publisher>().HasData(publisher1, publisher2);

            var author1 = new Author { Id = Guid.NewGuid(), Name = "Джон Сміт", Biography = "Відомий письменник у жанрі фантастики." };
            var author2 = new Author { Id = Guid.NewGuid(), Name = "Анна Браун", Biography = "Авторка детективних романів." };

            modelBuilder.Entity<Author>().HasData(author1, author2);

            var book1 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Книга 1",
                AuthorId = author1.Id,
                PublisherId = publisher1.Id,
                CategoryId = category1.Id,
                Price = 299.99f,
                Language = Language.UKRAINIAN,
                Year = new DateTime(2023, 1, 1),
                Description = "Опис Книги 1",
                Cover = CoverType.HARDCOVER,
                IsAvaliable = true
            };

            var book2 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Книга 2",
                AuthorId = author2.Id,
                PublisherId = publisher2.Id,
                CategoryId = category2.Id,
                Price = 199.99f,
                Language = Language.ENGLISH,
                Year = new DateTime(2022, 1, 1),
                Description = "Опис Книги 2",
                Cover = CoverType.SOFT_COVER,
                IsAvaliable = false
            };

            modelBuilder.Entity<Book>().HasData(book1, book2);

            var feedback1 = new Feedback
            {
                Id = Guid.NewGuid(),
                ReviewerName = "Іван",
                Comment = "Чудова книга!",
                Rating = 5,
                Date = DateTime.UtcNow,
                IsPurchased = true,
                BookId = book1.Id 
            };

            var feedback2 = new Feedback
            {
                Id = Guid.NewGuid(),
                ReviewerName = "Ольга",
                Comment = "Не дуже сподобалась.",
                Rating = 3,
                Date = DateTime.UtcNow,
                IsPurchased = false,
                BookId = book2.Id  
            };


            modelBuilder.Entity<Feedback>().HasData(feedback1, feedback2);
        }
    }

}
