using AutoMapper;
using Library.OrderEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Controllers;
using OrderApi.Data;
using OrderApi.Profiles;
using OrderApi.Services;
using System.ComponentModel.DataAnnotations;

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

        private void AddValidationErrorsFromDto<T>(ControllerBase controller, T dto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(dto);

            foreach (var property in typeof(T).GetProperties())
            {
                bool isValid = Validator.TryValidateProperty(
                    property.GetValue(dto),
                    new ValidationContext(dto) { MemberName = property.Name },
                    validationResults
                );

                if (validationResults.Any())
                {
                    foreach (var validationResult in validationResults)
                    {
                        foreach (var memberName in validationResult.MemberNames)
                        {
                            controller.ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                        }
                    }

                    validationResults.Clear();
                }
            }
        }

        private async Task<OrderDbContext> GetInMemoryDbContext(string dbName = null)
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
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
            var context = await GetInMemoryDbContext("TestDb_GetOrders");
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
            var context = await GetInMemoryDbContext("TesbDb_GetOrderById");
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

        [Fact]
        public async Task CreateOrder_Success()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_CreateOrder");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Lviv",
                City = "Lviv",
                Address = "Address10",
                Price = (float)100.59,
                Delivery = DeliveryType.NOVA_POST,
                DeliveryPrice = (float)125.99,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(2),
                Status = OrderStatus.TRANSIT,
            };

            // Act
            var result = await controller.CreateOrder(newOrder);

            // Assert
            var actionResult = Assert.IsType<ActionResult<OrderDto>>(result);
            var createdAtObjectResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var value = Assert.IsType<OrderDto>(createdAtObjectResult.Value);
            var order = Assert.IsAssignableFrom<OrderDto>(value);
            Assert.True(compareOrders(order, newOrder));
        }

        [Fact]
        public async Task CreateOrder_TestForFalsePositives()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_CreateOrder_FalsePositives");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var addedOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Lviv",
                City = "Lviv",
                Address = "Address10",
                Price = (float)100.59,
                Delivery = DeliveryType.NOVA_POST,
                DeliveryPrice = (float)125.99,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(2),
                Status = OrderStatus.TRANSIT,
            };

            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Kyiv",
                City = "Kyiv",
                Address = "Address12",
                Price = (float)124.59,
                Delivery = DeliveryType.UKR_POST,
                DeliveryPrice = (float)125.99,
                DeliveryDate = DateTime.Now.AddDays(-1),
                DeliveryTime = DateTime.Now.AddDays(1),
                Status = OrderStatus.PROCESSING,
            };

            // Act
            var result = await controller.CreateOrder(addedOrder);

            // Assert
            var actionResult = Assert.IsType<ActionResult<OrderDto>>(result);
            var createdAtObjectResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var value = Assert.IsType<OrderDto>(createdAtObjectResult.Value);
            var order = Assert.IsAssignableFrom<OrderDto>(value);
            Assert.False(compareOrders(order, newOrder));
        }

        [Fact]
        public async Task CreateOrder_PassedInvalidDataEmptyClass()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_CreateOrder_InvalidData");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var newOrder = new Order();

            //Adding validations
            AddValidationErrorsFromDto(controller, newOrder);


            // Act
            var result = await controller.CreateOrder(newOrder);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateOrder_PassedInvalidDataNull()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_CreateOrder_InvalidData");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            // Act
            var result = await controller.CreateOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateOrder_Success()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_UpdateOrder");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var request = (await controller.GetOrders());
            var ok = Assert.IsType<OkObjectResult>(request.Result);
            var updatedOrderId = Assert.IsType<List<OrderDto>>(ok.Value)[0].OrderId;

            var newOrder = new Order
            {
                OrderId = updatedOrderId,
                UserId = Guid.NewGuid(),
                BookIds = { Guid.NewGuid() },
                Region = "Kyiv",
                City = "Kyiv",
                Address = "Address12",
                Price = (float)124.59,
                Delivery = DeliveryType.UKR_POST,
                DeliveryPrice = (float)125.99,
                DeliveryDate = DateTime.Now,
                DeliveryTime = DateTime.Now.AddDays(2),
                Status = OrderStatus.PROCESSING,
            };

            // Act
            var result = await controller.UpdateOrder(updatedOrderId,newOrder);

            // Assert
            var updateResult = await controller.GetOrderById(updatedOrderId);
            var actionResult = Assert.IsType<ActionResult<OrderDto>>(updateResult);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<OrderDto>(okObjectResult.Value);
            var order = Assert.IsAssignableFrom<OrderDto>(value);
            Assert.True(compareOrders(order, newOrder));
        }

        [Fact]
        public async Task UpdateOrder_PassedInvalidDataEmptyClass()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_UpdateOrder_InvalidData");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var request = (await controller.GetOrders());
            var ok = Assert.IsType<OkObjectResult>(request.Result);
            var updatedOrderId = Assert.IsType<List<OrderDto>>(ok.Value)[0].OrderId;

            var newOrder = new Order();

            //Adding validations
            AddValidationErrorsFromDto(controller, newOrder);

            // Act
            var result = await controller.UpdateOrder(updatedOrderId,newOrder);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateOrder_PassedInvalidDataNull()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_UpdateOrder_InvalidData");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var request = (await controller.GetOrders());
            var ok = Assert.IsType<OkObjectResult>(request.Result);
            var updatedOrderId = Assert.IsType<List<OrderDto>>(ok.Value)[0].OrderId;

            // Act
            var result = await controller.UpdateOrder(updatedOrderId, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteOrder_Success()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_DeleteOrder");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            var request = (await controller.GetOrders());
            var ok = Assert.IsType<OkObjectResult>(request.Result);
            var deletedOrderId = Assert.IsType<List<OrderDto>>(ok.Value)[0].OrderId;



            // Act
            var result = await controller.DeleteOrder(deletedOrderId);
            var fetchResult = await controller.GetOrderById(deletedOrderId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundObjectResult>(fetchResult.Result);

        }

        [Fact]
        public async Task DeleteOrder_NonExistingObjectId()
        {
            // Arrange
            var context = await GetInMemoryDbContext("TestDb_DeleteOrder");
            var mapper = GetMapper();
            var orderService = new OrderService(context, mapper);
            var controller = new OrdersController(orderService);

            // Act
            var result = await controller.DeleteOrder(new Guid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}