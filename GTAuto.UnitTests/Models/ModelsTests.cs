using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GTAuto.Data.Models;
using GTAutoWeb.Models;
using GTAutoWeb.ViewModel;
using GTAutoWeb.ViewModels.User;

namespace GTAuto.UnitTests.Models
{
    [TestFixture]
    public class ModelsTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Test]
        public void Brand_Validation_MissingName_ReturnsError()
        {
            var brand = new Brand();
            var results = ValidateModel(brand);
            Assert.That(results.Count, Is.GreaterThan(0));
            Assert.That(results[0].MemberNames, Contains.Item("Name"));
        }

        [Test]
        public void Brand_Validation_ValidModel_Passes()
        {
            var brand = new Brand { Name = "Audi" };
            var results = ValidateModel(brand);
            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(brand.Models, Is.Not.Null); // Check collection init
        }

        [Test]
        public void Model_Validation_MissingName_ReturnsError()
        {
            var model = new Model { BrandId = Guid.NewGuid() };
            var results = ValidateModel(model);
            Assert.That(results.Count, Is.GreaterThan(0));
            Assert.That(results[0].MemberNames, Contains.Item("Name"));
        }

        [Test]
        public void Model_Validation_ValidModel_Passes()
        {
            var model = new Model { Name = "A6", BrandId = Guid.NewGuid() };
            var results = ValidateModel(model);
            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(model.Cars, Is.Not.Null);
        }

        [Test]
        public void Car_Validation_InvalidRanges_ReturnsErrors()
        {
            var car = new Car
            {
                ModelId = Guid.NewGuid(),
                Year = 1900, // Invalid range
                HorsePower = 10, // Invalid range
                Price = -10, // Invalid range
                Mileage = 10000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Color = "Black",
                Description = "Desc"
            };

            var results = ValidateModel(car);
            Assert.That(results.Count, Is.EqualTo(3));
        }

        [Test]
        public void Car_Validation_ValidModel_Passes()
        {
            var car = new Car
            {
                ModelId = Guid.NewGuid(),
                Year = 2020,
                HorsePower = 300,
                Price = 50000,
                Mileage = 10000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Color = "Black",
                Description = "Desc",
                IsReserved = false,
                IsSold = false,
                IsAutomatic = true,
                IsFlashOffer = false
            };

            var results = ValidateModel(car);
            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(car.CarFeatures, Is.Not.Null);
            Assert.That(car.Orders, Is.Not.Null);
            Assert.That(car.Images, Is.Not.Null);
            Assert.That(car.Reservations, Is.Not.Null);
        }

        [Test]
        public void Order_Validation_InvalidDeposit_ReturnsError()
        {
            var order = new Order
            {
                UserId = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                DepositAmount = -50 // Invalid deposit range
            };

            var results = ValidateModel(order);
            Assert.That(results.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Order_Validation_ValidModel_Passes()
        {
            var order = new Order
            {
                UserId = Guid.NewGuid(),
                CarId = Guid.NewGuid(),
                DepositAmount = 1000,
                CreatedOn = DateTime.UtcNow
            };

            var results = ValidateModel(order);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void Reservation_Properties_AreSetCorrectly()
        {
            var res = new Reservation
            {
                Id = Guid.NewGuid(),
                UserId = "user1",
                CarId = Guid.NewGuid(),
                DepositPaid = 1000,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            };

            Assert.That(res.UserId, Is.EqualTo("user1"));
            Assert.That(res.DepositPaid, Is.EqualTo(1000));
        }

        [Test]
        public void User_Properties_AreSetCorrectly()
        {
            var user = new User { FullName = "John Doe" };
            Assert.That(user.FullName, Is.EqualTo("John Doe"));
        }

        [Test]
        public void WishlistCar_Properties_AreSetCorrectly()
        {
            var wc = new WishlistCar { UserId = "user1", CarId = Guid.NewGuid() };
            Assert.That(wc.UserId, Is.EqualTo("user1"));
        }

        [Test]
        public void ErrorViewModel_Properties_AreSetCorrectly()
        {
            var evm = new ErrorViewModel { RequestId = "123" };
            Assert.That(evm.ShowRequestId, Is.True);
            Assert.That(evm.RequestId, Is.EqualTo("123"));

            evm.RequestId = null;
            Assert.That(evm.ShowRequestId, Is.False);
        }

        [Test]
        public void CarFeature_Properties_AreSetCorrectly()
        {
            var cf = new CarFeature { CarId = Guid.NewGuid(), FeatureId = Guid.NewGuid() };
            Assert.That(cf.CarId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(cf.FeatureId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void CarImage_Properties_AreSetCorrectly()
        {
            var ci = new CarImage { ImagePath = "/test.png", Order = 1, CarId = Guid.NewGuid() };
            Assert.That(ci.ImagePath, Is.EqualTo("/test.png"));
            Assert.That(ci.Order, Is.EqualTo(1));
        }

        [Test]
        public void Feature_Validation_Passes()
        {
            var f = new Feature { Name = "Sunroof" };
            var results = ValidateModel(f);
            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(f.CarFeatures, Is.Not.Null);
        }

        [Test]
        public void Fuel_Properties_AreSetCorrectly()
        {
            var fuel = new Fuel { Type = "Electric", FuelConsumption = "10kW" };
            Assert.That(fuel.Type, Is.EqualTo("Electric"));
            Assert.That(fuel.FuelConsumption, Is.EqualTo("10kW"));
        }
    }
}
