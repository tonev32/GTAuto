using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAutoWeb.Controllers;
using GTAutoWeb.ViewModel;

namespace GTAutoWeb.Tests.Controllers
{
    [TestFixture]
    public class CarsControllerTests
    {
        private GTAutoDbContext _context;
        private CarsController _controller;

        [SetUp]
        public void Setup()
        {
            // 1. In-Memory База данни
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);

            // 2. Симулация на HttpContext и логнат потребител
            var httpContext = new DefaultHttpContext();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
                new Claim(ClaimTypes.Name, "test@gt-auto.com")
            };
            var identity = new ClaimsIdentity(claims, "mock");
            httpContext.User = new ClaimsPrincipal(identity);

            // 3. Симулация на TempData
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            // 4. Инициализация на контролера (ВЕЧЕ ИСКА САМО ЕДИН АРГУМЕНТ!)
            _controller = new CarsController(_context)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext },
                TempData = tempData
            };

            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _controller.Dispose(); // Оправяме предупреждението за TearDown
        }

        private void SeedDatabase()
        {
            var model = new Model { Id = Guid.NewGuid(), Name = "Audi RS6" };
            _context.Models.Add(model);

            // Свободна кола
            var availableCar = new Car
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ModelId = model.Id,
                Model = model,
                Price = 120000,
                Year = 2023,
                HorsePower = 600,
                IsReserved = false,
                IsSold = false,
                Images = new List<CarImage>()
            };
            _context.Cars.Add(availableCar);

            // Резервирана кола
            var reservedCar = new Car
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ModelId = model.Id,
                Model = model,
                Price = 85000,
                IsReserved = true,
                ReservedByUserId = "other-user-456",
                Images = new List<CarImage>()
            };
            _context.Cars.Add(reservedCar);

            // Резервация за резервираната кола
            _context.Reservations.Add(new Reservation
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                UserId = "other-user-456",
                CarId = reservedCar.Id,
                DepositPaid = 4250,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            });

            _context.SaveChanges();
        }

        // ==========================================
        // 1. ТЕСТОВЕ ЗА INDEX
        // ==========================================
        [Test]
        public async Task Index_ReturnsViewWithFilteredCars_ByPrice()
        {
            var result = await _controller.Index(null, 100000, null, null, null) as ViewResult;
            Assert.That(result, Is.Not.Null);

            var cars = result.Model as List<Car>;
            Assert.That(cars.Count, Is.EqualTo(1));
        }

        // ==========================================
        // 2. ТЕСТОВЕ ЗА DETAILS
        // ==========================================
        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsView_WhenCarExists()
        {
            var carId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.Details(carId) as ViewResult;
            Assert.That(result, Is.Not.Null);

            var model = result.Model as Car;
            Assert.That(model.Id, Is.EqualTo(carId));
        }

        // ==========================================
        // 3. ТЕСТОВЕ ЗА CREATE & EDIT
        // ==========================================
        [Test]
        public async Task CreatePost_ValidModel_RedirectsToIndexAndSaves()
        {
            var newCar = new CarViewModel
            {
                ModelId = _context.Models.First().Id,
                Price = 50000,
                Year = 2024
            };

            var result = await _controller.Create(newCar) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Cars.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task EditPost_ValidModel_UpdatesCarAndRedirects()
        {
            var carId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var editVm = new CarViewModel
            {
                Id = carId,
                ModelId = _context.Models.First().Id,
                Price = 99999, // Сменяме цената
                Year = 2023
            };

            var result = await _controller.Edit(carId, editVm) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));

            var updatedCar = await _context.Cars.FindAsync(carId);
            Assert.That(updatedCar.Price, Is.EqualTo(99999));
        }

        // ==========================================
        // 4. ТЕСТОВЕ ЗА DELETE
        // ==========================================
        [Test]
        public async Task DeleteConfirmed_RemovesCar_RedirectsToIndex()
        {
            var carId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var result = await _controller.DeleteConfirmed(carId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Cars.Count(), Is.EqualTo(1));
        }

        // ==========================================
        // 5. ТЕСТОВЕ ЗА РЕЗЕРВАЦИИ (CHECKOUT & CANCEL)
        // ==========================================
        [Test]
        public async Task CheckoutConfirm_AvailableCar_CreatesReservationAndRedirects()
        {
            var carId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            var result = await _controller.CheckoutConfirm(carId, "1234") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Confirmation"));

            var car = await _context.Cars.FindAsync(carId);
            Assert.That(car.IsReserved, Is.True);
            Assert.That(car.ReservedByUserId, Is.EqualTo("test-user-id"));
        }

        [Test]
        public async Task CheckoutConfirm_AlreadyReservedCar_RedirectsToDetailsWithError()
        {
            var reservedCarId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var result = await _controller.CheckoutConfirm(reservedCarId, "1234") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(_controller.TempData.ContainsKey("ErrorMessage"), Is.True);
        }

        [Test]
        public async Task CancelReservation_ValidId_FreesCarAndRemovesReservation()
        {
            var reservationId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var reservedCarId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var result = await _controller.CancelReservation(reservationId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("ReservedCars"));

            var freedCar = await _context.Cars.FindAsync(reservedCarId);
            Assert.That(freedCar.IsReserved, Is.False);
            Assert.That(freedCar.ReservedByUserId, Is.Null);
            Assert.That(_context.Reservations.Count(), Is.EqualTo(0));
        }
    }
}