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
    public class BrandsControllerTests
    {
        private GTAutoDbContext _context = null!;
        private BrandsController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Brands_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new BrandsController(_context);

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
            _context.Brands.Add(new Brand { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Audi" });
            _context.Brands.Add(new Brand { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "BMW" });
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithBrands()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Brand>;
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
        public async Task Details_ReturnsNotFound_WhenBrandDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithBrand_WhenIdIsValid()
        {
            var result = await _controller.Details(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Brand;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Audi"));
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
            var brand = new Brand { Name = "Mercedes" };
            var result = await _controller.Create(brand) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Brands.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var brand = new Brand();
            var result = await _controller.Create(brand) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(brand));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenBrandDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithBrand_WhenIdIsValid()
        {
            var result = await _controller.Edit(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Brand;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Audi"));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var brand = new Brand { Id = Guid.NewGuid(), Name = "Audi Update" };
            var result = await _controller.Edit(Guid.NewGuid(), brand);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var brandId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var brand = new Brand { Id = brandId, Name = "Audi Updated" };
            
           
            _context.ChangeTracker.Clear();

            var result = await _controller.Edit(brandId, brand) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedBrand = await _context.Brands.FindAsync(brandId);
            Assert.That(updatedBrand?.Name, Is.EqualTo("Audi Updated"));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var brandId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var brand = new Brand { Id = brandId, Name = "" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(brandId, brand) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(brand));
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenBrandDoesNotExist()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithBrand_WhenIdIsValid()
        {
            var result = await _controller.Delete(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Brand;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Audi"));
        }

        [Test]
        public async Task DeleteConfirmed_DeletesBrand_AndRedirectsToIndex()
        {
            var brandId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.DeleteConfirmed(brandId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var deletedBrand = await _context.Brands.FindAsync(brandId);
            Assert.That(deletedBrand, Is.Null);
            Assert.That(_context.Brands.Count(), Is.EqualTo(1));
        }
    }
}
