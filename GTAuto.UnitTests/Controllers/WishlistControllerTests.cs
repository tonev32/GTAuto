using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAutoWeb.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace GTAuto.UnitTests.Controllers
{
    [TestFixture]
    public class WishlistControllerTests
    {
        private GTAutoDbContext _context = null!;
        private WishlistController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Wishlist_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);

            var httpContext = new DefaultHttpContext();
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "test-user-id") };
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

            
            httpContext.Request.Headers["Referer"] = "http://test.com/referer";

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            _controller = new WishlistController(_context)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext },
                TempData = tempData
            };

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
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Audi" };
            var model = new Model { Id = Guid.NewGuid(), Name = "A6", BrandId = brand.Id };
            
            _context.Brands.Add(brand);
            _context.Models.Add(model);

            var car1 = new Car 
            { 
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                ModelId = model.Id,
                Price = 1000, 
                Color = "C", Description = "D", FuelType = "F", Transmission = "T",
                IsReserved = false 
            };
            
            var car2 = new Car 
            { 
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), 
                ModelId = model.Id,
                Price = 2000, 
                Color = "C", Description = "D", FuelType = "F", Transmission = "T",
                IsReserved = true, 
                ReservedByUserId = "test-user-id" 
            };

            var car3 = new Car 
            { 
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), 
                ModelId = model.Id,
                Price = 3000, 
                Color = "C", Description = "D", FuelType = "F", Transmission = "T",
                IsReserved = false 
            };

            _context.Cars.AddRange(car1, car2, car3);

            _context.WishlistCars.Add(new WishlistCar
            {
                Id = Guid.NewGuid(),
                UserId = "test-user-id",
                CarId = car1.Id
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithWishlistCars()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            
            var model = result.Model as List<WishlistCar>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model.First().CarId, Is.EqualTo(Guid.Parse("11111111-1111-1111-1111-111111111111")));
        }

        [Test]
        public async Task MyOrders_ReturnsViewWithReservedCars()
        {
            var result = await _controller.MyOrders() as ViewResult;
            Assert.That(result, Is.Not.Null);
            
            var model = result.Model as List<Car>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model.First().Id, Is.EqualTo(Guid.Parse("22222222-2222-2222-2222-222222222222")));
        }

        [Test]
        public async Task Buy_ReservesCar_AndRedirectsToMyOrders()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Buy(carId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("MyOrders"));
            Assert.That(_controller.TempData["SuccessMessage"], Is.Not.Null);

            var car = await _context.Cars.FindAsync(carId);
            Assert.That(car?.IsReserved, Is.True);
            Assert.That(car?.ReservedByUserId, Is.EqualTo("test-user-id"));
        }

        [Test]
        public async Task Add_AddsCarToWishlist_AndRedirectsToReferer()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Add(carId) as RedirectResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("http://test.com/referer"));

            var exists = await _context.WishlistCars.AnyAsync(w => w.CarId == carId && w.UserId == "test-user-id");
            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task Remove_RemovesCarFromWishlist_AndRedirectsToIndex()
        {
            var carId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.Remove(carId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));

            var exists = await _context.WishlistCars.AnyAsync(w => w.CarId == carId && w.UserId == "test-user-id");
            Assert.That(exists, Is.False);
        }
    }
}
