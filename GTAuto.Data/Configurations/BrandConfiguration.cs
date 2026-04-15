using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GTAuto.Data.Models;

namespace GTAuto.Data.Configurations
{
    public class BrandsConfiguration : IEntityTypeConfiguration<Brand>
    {
        public static readonly Guid GeneralBrandId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(new Brand { Id = GeneralBrandId, Name = "General" });
        }
    }
}