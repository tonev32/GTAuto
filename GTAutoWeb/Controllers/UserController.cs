using GTAuto.Data.Models;
using GTAutoWeb.ViewModels.User;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace Hospital.WebProject.Controllers

{

    public class UserController : Controller

    {

        private readonly GTAutoDbContext Context;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signManager;

        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserController(GTAutoDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager)

        {

            this.Context = context;

            this.userManager = userManager;

            this.signManager = signInManager;

            this.roleManager = roleManager;

        }

        [HttpGet]

        public IActionResult Register()

        {

            if (User?.Identity?.IsAuthenticated ?? false)

            {

                return RedirectToAction("Index", "Home");

            }

            var model = new RegisterViewModel();

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)

        {

            if (!ModelState.IsValid)

            {

                return View(model);

            }

            var user = new User()

            {

                Email = model.Email,

                UserName = model.FirstName + model.LastName,

                FullName=model.Email

            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)

            {

                if (model.Role == "Client")

                {

                    await userManager.AddToRoleAsync(user, model.Role);

                }

                return RedirectToAction("Login", "User");

            }

            foreach (var item in result.Errors)

            {

                ModelState.AddModelError("", item.Description);

            }

            return View(model);

        }

        [HttpGet]

        public IActionResult Login()

        {

            if (User?.Identity?.IsAuthenticated ?? false)

            {

                return RedirectToAction("Index", "Home");

            }

            var model = new LoginViewModel();

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)

        {

            if (!ModelState.IsValid)

            {

                return View(model);

            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)

            {

                ModelState.AddModelError("", "Invalid email or password");

                return View(model);

            }

            var result = await signManager.PasswordSignInAsync(user, model.Password, true, false);

            if (!result.Succeeded)

            {

                ModelState.AddModelError("", "Invalid email or password");

                return View(model);

            }


            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()

        {

            await signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }

        [AllowAnonymous]

        public async Task<IActionResult> SeedRoles()

        {

            string[] roles = { "Admin", "Client" };

            foreach (var role in roles)

            {

                if (!await roleManager.RoleExistsAsync(role))

                {

                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));

                }

            }

            return Content("Roles seeded (created if missing).");

        }

    }

}

