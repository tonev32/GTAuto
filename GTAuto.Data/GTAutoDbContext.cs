using GTAuto.Data.Configurations;
using GTAuto.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GTAuto.Data
{
    public class GTAutoDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public GTAutoDbContext(DbContextOptions<GTAutoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CarFeature> CarFeatures { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Първо зареждаме главната марка (General)
            modelBuilder.ApplyConfiguration(new BrandsConfiguration());

            // 2. След това зареждаме всички 14 модела (те се свързват с марката)
            modelBuilder.ApplyConfiguration(new ModelsConfiguration());

            // 3. Накрая зареждаме готовите обяви/карти (те се свързват с моделите)
            modelBuilder.ApplyConfiguration(new CarsConfiguration());

            // Конфигурация за сложната връзка между коли и екстри
            modelBuilder.Entity<CarFeature>()
                .HasKey(cf => new { cf.CarId, cf.FeatureId });

            // Ограничаваме точността на цената за SQL Server
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.DepositAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}