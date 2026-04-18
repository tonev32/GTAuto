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
using System.Linq;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAutoWeb.Controllers;
using GTAutoWeb.ViewModel;

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
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Cars_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);

            var httpContext = new DefaultHttpContext();
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "test-user-id") };
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

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
            _controller?.Dispose();
            _context?.Dispose();
        }

        private void SeedDatabase()
        {
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Audi" };
            _context.Brands.Add(brand);

            var model = new Model { Id = Guid.NewGuid(), Name = "Audi RS6", BrandId = brand.Id };
            _context.Models.Add(model);

            var car1 = new Car
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ModelId = model.Id,
                Price = 120000,
                Year = 2023,
                Color = "Black",
                Description = "Test desc",
                FuelType = "Petrol",
                Transmission = "Automatic",
                IsReserved = false,
                IsSold = false,
                Images = new List<CarImage>()
            };

            var car2 = new Car
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ModelId = model.Id,
                Price = 150000,
                Year = 2024,
                Color = "White",
                Description = "Reserved car",
                FuelType = "Diesel",
                Transmission = "Manual",
                IsReserved = true,
                ReservedByUserId = "test-user-id",
                IsSold = false,
                Images = new List<CarImage>()
            };

            _context.Cars.Add(car1);
            _context.Cars.Add(car2);

            _context.WishlistCars.Add(new WishlistCar { Id = Guid.NewGuid(), CarId = car1.Id, UserId = "test-user-id" });

            _context.Reservations.Add(new Reservation
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                UserId = "test-user-id",
                CarId = car2.Id,
                DepositPaid = 1000,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithCars()
        {
            var result = await _controller.Index(null, null, null, null, null) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Car>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(2));
            Assert.That(_controller.ViewBag.FavoriteCars as List<Guid>, Contains.Item(Guid.Parse("33333333-3333-3333-3333-333333333333")));
        }

        [Test]
        public async Task Index_WithCyrillicSearch_ReturnsEmptyAndErrorMessage()
        {
            var result = await _controller.Index("кола", null, null, null, null) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Car>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(0));
            Assert.That(_controller.ViewData["ErrorMessage"], Is.EqualTo("Invalid characters detected. Please use English only."));
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithCar_WhenIdIsValid()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Details(carId) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as Car;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(carId));
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
            var result = _controller.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var vm = new CarViewModel
            {
                ModelId = _context.Models.First().Id,
                Year = 2022,
                Price = 50000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Color = "Blue",
                Description = "New Car"
            };

            var result = await _controller.Create(vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Cars.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Price", "Required");
            var vm = new CarViewModel();
            
            var result = await _controller.Create(vm) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(vm));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenCarDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithCarViewModel_WhenIdIsValid()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Edit(carId) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as CarViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(carId));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var vm = new CarViewModel { Id = Guid.NewGuid() };
            var result = await _controller.Edit(Guid.NewGuid(), vm);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var vm = new CarViewModel
            {
                Id = carId,
                ModelId = _context.Models.First().Id,
                Color = "Red Updated"
            };

            var result = await _controller.Edit(carId, vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedCar = await _context.Cars.FindAsync(carId);
            Assert.That(updatedCar?.Color, Is.EqualTo("Red Updated"));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var vm = new CarViewModel { Id = carId };
            _controller.ModelState.AddModelError("Price", "Required");

            var result = await _controller.Edit(carId, vm) as ViewResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithCar_WhenIdIsValid()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Delete(carId) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as Car;
            Assert.That(model?.Id, Is.EqualTo(carId));
        }

        [Test]
        public async Task DeleteConfirmed_DeletesCar_AndRedirectsToIndex()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.DeleteConfirmed(carId) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));

            var deletedCar = await _context.Cars.FindAsync(carId);
            Assert.That(deletedCar, Is.Null);
        }

        [Test]
        public async Task Checkout_ReturnsView_WhenCarIsNotReserved()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.Checkout(carId) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Car;
            Assert.That(model?.Id, Is.EqualTo(carId));
        }

        [Test]
        public async Task Checkout_RedirectsToDetails_WhenCarIsAlreadyReserved()
        {
            var carId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var result = await _controller.Checkout(carId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
            Assert.That(_controller.TempData["ErrorMessage"], Is.Not.Null);
        }

        [Test]
        public async Task CheckoutConfirm_CreatesReservation_AndRedirectsToConfirmation()
        {
            var carId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var result = await _controller.CheckoutConfirm(carId, "1234123412341234") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Confirmation"));

            var car = await _context.Cars.FindAsync(carId);
            Assert.That(car?.IsReserved, Is.True);
            Assert.That(car?.ReservedByUserId, Is.EqualTo("test-user-id"));
            Assert.That(_context.Reservations.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CheckoutConfirm_RedirectsToDetails_WhenAlreadyReserved()
        {
            var carId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var result = await _controller.CheckoutConfirm(carId, "1234123412341234") as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Details"));
        }

        [Test]
        public async Task Confirmation_ReturnsView_WhenReservationExists()
        {
            var resId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var result = await _controller.Confirmation(resId) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Reservation;
            Assert.That(model?.Id, Is.EqualTo(resId));
        }

        [Test]
        public async Task ReservedCars_ReturnsViewWithReservedCars()
        {
            var result = await _controller.ReservedCars() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Reservation>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task CancelReservation_RemovesReservation_AndUnreservesCar()
        {
            var resId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var result = await _controller.CancelReservation(resId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("ReservedCars"));

            var car = await _context.Cars.FindAsync(Guid.Parse("44444444-4444-4444-4444-444444444444"));
            Assert.That(car?.IsReserved, Is.False);
            Assert.That(car?.ReservedByUserId, Is.Null);
            Assert.That(_context.Reservations.Count(), Is.EqualTo(0));
        }
    }
}
