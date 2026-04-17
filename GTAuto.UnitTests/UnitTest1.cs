using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAutoWeb.Controllers;

namespace GTAuto.UnitTests.Controllers
{
    [TestFixture]
    public class CarsControllerTests
    {
        private GTAutoDbContext _context = null!;
        private CarsController _controller = null!;

        [SetUp]
        public void Setup()
        {
            // 1. Настройка на базата данни в паметта
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);

            // 2. Симулиране на потребител (HttpContext)
            var httpContext = new DefaultHttpContext();
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "test-user") };
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

            // 3. Настройка на TempData
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            // 4. Създаване на контролера
            _controller = new CarsController(_context)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext },
                TempData = tempData
            };

            SeedDatabase();
        }

        // ТОЗИ БЛОК ТРЯБВА ДА МАХНЕ ВЪЛНИЧКИТЕ (NUnit1032)
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
            _context?.Dispose();
        }

        private void SeedDatabase()
        {
            var model = new Model { Id = Guid.NewGuid(), Name = "Audi RS6" };
            _context.Models.Add(model);

            // Добавяме липсващите задължителни данни тук
            var car = new Car
            {
                Id = Guid.NewGuid(),
                ModelId = model.Id,
                Price = 120000,
                Year = 2023,
                Color = "Black",           // Добавено
                Description = "Test desc", // Добавено
                FuelType = "Petrol",       // Добавено (провери дали при теб е стринг или Енумация)
                Transmission = "Automatic",// Добавено
                Images = new List<CarImage>()
            };

            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithCars()
        {
            var result = await _controller.Index(null, null, null, null, null) as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [Test]
        public async Task Details_ReturnsView_WhenCarExists()
        {
            // 1. Взимаме ID-то на колата, която добавихме в SeedDatabase
            var carId = _context.Cars.First().Id;

            // 2. Викаме Details с това ID
            var result = await _controller.Details(carId) as ViewResult;

            // 3. Проверяваме дали ни връща View и дали моделът е правилната кола
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Car;
            Assert.That(model?.Id, Is.EqualTo(carId));
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Create_Get_ReturnsView()
        {
            // Тестваме просто дали страницата за добавяне на кола се отваря
            var result = _controller.Create();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task DeleteConfirmed_RemovesCarFromDatabase()
        {
            // 1. Взимаме съществуваща кола
            var car = _context.Cars.First();
            var carId = car.Id;

            // 2. Викаме метода за изтриване
            var result = await _controller.DeleteConfirmed(carId) as RedirectToActionResult;

            // 3. Проверяваме дали колата вече я няма в базата
            var deletedCar = await _context.Cars.FindAsync(carId);
            Assert.That(deletedCar, Is.Null);
            Assert.That(result?.ActionName, Is.EqualTo("Index"));
        }
    }
}