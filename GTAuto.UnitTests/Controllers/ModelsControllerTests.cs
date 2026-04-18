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
using GTAutoWeb.ViewModel;

namespace GTAuto.UnitTests.Controllers
{
    [TestFixture]
    public class ModelsControllerTests
    {
        private GTAutoDbContext _context = null!;
        private ModelsController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Models_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new ModelsController(_context);

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
            var brand = new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Audi" };
            _context.Brands.Add(brand);
            
            _context.Models.Add(new Model { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "RS6", BrandId = brand.Id });
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithModels()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Model>;
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
        public async Task Details_ReturnsNotFound_WhenModelDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithModel_WhenIdIsValid()
        {
            var result = await _controller.Details(Guid.Parse("22222222-2222-2222-2222-222222222222")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Model;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("RS6"));
        }

        [Test]
        public void Create_Get_ReturnsView_AndSetsBrandId()
        {
            var result = _controller.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ViewBag.BrandId, Is.Not.Null);
        }

        [Test]
        public void Create_Get_CreatesGeneralBrand_IfNoneExists()
        {
            _context.Models.RemoveRange(_context.Models);
            _context.Brands.RemoveRange(_context.Brands);
            _context.SaveChanges();

            var result = _controller.Create() as ViewResult;
            Assert.That(result, Is.Not.Null);
            
            var brand = _context.Brands.FirstOrDefault();
            Assert.That(brand, Is.Not.Null);
            Assert.That(brand.Name, Is.EqualTo("General"));
            Assert.That(_controller.ViewBag.BrandId, Is.EqualTo(brand.Id));
        }

        [Test]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var vm = new ModelViewModel { Name = "A8" };
            var result = await _controller.Create(vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Models.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task Create_Post_CreatesGeneralBrand_IfNoneExists()
        {
            _context.Models.RemoveRange(_context.Models);
            _context.Brands.RemoveRange(_context.Brands);
            _context.SaveChanges();

            var vm = new ModelViewModel { Name = "A8" };
            var result = await _controller.Create(vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var brand = _context.Brands.FirstOrDefault();
            Assert.That(brand, Is.Not.Null);
            Assert.That(brand.Name, Is.EqualTo("General"));
            
            var model = _context.Models.FirstOrDefault(m => m.Name == "A8");
            Assert.That(model, Is.Not.Null);
            Assert.That(model.BrandId, Is.EqualTo(brand.Id));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenModelDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithModel_WhenIdIsValid()
        {
            var result = await _controller.Edit(Guid.Parse("22222222-2222-2222-2222-222222222222")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Model;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("RS6"));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var model = new Model { Id = Guid.NewGuid(), Name = "Update" };
            var result = await _controller.Edit(Guid.NewGuid(), model);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var modelId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var model = new Model { Id = modelId, Name = "RS6 Updated" };
            
            _context.ChangeTracker.Clear();

            var result = await _controller.Edit(modelId, model) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedModel = await _context.Models.FindAsync(modelId);
            Assert.That(updatedModel?.Name, Is.EqualTo("RS6 Updated"));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var modelId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var model = new Model { Id = modelId, Name = "" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(modelId, model) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(model));
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenModelDoesNotExist()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithModel_WhenIdIsValid()
        {
            var result = await _controller.Delete(Guid.Parse("22222222-2222-2222-2222-222222222222")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Model;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("RS6"));
        }

        [Test]
        public async Task DeleteConfirmed_DeletesModel_AndRedirectsToIndex()
        {
            var modelId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var result = await _controller.DeleteConfirmed(modelId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var deletedModel = await _context.Models.FindAsync(modelId);
            Assert.That(deletedModel, Is.Null);
            Assert.That(_context.Models.Count(), Is.EqualTo(0));
        }
    }
}
