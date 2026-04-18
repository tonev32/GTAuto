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
    public class FuelsControllerTests
    {
        private GTAutoDbContext _context = null!;
        private FuelsController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Fuels_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new FuelsController(_context);

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
            _context.Fuels.Add(new Fuel { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Type = "Petrol", FuelConsumption = "10L/100km" });
            _context.Fuels.Add(new Fuel { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Type = "Diesel", FuelConsumption = "6L/100km" });
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithFuels()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Fuel>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenFuelDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithFuel_WhenIdIsValid()
        {
            var result = await _controller.Details(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Fuel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Type, Is.EqualTo("Petrol"));
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
            var fuel = new Fuel { Type = "Electric", FuelConsumption = "15kWh/100km" };
            var result = await _controller.Create(fuel) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Fuels.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Type", "Required");
            var fuel = new Fuel();
            var result = await _controller.Create(fuel) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(fuel));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenFuelDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithFuel_WhenIdIsValid()
        {
            var result = await _controller.Edit(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Fuel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Type, Is.EqualTo("Petrol"));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var fuel = new Fuel { Id = Guid.NewGuid(), Type = "Petrol Update" };
            var result = await _controller.Edit(Guid.NewGuid(), fuel);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var fuelId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fuel = new Fuel { Id = fuelId, Type = "Petrol Updated" };
            
            _context.ChangeTracker.Clear();

            var result = await _controller.Edit(fuelId, fuel) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedFuel = await _context.Fuels.FindAsync(fuelId);
            Assert.That(updatedFuel?.Type, Is.EqualTo("Petrol Updated"));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var fuelId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var fuel = new Fuel { Id = fuelId, Type = "" };
            _controller.ModelState.AddModelError("Type", "Required");

            var result = await _controller.Edit(fuelId, fuel) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(fuel));
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenFuelDoesNotExist()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithFuel_WhenIdIsValid()
        {
            var result = await _controller.Delete(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Fuel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Type, Is.EqualTo("Petrol"));
        }

        [Test]
        public async Task DeleteConfirmed_DeletesFuel_AndRedirectsToIndex()
        {
            var fuelId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.DeleteConfirmed(fuelId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var deletedFuel = await _context.Fuels.FindAsync(fuelId);
            Assert.That(deletedFuel, Is.Null);
            Assert.That(_context.Fuels.Count(), Is.EqualTo(1));
        }
    }
}
