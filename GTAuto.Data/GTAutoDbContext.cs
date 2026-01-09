using GTAuto.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarFeature>()
            .HasKey(cf => new { cf.CarId, cf.FeatureId });
    }
}