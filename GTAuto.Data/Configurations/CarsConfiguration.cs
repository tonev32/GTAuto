using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GTAuto.Data.Models;
using System;

namespace GTAuto.Data.Configurations
{
    public class CarsConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000001"),
                    ModelId = ModelsConfiguration.M4Id,
                    Year = 2022,
                    HorsePower = 510,
                    Price = 145000,
                    Mileage = 8500,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Green",
                    Description = "M-Track Package, Carbon Seats, Laser Lights, Like new!",
                    ImageUrl = "/images/m4.jpg", // Свързано с m4.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000002"),
                    ModelId = ModelsConfiguration.RS7Id,
                    Year = 2023,
                    HorsePower = 600,
                    Price = 178000,
                    Mileage = 1200,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Grey",
                    Description = "Ceramic Brakes, RS Dynamic Plus, Bang & Olufsen.",
                    ImageUrl = "/images/rs7.jpg", // Свързано с rs7.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000003"),
                    ModelId = ModelsConfiguration.AMGGTId,
                    Year = 2021,
                    HorsePower = 530,
                    Price = 155000,
                    Mileage = 14000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Black",
                    Description = "AMG Night Package, Performance Exhaust.",
                    ImageUrl = "/images/gt63.jpg", // Свързано с gt63.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000004"),
                    ModelId = ModelsConfiguration.Golf6Id,
                    Year = 2012,
                    HorsePower = 211,
                    Price = 18000,
                    Mileage = 155000,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    Color = "White",
                    Description = "Stage 1, Akrapovic tips, Edition 35 wheels.",
                    ImageUrl = "/images/golf6.jpg", // Свързано с golf6.jpg
                    IsAutomatic = false
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000005"),
                    ModelId = ModelsConfiguration.TeslaSId,
                    Year = 2022,
                    HorsePower = 1020,
                    Price = 95000,
                    Mileage = 10000,
                    FuelType = "Electric",
                    Transmission = "Automatic",
                    Color = "Red",
                    Description = "Plaid version, Ludicrous mode, Full self-driving.",
                    ImageUrl = "/images/tesla.jpg", // Свързано с tesla.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000006"),
                    ModelId = ModelsConfiguration.X5Id,
                    Year = 2023,
                    HorsePower = 400,
                    Price = 110000,
                    Mileage = 5000,
                    FuelType = "Diesel",
                    Transmission = "Automatic",
                    Color = "Blue",
                    Description = "M-Sport, Sky Lounge, Harman Kardon.",
                    ImageUrl = "/images/x5.jpg", // Свързано с x5.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000007"),
                    ModelId = ModelsConfiguration.A6Id,
                    Year = 2020,
                    HorsePower = 286,
                    Price = 55000,
                    Mileage = 65000,
                    FuelType = "Diesel",
                    Transmission = "Automatic",
                    Color = "Silver",
                    Description = "S-line, Matrix lights, Virtual cockpit.",
                    ImageUrl = "/images/a6.jpg", // Свързано с a6.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000008"),
                    ModelId = ModelsConfiguration.CorollaId,
                    Year = 2023,
                    HorsePower = 140,
                    Price = 32000,
                    Mileage = 0,
                    FuelType = "Hybrid",
                    Transmission = "Automatic",
                    Color = "Blue",
                    Description = "Brand new, 10 years warranty, Hybrid system.",
                    ImageUrl = "/images/toyota.jpg", // Свързано с toyota.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000009"),
                    ModelId = ModelsConfiguration.GClassId,
                    Year = 2022,
                    HorsePower = 585,
                    Price = 235000,
                    Mileage = 12000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Matte Black",
                    Description = "G63 AMG, Night Package, Carbon interior.",
                    ImageUrl = "/images/gclass.jpg", // Свързано с gclass.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000010"),
                    ModelId = ModelsConfiguration.M5E39Id,
                    Year = 2002,
                    HorsePower = 400,
                    Price = 45000,
                    Mileage = 180000,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    Color = "Red",
                    Description = "Collector's car, Perfect condition, V8 Manual.",
                    ImageUrl = "/images/e39.jpg", // Свързано с e39.jpg
                    IsAutomatic = false
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000011"),
                    ModelId = ModelsConfiguration.ML63Id,
                    Year = 2014,
                    HorsePower = 525,
                    Price = 38000,
                    Mileage = 160000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "White",
                    Description = "AMG Performance, Panoramic roof, Full service history.",
                    ImageUrl = "/images/ml63.jpg", // Свързано с ml63.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000012"),
                    ModelId = ModelsConfiguration.RaptorId,
                    Year = 2023,
                    HorsePower = 450,
                    Price = 125000,
                    Mileage = 2000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Orange",
                    Description = "Fox Shocks, 37 Performance Package, Off-road monster.",
                    ImageUrl = "/images/f150.jpg", // Свързано с f150.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000013"),
                    ModelId = ModelsConfiguration.UrusId,
                    Year = 2023,
                    HorsePower = 666,
                    Price = 350000,
                    Mileage = 1500,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "Yellow",
                    Description = "Lamborghini Urus Performante, Titanium Exhaust.",
                    ImageUrl = "/images/urus.jpg", // Свързано с urus.jpg
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.Parse("C0000000-0000-0000-0000-000000000014"),
                    ModelId = ModelsConfiguration.Nissan350ZId,
                    Year = 2007,
                    HorsePower = 350,
                    Price = 25000,
                    Mileage = 120000,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    Color = "Sunset Orange",
                    Description = "Widebody, Custom wheels, Drift setup.",
                    ImageUrl = "/images/350z.jpg", // Свързано с 350z.jpg
                    IsAutomatic = false
                }
            );
        }
    }
}