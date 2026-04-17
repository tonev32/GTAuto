using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GTAuto.Data.Models;
using System;

namespace GTAuto.Data.Configurations
{
    public class CarsConfiguration : IEntityTypeConfiguration<Car>
    {
        public static readonly Guid M4Id = Guid.Parse("C0000000-0000-0000-0000-000000000001");
        public static readonly Guid RS7Id = Guid.Parse("C0000000-0000-0000-0000-000000000002");
        public static readonly Guid AMGGTId = Guid.Parse("C0000000-0000-0000-0000-000000000003");
        public static readonly Guid Golf6Id = Guid.Parse("C0000000-0000-0000-0000-000000000004");
        public static readonly Guid TeslaSId = Guid.Parse("C0000000-0000-0000-0000-000000000005");
        public static readonly Guid X5Id = Guid.Parse("C0000000-0000-0000-0000-000000000006");
        public static readonly Guid A6Id = Guid.Parse("C0000000-0000-0000-0000-000000000007");
        public static readonly Guid CorollaId = Guid.Parse("C0000000-0000-0000-0000-000000000008");
        public static readonly Guid GClassId = Guid.Parse("C0000000-0000-0000-0000-000000000009");
        public static readonly Guid M5E39Id = Guid.Parse("C0000000-0000-0000-0000-000000000010");
        public static readonly Guid ML63Id = Guid.Parse("C0000000-0000-0000-0000-000000000011");
        public static readonly Guid RaptorId = Guid.Parse("C0000000-0000-0000-0000-000000000012");
        public static readonly Guid UrusId = Guid.Parse("C0000000-0000-0000-0000-000000000013");
        public static readonly Guid Nissan350ZId = Guid.Parse("C0000000-0000-0000-0000-000000000014");

        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
                // 🔥 ТРИТЕ "SALE" КОЛИ (IsFlashOffer = true)
                new Car { Id = M4Id, ModelId = ModelsConfiguration.M4Id, Year = 2022, HorsePower = 510, Price = 145000, Mileage = 8500, FuelType = "Petrol", Transmission = "Automatic", Color = "Green", Description = "M-Track Package, Carbon Seats, Laser Lights, Like new!", IsAutomatic = true, IsFlashOffer = true, IsReserved = false, IsSold = false },
                new Car { Id = RS7Id, ModelId = ModelsConfiguration.RS7Id, Year = 2023, HorsePower = 600, Price = 178000, Mileage = 1200, FuelType = "Petrol", Transmission = "Automatic", Color = "Grey", Description = "Ceramic Brakes, RS Dynamic Plus, Bang & Olufsen.", IsAutomatic = true, IsFlashOffer = true, IsReserved = false, IsSold = false },
                new Car { Id = GClassId, ModelId = ModelsConfiguration.GClassId, Year = 2022, HorsePower = 585, Price = 235000, Mileage = 12000, FuelType = "Petrol", Transmission = "Automatic", Color = "Matte Black", Description = "G63 AMG, Night Package, Carbon interior.", IsAutomatic = true, IsFlashOffer = true, IsReserved = false, IsSold = false },

                // 🚗 ОСТАНАЛИТЕ КОЛИ (IsFlashOffer = false)
                new Car { Id = AMGGTId, ModelId = ModelsConfiguration.AMGGTId, Year = 2021, HorsePower = 530, Price = 155000, Mileage = 14000, FuelType = "Petrol", Transmission = "Automatic", Color = "Black", Description = "AMG Night Package, Performance Exhaust.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = Golf6Id, ModelId = ModelsConfiguration.Golf6Id, Year = 2012, HorsePower = 211, Price = 18000, Mileage = 155000, FuelType = "Petrol", Transmission = "Manual", Color = "White", Description = "Stage 1, Akrapovic tips, Edition 35 wheels.", IsAutomatic = false, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = TeslaSId, ModelId = ModelsConfiguration.TeslaSId, Year = 2022, HorsePower = 1020, Price = 95000, Mileage = 10000, FuelType = "Electric", Transmission = "Automatic", Color = "Red", Description = "Plaid version, Ludicrous mode, Full self-driving.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = X5Id, ModelId = ModelsConfiguration.X5Id, Year = 2023, HorsePower = 400, Price = 110000, Mileage = 5000, FuelType = "Diesel", Transmission = "Automatic", Color = "Blue", Description = "M-Sport, Sky Lounge, Harman Kardon.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = A6Id, ModelId = ModelsConfiguration.A6Id, Year = 2020, HorsePower = 286, Price = 55000, Mileage = 65000, FuelType = "Diesel", Transmission = "Automatic", Color = "Silver", Description = "S-line, Matrix lights, Virtual cockpit.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = CorollaId, ModelId = ModelsConfiguration.CorollaId, Year = 2023, HorsePower = 140, Price = 32000, Mileage = 0, FuelType = "Hybrid", Transmission = "Automatic", Color = "Blue", Description = "Brand new, 10 years warranty, Hybrid system.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = M5E39Id, ModelId = ModelsConfiguration.M5E39Id, Year = 2002, HorsePower = 400, Price = 45000, Mileage = 180000, FuelType = "Petrol", Transmission = "Manual", Color = "Red", Description = "Collector's car, Perfect condition, V8 Manual.", IsAutomatic = false, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = ML63Id, ModelId = ModelsConfiguration.ML63Id, Year = 2014, HorsePower = 525, Price = 38000, Mileage = 160000, FuelType = "Petrol", Transmission = "Automatic", Color = "White", Description = "AMG Performance, Panoramic roof, Full service history.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = RaptorId, ModelId = ModelsConfiguration.RaptorId, Year = 2023, HorsePower = 450, Price = 125000, Mileage = 2000, FuelType = "Petrol", Transmission = "Automatic", Color = "Orange", Description = "Fox Shocks, 37 Performance Package, Off-road monster.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = UrusId, ModelId = ModelsConfiguration.UrusId, Year = 2023, HorsePower = 666, Price = 350000, Mileage = 1500, FuelType = "Petrol", Transmission = "Automatic", Color = "Yellow", Description = "Lamborghini Urus Performante, Titanium Exhaust.", IsAutomatic = true, IsFlashOffer = false, IsReserved = false, IsSold = false },
                new Car { Id = Nissan350ZId, ModelId = ModelsConfiguration.Nissan350ZId, Year = 2007, HorsePower = 350, Price = 25000, Mileage = 120000, FuelType = "Petrol", Transmission = "Manual", Color = "Black", Description = "Widebody, Custom wheels, Drift setup.", IsAutomatic = false, IsFlashOffer = false, IsReserved = false, IsSold = false }
            );
        }
    }
}