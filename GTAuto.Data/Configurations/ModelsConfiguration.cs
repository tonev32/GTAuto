using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GTAuto.Data.Models;

namespace GTAuto.Data.Configurations
{
    public class ModelsConfiguration : IEntityTypeConfiguration<Model>
    {
        public static readonly Guid M4Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000001");
        public static readonly Guid RS7Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000002");
        public static readonly Guid AMGGTId = Guid.Parse("AAAA0000-0000-0000-0000-000000000003");
        public static readonly Guid Golf6Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000004");
        public static readonly Guid TeslaSId = Guid.Parse("AAAA0000-0000-0000-0000-000000000005");
        public static readonly Guid X5Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000006");
        public static readonly Guid A6Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000007");
        public static readonly Guid CorollaId = Guid.Parse("AAAA0000-0000-0000-0000-000000000008");
        public static readonly Guid GClassId = Guid.Parse("AAAA0000-0000-0000-0000-000000000009");
        public static readonly Guid M5E39Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000010");
        public static readonly Guid ML63Id = Guid.Parse("AAAA0000-0000-0000-0000-000000000011");
        public static readonly Guid RaptorId = Guid.Parse("AAAA0000-0000-0000-0000-000000000012");
        public static readonly Guid UrusId = Guid.Parse("AAAA0000-0000-0000-0000-000000000013");
        public static readonly Guid Nissan350ZId = Guid.Parse("AAAA0000-0000-0000-0000-000000000014");

        public void Configure(EntityTypeBuilder<Model> builder)
        {
            var brandId = BrandsConfiguration.GeneralBrandId;

            builder.HasData(
                new Model { Id = M4Id, Name = "BMW M4 Competition", BrandId = brandId },
                new Model { Id = RS7Id, Name = "Audi RS7", BrandId = brandId },
                new Model { Id = AMGGTId, Name = "Mercedes AMG GT", BrandId = brandId },
                new Model { Id = Golf6Id, Name = "VW Golf 6 GTI", BrandId = brandId },
                new Model { Id = TeslaSId, Name = "Tesla Model S", BrandId = brandId },
                new Model { Id = X5Id, Name = "BMW X5", BrandId = brandId },
                new Model { Id = A6Id, Name = "Audi A6", BrandId = brandId },
                new Model { Id = CorollaId, Name = "Toyota Corolla", BrandId = brandId },
                new Model { Id = GClassId, Name = "Mercedes G-Class", BrandId = brandId },
                new Model { Id = M5E39Id, Name = "BMW М5 Е39", BrandId = brandId },
                new Model { Id = ML63Id, Name = "Mercedes ML 63 AMG", BrandId = brandId },
                new Model { Id = RaptorId, Name = "Ford F150 Raptor", BrandId = brandId },
                new Model { Id = UrusId, Name = "Lamborghini Urus", BrandId = brandId },
                new Model { Id = Nissan350ZId, Name = "Nissan 350Z Tuned", BrandId = brandId }
            );
        }
    }
}