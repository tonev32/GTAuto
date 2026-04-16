using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GTAuto.Data.Models;
using System;

namespace GTAuto.Data.Configurations
{
    public class CarImagesConfiguration : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.HasData(
                // 1. BMW M4
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000001"), CarId = CarsConfiguration.M4Id, ImagePath = "/images/m4.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000002"), CarId = CarsConfiguration.M4Id, ImagePath = "/images/m4inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000003"), CarId = CarsConfiguration.M4Id, ImagePath = "/images/m4back.jpg", Order = 3 },

                // 2. Audi RS7
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000004"), CarId = CarsConfiguration.RS7Id, ImagePath = "/images/rs7.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000005"), CarId = CarsConfiguration.RS7Id, ImagePath = "/images/rs7inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000006"), CarId = CarsConfiguration.RS7Id, ImagePath = "/images/rs7back.jpg", Order = 3 },

                // 3. Mercedes AMG GT
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000007"), CarId = CarsConfiguration.AMGGTId, ImagePath = "/images/gt63.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000008"), CarId = CarsConfiguration.AMGGTId, ImagePath = "/images/amggtinside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000009"), CarId = CarsConfiguration.AMGGTId, ImagePath = "/images/amggtback.jpg", Order = 3 },

                // 4. VW Golf 6 GTI
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000010"), CarId = CarsConfiguration.Golf6Id, ImagePath = "/images/golf6.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000011"), CarId = CarsConfiguration.Golf6Id, ImagePath = "/images/golf6inside.JPG", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000012"), CarId = CarsConfiguration.Golf6Id, ImagePath = "/images/golf6back.jpg", Order = 3 },

                // 5. Tesla Model S Plaid
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000013"), CarId = CarsConfiguration.TeslaSId, ImagePath = "/images/tesla.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000014"), CarId = CarsConfiguration.TeslaSId, ImagePath = "/images/teslainside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000015"), CarId = CarsConfiguration.TeslaSId, ImagePath = "/images/teslaback.jpg", Order = 3 },

                // 6. BMW X5
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000016"), CarId = CarsConfiguration.X5Id, ImagePath = "/images/x5.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000017"), CarId = CarsConfiguration.X5Id, ImagePath = "/images/x5inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000018"), CarId = CarsConfiguration.X5Id, ImagePath = "/images/x5back.jpg", Order = 3 },

                // 7. Audi A6
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000019"), CarId = CarsConfiguration.A6Id, ImagePath = "/images/a6.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000020"), CarId = CarsConfiguration.A6Id, ImagePath = "/images/a6inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000021"), CarId = CarsConfiguration.A6Id, ImagePath = "/images/a6back.jpg", Order = 3 },

                // 8. Toyota Corolla (Toyota в твоите файлове)
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000022"), CarId = CarsConfiguration.CorollaId, ImagePath = "/images/toyota.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000023"), CarId = CarsConfiguration.CorollaId, ImagePath = "/images/toyotainside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000024"), CarId = CarsConfiguration.CorollaId, ImagePath = "/images/toyotaback.jpg", Order = 3 },

                // 9. Mercedes G-Class
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000025"), CarId = CarsConfiguration.GClassId, ImagePath = "/images/gclass.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000026"), CarId = CarsConfiguration.GClassId, ImagePath = "/images/gclassinside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000027"), CarId = CarsConfiguration.GClassId, ImagePath = "/images/gclassback.jpg", Order = 3 },

                // 10. BMW M5 E39
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000028"), CarId = CarsConfiguration.M5E39Id, ImagePath = "/images/e39.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000029"), CarId = CarsConfiguration.M5E39Id, ImagePath = "/images/e39inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000030"), CarId = CarsConfiguration.M5E39Id, ImagePath = "/images/e39back.jpg", Order = 3 },

                // 11. Mercedes ML63
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000031"), CarId = CarsConfiguration.ML63Id, ImagePath = "/images/ml63.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000032"), CarId = CarsConfiguration.ML63Id, ImagePath = "/images/ml63inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000033"), CarId = CarsConfiguration.ML63Id, ImagePath = "/images/ml63back.jpg", Order = 3 },

                // 12. Ford F-150 Raptor
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000034"), CarId = CarsConfiguration.RaptorId, ImagePath = "/images/f150.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000035"), CarId = CarsConfiguration.RaptorId, ImagePath = "/images/f150inside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000036"), CarId = CarsConfiguration.RaptorId, ImagePath = "/images/f150back.jpg", Order = 3 },

                // 13. Lamborghini Urus
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000037"), CarId = CarsConfiguration.UrusId, ImagePath = "/images/urus.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000038"), CarId = CarsConfiguration.UrusId, ImagePath = "/images/urusinside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000039"), CarId = CarsConfiguration.UrusId, ImagePath = "/images/urusback.jpg", Order = 3 },

                // 14. Nissan 350Z
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000040"), CarId = CarsConfiguration.Nissan350ZId, ImagePath = "/images/350z.jpg", Order = 1 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000041"), CarId = CarsConfiguration.Nissan350ZId, ImagePath = "/images/350zinside.jpg", Order = 2 },
                new CarImage { Id = Guid.Parse("F0000000-0000-0000-0000-000000000042"), CarId = CarsConfiguration.Nissan350ZId, ImagePath = "/images/350zback.jpg", Order = 3 }
            );
        }
    }
}