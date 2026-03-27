using GTAuto.WebApp.Seed;

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
