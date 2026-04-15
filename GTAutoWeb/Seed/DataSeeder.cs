using GTAuto.WebApp.Seed;
using GTAutoWeb.Seed;
using GTAuto.Data;

namespace GTAutoWeb.Seed
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<GTAutoDbContext>();

            await CarSeeder.Seed(context);
        }
    }
}
