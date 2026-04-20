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
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WishlistCar> WishlistCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BrandsConfiguration());
            modelBuilder.ApplyConfiguration(new ModelsConfiguration());
            modelBuilder.ApplyConfiguration(new CarsConfiguration());
            modelBuilder.ApplyConfiguration(new CarImagesConfiguration());
        modelBuilder.Entity<CarFeature>()
                .HasKey(cf => new { cf.CarId, cf.FeatureId });
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.DepositAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DepositPaid)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}