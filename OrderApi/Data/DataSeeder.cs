using Library.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Order = OrderApi.Models.Order;

namespace OrderApi.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder) 
        {
            var order1 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Lviv",
                City = "Lviv",
                Address = "Address1",
                Price = (float)100.59,
                Delivery = DeliveryType.NOVA_POST,
                DeliveryPrice = (float)120.59,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(2),
            };
            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Lviv",
                City = "Lviv",
                Address = "Address2",
                Price = (float)100.59,
                Delivery = DeliveryType.UKR_POST,
                DeliveryPrice = (float)120.59,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(7),
            };

            var order3 = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Lviv",
                City = "Lviv",
                Address = "Address3",
                Price = (float)100.59,
                Delivery = DeliveryType.LIBRO,
                DeliveryPrice = (float)120.59,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(4),
            };

            modelBuilder.Entity<Order>().HasData(order1, order2, order3);
        }
    }
}
