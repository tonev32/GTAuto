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
    public class FeaturesControllerTests
    {
        private GTAutoDbContext _context = null!;
        private FeaturesController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Features_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
            _controller = new FeaturesController(_context);

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
            _context.Features.Add(new Feature { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Navigation" });
            _context.Features.Add(new Feature { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Sunroof" });
            _context.SaveChanges();
        }

        [Test]
        public async Task Index_ReturnsViewWithFeatures()
        {
            var result = await _controller.Index() as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<Feature>;
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
        public async Task Details_ReturnsNotFound_WhenFeatureDoesNotExist()
        {
            var result = await _controller.Details(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Details_ReturnsViewWithFeature_WhenIdIsValid()
        {
            var result = await _controller.Details(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Feature;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Navigation"));
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
            var feature = new Feature { Name = "Leather Seats" };
            var result = await _controller.Create(feature) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(_context.Features.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Create_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var feature = new Feature();
            var result = await _controller.Create(feature) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(feature));
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenFeatureDoesNotExist()
        {
            var result = await _controller.Edit(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Get_ReturnsViewWithFeature_WhenIdIsValid()
        {
            var result = await _controller.Edit(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Feature;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Navigation"));
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var feature = new Feature { Id = Guid.NewGuid(), Name = "Navigation Update" };
            var result = await _controller.Edit(Guid.NewGuid(), feature);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var featureId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var feature = new Feature { Id = featureId, Name = "Navigation Updated" };
            
            _context.ChangeTracker.Clear();

            var result = await _controller.Edit(featureId, feature) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var updatedFeature = await _context.Features.FindAsync(featureId);
            Assert.That(updatedFeature?.Name, Is.EqualTo("Navigation Updated"));
        }

        [Test]
        public async Task Edit_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            var featureId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var feature = new Feature { Id = featureId, Name = "" };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Edit(featureId, feature) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(feature));
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Delete(null);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenFeatureDoesNotExist()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Delete_Get_ReturnsViewWithFeature_WhenIdIsValid()
        {
            var result = await _controller.Delete(Guid.Parse("11111111-1111-1111-1111-111111111111")) as ViewResult;
            Assert.That(result, Is.Not.Null);
            var model = result.Model as Feature;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Name, Is.EqualTo("Navigation"));
        }

        [Test]
        public async Task DeleteConfirmed_DeletesFeature_AndRedirectsToIndex()
        {
            var featureId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var result = await _controller.DeleteConfirmed(featureId) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            
            var deletedFeature = await _context.Features.FindAsync(featureId);
            Assert.That(deletedFeature, Is.Null);
            Assert.That(_context.Features.Count(), Is.EqualTo(1));
        }
    }
}
