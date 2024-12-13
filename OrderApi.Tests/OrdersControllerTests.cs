using AutoMapper;
using Library.OrderEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Controllers;
using OrderApi.Data;
using OrderApi.Profiles;
using OrderApi.Services;

namespace OrderApi.Tests
{
    public class OrdersControllerTests
    {
        private bool compareOrders(OrderDto order1, OrderDto order2)
        {
            return (order1.OrderId == order2.OrderId &&
                    order1.UserId == order2.UserId &&
                    order1.Address == order2.Address &&
                    !order1.BookIds.Except(order2.BookIds).ToList().Any() &&
                    order1.City == order2.City &&
                    order1.Region == order2.Region &&
                    order1.Price == order2.Price &&
                    order1.Delivery == order2.Delivery &&
                    order1.DeliveryPrice == order2.DeliveryPrice &&
                    order1.DeliveryTime == order2.DeliveryTime &&
                    order1.DeliveryDate == order2.DeliveryDate &&
                    order1.Status == order2.Status);
        }

        private async Task<OrderDbContext> GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new OrderDbContext(options);
            await context.Database.EnsureCreatedAsync();

            return context;
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            });

            return config.CreateMapper();
        }

        [Fact]
        public async Task GetOrders_ReturnsAll()
        {
            // Arrange
            var context = await GetInMemoryDbContext();
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            // Act
            var result = await controller.GetOrders();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<OrderDto>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<List<OrderDto>>(okObjectResult.Value);
            var orders = Assert.IsAssignableFrom<List<OrderDto>>(value);
            Assert.Equal(3, orders.Count());
        }

        [Fact]
        public async Task GetOrdersById_ReturnsCorrect()
        {
            // Arrange
            var context = await GetInMemoryDbContext();
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var request = (await controller.GetOrders());
            var ok = Assert.IsType<OkObjectResult>(request.Result);
            var requestOrder = Assert.IsType<List<OrderDto>>(ok.Value)[0];

            // Act
            var result = await controller.GetOrderById(requestOrder.OrderId);
             
            // Assert
            var actionResult = Assert.IsType<ActionResult<OrderDto>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<OrderDto>(okObjectResult.Value);
            var order = Assert.IsAssignableFrom<OrderDto>(value);
            Assert.True(compareOrders(order,requestOrder));
        }


    }
}