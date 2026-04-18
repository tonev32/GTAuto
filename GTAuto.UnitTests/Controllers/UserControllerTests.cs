using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GTAuto.Data;
using GTAuto.Data.Models;
using Hospital.WebProject.Controllers; // based on original file namespace
using GTAutoWeb.ViewModels.User;

namespace GTAuto.UnitTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<UserManager<User>> _userManagerMock = null!;
        private Mock<SignInManager<User>> _signInManagerMock = null!;
        private Mock<RoleManager<IdentityRole<Guid>>> _roleManagerMock = null!;
        private UserController _controller = null!;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            _signInManagerMock = new Mock<SignInManager<User>>(_userManagerMock.Object, contextAccessorMock.Object, claimsFactoryMock.Object, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(roleStoreMock.Object, null, null, null, null);

            _controller = new UserController(null!, _userManagerMock.Object, _signInManagerMock.Object, _roleManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        private void SetUserIsAuthenticated(bool isAuthenticated)
        {
            var claims = isAuthenticated ? new[] { new Claim(ClaimTypes.NameIdentifier, "test-user") } : new Claim[0];
            var identity = isAuthenticated ? new ClaimsIdentity(claims, "TestAuth") : new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
        }

        [Test]
        public void Register_Get_ReturnsView_WhenNotAuthenticated()
        {
            SetUserIsAuthenticated(false);
            var result = _controller.Register() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<RegisterViewModel>());
        }

        [Test]
        public void Register_Get_RedirectsToHome_WhenAuthenticated()
        {
            SetUserIsAuthenticated(true);
            var result = _controller.Register() as RedirectToActionResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Register_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var vm = new RegisterViewModel();
            
            var result = await _controller.Register(vm) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(vm));
        }

        [Test]
        public async Task Register_Post_RedirectsToLogin_WhenSuccessful()
        {
            var vm = new RegisterViewModel { Email = "test@test.com", Password = "Password123!", Role = "Client", FirstName = "John", LastName = "Doe" };
            
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), vm.Password))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), vm.Role))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _controller.Register(vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Login"));
            Assert.That(result.ControllerName, Is.EqualTo("User"));
        }

        [Test]
        public async Task Register_Post_ReturnsViewWithErrors_WhenCreateFails()
        {
            var vm = new RegisterViewModel { Email = "test@test.com", Password = "Password123!" };
            var error = new IdentityError { Description = "Password too weak" };
            
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), vm.Password))
                .ReturnsAsync(IdentityResult.Failed(error));

            var result = await _controller.Register(vm) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ModelState.ErrorCount, Is.EqualTo(1));
        }

        [Test]
        public void Login_Get_ReturnsView_WhenNotAuthenticated()
        {
            SetUserIsAuthenticated(false);
            var result = _controller.Login() as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<LoginViewModel>());
        }

        [Test]
        public void Login_Get_RedirectsToHome_WhenAuthenticated()
        {
            SetUserIsAuthenticated(true);
            var result = _controller.Login() as RedirectToActionResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Login_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Required");
            var vm = new LoginViewModel();
            
            var result = await _controller.Login(vm) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(vm));
        }

        [Test]
        public async Task Login_Post_ReturnsViewWithError_WhenUserNotFound()
        {
            var vm = new LoginViewModel { Email = "test@test.com", Password = "Password123!" };
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(vm.Email))
                .ReturnsAsync((User)null!);

            var result = await _controller.Login(vm) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ModelState.ErrorCount, Is.GreaterThan(0));
        }

        [Test]
        public async Task Login_Post_ReturnsViewWithError_WhenSignInFails()
        {
            var vm = new LoginViewModel { Email = "test@test.com", Password = "Password123!" };
            var user = new User { Email = vm.Email };
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(vm.Email)).ReturnsAsync(user);
            _signInManagerMock.Setup(x => x.PasswordSignInAsync(user, vm.Password, true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var result = await _controller.Login(vm) as ViewResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ModelState.ErrorCount, Is.GreaterThan(0));
        }

        [Test]
        public async Task Login_Post_RedirectsToHome_WhenSuccessful()
        {
            var vm = new LoginViewModel { Email = "test@test.com", Password = "Password123!" };
            var user = new User { Email = vm.Email };
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(vm.Email)).ReturnsAsync(user);
            _signInManagerMock.Setup(x => x.PasswordSignInAsync(user, vm.Password, true, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await _controller.Login(vm) as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Logout_CallsSignOut_AndRedirectsToHome()
        {
            _signInManagerMock.Setup(x => x.SignOutAsync()).Returns(Task.CompletedTask);

            var result = await _controller.Logout() as RedirectToActionResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
            Assert.That(result.ControllerName, Is.EqualTo("Home"));
            _signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
        }

        [Test]
        public async Task SeedRoles_CreatesMissingRoles_AndReturnsContent()
        {
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Admin")).ReturnsAsync(false);
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Client")).ReturnsAsync(true);
            
            _roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _controller.SeedRoles() as ContentResult;
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo("Roles seeded (created if missing)."));
            
            _roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Admin")), Times.Once);
            _roleManagerMock.Verify(x => x.CreateAsync(It.Is<IdentityRole<Guid>>(r => r.Name == "Client")), Times.Never);
        }
    }
}
