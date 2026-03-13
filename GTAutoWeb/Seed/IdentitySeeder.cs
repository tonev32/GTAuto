using GTAuto.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Hospital.WebProject.Seed
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
        public static async Task SeedAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminUsername = "admin";
            var adminEmail = "admin@admin.com";
            var password = "Admin1234";

            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                user = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName= adminUsername,
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}   