using GTAuto.Data.Models;
using GTAuto.Data;
using Microsoft.EntityFrameworkCore;

namespace GTAuto.WebApp.Seed
{
    public static class CarSeeder
    {
        public static async Task Seed(GTAutoDbContext context)
        {
            if (context.Cars.Any())
                return;

            var bmwId = Guid.NewGuid();
            var audiId = Guid.NewGuid();
            var golfId = Guid.NewGuid();

            var cars = new List<Car>
            {
                new Car
                {
                    Id = bmwId,
                    ModelId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Year = 2018,
                    HorsePower = 150,
                    Price = 18000,
                    Mileage = 120000,
                    FuelType = "Diesel",
                    Transmission = "Manual",
                    Color = "Black",
                    Description = "Well maintained car, no accidents, full service history.",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = false
                },
                new Car
                {
                    Id = audiId,
                    ModelId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Year = 2020,
                    HorsePower = 190,
                    Price = 25000,
                    Mileage = 80000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "White",
                    Description = "Like new condition, fully loaded, automatic gearbox.",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = true
                },
                new Car
                {
                    Id = golfId,
                    ModelId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Year = 2015,
                    HorsePower = 110,
                    Price = 9500,
                    Mileage = 160000,
                    FuelType = "Diesel",
                    Transmission = "Manual",
                    Color = "Grey",
                    Description = "Reliable and economic car, perfect for daily driving.",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = false
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
            var images = new List<CarImage>
            {
                new CarImage { Id = Guid.NewGuid(), CarId = bmwId, ImagePath = "/images/cars/bmw1.jpg", Order = 1 },
                new CarImage { Id = Guid.NewGuid(), CarId = audiId, ImagePath = "/images/audi.jpg", Order = 1 },
                new CarImage { Id = Guid.NewGuid(), CarId = golfId, ImagePath = "/images/cars/vw_golf.jpg", Order = 1 }
            };

            await context.CarImages.AddRangeAsync(images);
            await context.SaveChangesAsync();
        }
    }
}