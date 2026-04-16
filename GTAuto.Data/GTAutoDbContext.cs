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

        // НОВИТЕ ТАБЛИЦИ
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- SEED DATA CONFIGURATIONS ---

            // 1. Първо зареждаме главната марка
            modelBuilder.ApplyConfiguration(new BrandsConfiguration());

            // 2. След това моделите
            modelBuilder.ApplyConfiguration(new ModelsConfiguration());

            // 3. След това готовите обяви (Cars)
            modelBuilder.ApplyConfiguration(new CarsConfiguration());

            // 4. НАКРАЯ: Зареждаме 3-те автоматични снимки за всяка кола
            modelBuilder.ApplyConfiguration(new CarImagesConfiguration());


            // --- ENTITY RELATIONSHIPS & CONSTRAINTS ---

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

            // Ограничаваме точността за новата таблица за Резервации
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DepositPaid)
                .HasColumnType("decimal(18,2)");

            // Настройка на връзката Car -> Images (One-to-Many)
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade); // Ако се изтрие кола, се трият и снимките ѝ
        }
    }
}