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
    public class HomeControllerTests
    {
        private GTAutoDbContext _context = null!;
        private HomeController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Home_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new HomeController(_context)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                }
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

            _context.Cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                ModelId = model.Id,
                Price = 10000,
                Year = 2020,
                Color = "Black",
                Description = "D", FuelType = "F", Transmission = "T",
                IsFlashOffer = true,
                IsSold = false,
                IsReserved = false
            });

            _context.Cars.Add(new Car
            {
                Id = Guid.NewGuid(),
                ModelId = model.Id,
                Price = 20000,
                Year = 2021,
                Color = "White",
                Description = "D", FuelType = "F", Transmission = "T",
                IsFlashOffer = false,
                IsSold = false,
                IsReserved = false
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithFlashOffersOnly()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            
            var model = result.Model as List<Car>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model.First().IsFlashOffer, Is.True);
        }

        [Test]
        public void Privacy_ReturnsView()
        {
            var result = _controller.Privacy() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void About_ReturnsView()
        {
            var result = _controller.About() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Error_ReturnsViewWithErrorViewModel()
        {
            var result = _controller.Error() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.Not.Null);
        }
    }
}
