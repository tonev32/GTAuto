using GTAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTAuto.Data.Configurations
{
    public class ModelsConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasData(
                new Model
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Porshe 911 Carrera"
                },
                new Model
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "BMW M4 Competition"
                },
                new Model
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Audi RS7"
                },
                 new Model
                 {
                     Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                     Name = "Mercedes AMG GT"
                 },
                new Model
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Name = "VW Golf 6 GTI"
                },
                new Model
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    Name = "Tesla Model S"
                },
                new Model
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    Name = "BMW X5"
                },
                new Model
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    Name = "Audi A6"
                },
                new Model
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Name = "Toyota Corolla"
                },
                new Model
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Mercedes G-Class"
                },
                new Model
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Name = "BMW М5 Е39"
                },
                new Model
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Name = "Mercedes ML 63 AMG"
                },
                new Model
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Name = "Ford F150 Raptor"
                },
                new Model
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Name = "Lamborghini Urus"
                },
                new Model
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    Name = "Nissan 350Z Tuned"
                }
           
            );
        }
    }
}