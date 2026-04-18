using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAutoWeb.Controllers;

namespace GTAuto.UnitTests.Controllers
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private GTAutoDbContext _context = null!;
        private OrdersController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Orders_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new OrdersController(_context);

            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
            _context?.Dispose();
        }

        private void SeedDatabase()
        {
            var user = new User { Id = Guid.NewGuid(), FullName = "Test User" };
            _context.Users.Add(user);

            var car = new Car { Id = Guid.NewGuid(), Color = "Red", Price = 1000, Description = "D", FuelType = "F", Transmission = "T" };
            _context.Cars.Add(car);

            _context.Orders.Add(new Order 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                UserId = user.Id,
                CarId = car.Id,
                DepositAmount = 50,
                CreatedOn = DateTime.UtcNow
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithOrders()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Order>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithOrder_WhenIdIsValid()
        {
            var result = await _controller.Details(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Order;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(Guid.Parse("11111111-1111-1111-1111-111111111111")));
        }

        [Test]
        public void Create_Get_ReturnsView()
        {
            var result = _controller.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ViewData["CarId"], Is.Not.Null);
            Assert.That(_controller.ViewData["UserId"], Is.Not.Null);
        }

        [Test]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var order = new Order 
            { 
                UserId = _context.Users.First().Id,
                CarId = _context.Cars.First().Id,
                DepositAmount = 100,
                CreatedOn = DateTime.UtcNow
            };
            var result = await _controller.Create(order) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Orders.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("DepositAmount", "Required");
            var order = new Order();
            var result = await _controller.Create(order) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(order));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithOrder_WhenIdIsValid()
        {
            var result = await _controller.Edit(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Order;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(Guid.Parse("11111111-1111-1111-1111-111111111111")));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var order = new Order { Id = Guid.NewGuid() };
            var result = await _controller.Edit(Guid.NewGuid(), order);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var orderId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var order = new Order 
            { 
                Id = orderId, 
                UserId = _context.Users.First().Id,
                CarId = _context.Cars.First().Id,
                DepositAmount = 150
            };
            
            _context.ChangeTracker.Clear();

            var result = await _controller.Edit(orderId, order) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedOrder = await _context.Orders.FindAsync(orderId);
            Assert.That(updatedOrder?.DepositAmount, Is.EqualTo(150));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var orderId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var order = new Order { Id = orderId };
            _controller.ModelState.AddModelError("DepositAmount", "Required");

            var result = await _controller.Edit(orderId, order) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(order));
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithOrder_WhenIdIsValid()
        {
            var result = await _controller.Delete(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Order;
            Assert.That(model, Is.Not.Null);
        }

        [Test]
        public async Task DeleteConfirmed_DeletesOrder_AndRedirectsToIndex()
        {
            var orderId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.DeleteConfirmed(orderId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var deletedOrder = await _context.Orders.FindAsync(orderId);
            Assert.That(deletedOrder, Is.Null);
            Assert.That(_context.Orders.Count(), Is.EqualTo(0));
        }
    }
}
