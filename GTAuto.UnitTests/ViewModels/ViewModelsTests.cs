using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GTAutoWeb.ViewModel;
using GTAutoWeb.ViewModels.User;

namespace GTAuto.UnitTests.ViewModels
{
    [TestFixture]
    public class ViewModelsTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Test]
        public void CarViewModel_Validation_MissingProperties_ReturnsErrors()
        {
            var vm = new CarViewModel();
            var results = ValidateModel(vm);
            
            Assert.That(results.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CarViewModel_Validation_ValidModel_Passes()
        {
            var vm = new CarViewModel
            {
                ModelId = Guid.NewGuid(),
                Year = 2020,
                HorsePower = 300,
                Price = 50000,
                Mileage = 10000,
                FuelType = "Petrol",
                Transmission = "Automatic",
                Color = "Black",
                Description = "Desc"
            };

            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.EqualTo(0));
            Assert.That(vm.CarFeatures, Is.Not.Null);
            Assert.That(vm.Orders, Is.Not.Null);
        }

        [Test]
        public void RegisterViewModel_Validation_ValidModel_Passes()
        {
            var vm = new RegisterViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                Role = "Client"
            };

            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void RegisterViewModel_Validation_MissingProperties_ReturnsErrors()
        {
            var vm = new RegisterViewModel();
            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.GreaterThan(0));
        }

        [Test]
        public void LoginViewModel_Validation_ValidModel_Passes()
        {
            var vm = new LoginViewModel
            {
                Email = "john@doe.com",
                Password = "Password123!"
            };

            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void BrandViewModel_Validation_ValidModel_Passes()
        {
            var vm = new BrandViewModel { Name = "Audi" };
            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void ModelViewModel_Validation_ValidModel_Passes()
        {
            var vm = new ModelViewModel { Name = "A6" };
            var results = ValidateModel(vm);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void OrderViewModel_Properties_AreSetCorrectly()
        {
            var status = new GTAuto.Data.Models.OrderStatus();
            var vm = new OrderViewModel
            {
                Id = Guid.NewGuid(),
                DepositAmount = 10000,
                Status = status
            };

            Assert.That(vm.DepositAmount, Is.EqualTo(10000));
            Assert.That(vm.Status, Is.EqualTo(status));
        }
    }
}
