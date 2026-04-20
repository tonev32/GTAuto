using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using GTAuto.Data;
using GTAuto.Data.Models;
using GTAuto.WebApp.Seed;
using GTAutoWeb.Seed;
using Hospital.WebProject.Seed;

namespace GTAuto.UnitTests.Seed
{
    [TestFixture]
    public class SeedersTests
    {
        private GTAutoDbContext _context = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GTAutoDbContext>()
                .UseInMemoryDatabase(databaseName: "GTAutoTestDb_Seeders_" + Guid.NewGuid().ToString())
                .Options;

            _context = new GTAutoDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public async Task CarSeeder_Seed_AddsCarsAndImages_WhenDatabaseIsEmpty()
        {
            await CarSeeder.Seed(_context);

            Assert.That(_context.Cars.Count(), Is.EqualTo(3));
            Assert.That(_context.CarImages.Count(), Is.EqualTo(3));
            
            var bmw = await _context.Cars.FirstOrDefaultAsync(c => c.Color == "Black" && c.Price == 18000);
            Assert.That(bmw, Is.Not.Null);
            Assert.That(bmw.ModelId, Is.EqualTo(Guid.Parse("11111111-1111-1111-1111-111111111111")));
        }

        [Test]
        public async Task CarSeeder_Seed_DoesNothing_WhenDatabaseIsNotEmpty()
        {
            _context.Cars.Add(new Car 
            { 
                Id = Guid.NewGuid(), 
                ModelId = Guid.NewGuid(), 
                Price = 1000, 
                Year = 2020, 
                Color = "C", Description = "D", FuelType = "F", Transmission = "T" 
            });
            await _context.SaveChangesAsync();

            await CarSeeder.Seed(_context);

            Assert.That(_context.Cars.Count(), Is.EqualTo(1));
            Assert.That(_context.CarImages.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DataSeeder_Initialize_CallsCarSeeder_UsingServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<GTAutoDbContext>(_context);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            await DataSeeder.Initialize(serviceProvider);

            Assert.That(_context.Cars.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task IdentitySeeder_SeedRolesAsync_CreatesAdminAndClientRoles()
        {
            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

            roleManagerMock.SetupSequence(r => r.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false) 
                .ReturnsAsync(false); 

            roleManagerMock.Setup(r => r.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                .ReturnsAsync(IdentityResult.Success);

            await IdentitySeeder.SeedRolesAsync(roleManagerMock.Object);

            roleManagerMock.Verify(r => r.CreateAsync(It.Is<IdentityRole<Guid>>(role => role.Name == "Admin")), Times.Once);
            roleManagerMock.Verify(r => r.CreateAsync(It.Is<IdentityRole<Guid>>(role => role.Name == "Client")), Times.Once);
        }

        [Test]
        public async Task IdentitySeeder_SeedRolesAsync_DoesNotCreateRole_IfItExists()
        {
            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

            roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            await IdentitySeeder.SeedRolesAsync(roleManagerMock.Object);

            roleManagerMock.Verify(r => r.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Never);
        }

        [Test]
        public async Task IdentitySeeder_SeedAdminAsync_CreatesAdminUserAndAssignsRole()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

            userManagerMock.Setup(u => u.FindByEmailAsync("admin@admin.com"))
                .ReturnsAsync((User)null!);
            
            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), "Admin1234"))
                .ReturnsAsync(IdentityResult.Success);
            
            roleManagerMock.Setup(r => r.RoleExistsAsync("Admin"))
                .ReturnsAsync(false);
            
            roleManagerMock.Setup(r => r.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                .ReturnsAsync(IdentityResult.Success);
            
            userManagerMock.Setup(u => u.IsInRoleAsync(It.IsAny<User>(), "Admin"))
                .ReturnsAsync(false);
            
            userManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<User>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            await IdentitySeeder.SeedAdminAsync(userManagerMock.Object, roleManagerMock.Object);

            userManagerMock.Verify(u => u.CreateAsync(It.Is<User>(u => u.Email == "admin@admin.com"), "Admin1234"), Times.Once);
            roleManagerMock.Verify(r => r.CreateAsync(It.Is<IdentityRole<Guid>>(role => role.Name == "Admin")), Times.Once);
            userManagerMock.Verify(u => u.AddToRoleAsync(It.Is<User>(u => u.Email == "admin@admin.com"), "Admin"), Times.Once);
        }

        [Test]
        public void IdentitySeeder_SeedAdminAsync_ThrowsException_IfUserCreationFailed()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            var userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            var roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

            userManagerMock.Setup(u => u.FindByEmailAsync("admin@admin.com"))
                .ReturnsAsync((User)null!);
            
            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), "Admin1234"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            Assert.ThrowsAsync<Exception>(async () => await IdentitySeeder.SeedAdminAsync(userManagerMock.Object, roleManagerMock.Object));
        }
    }
}
