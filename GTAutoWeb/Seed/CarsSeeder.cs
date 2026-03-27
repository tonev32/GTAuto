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

            var cars = new List<Car>
            {
                new Car
                {
                    Id = Guid.NewGuid(),
                    ModelId = Guid.Parse("11111111-1111-1111-1111-111111111111"), 
                    Year = 2018,
                    HorsePower = 150,
                    Price = 18000,
                    Mileage = 120000,
                    FuelType = "Diesel",
                    Transmission = "Manual",
                    Color = "Black",
                    Description = "Well maintained car, no accidents, full service history.",
                    ImageUrl = "/images/cars/bmw1.jpg",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = false
                },
                new Car
                {
                    Id = Guid.NewGuid(),
                    ModelId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Year = 2020,
                    HorsePower = 190,
                    Price = 25000,
                    Mileage = 80000,
                    FuelType = "Petrol",
                    Transmission = "Automatic",
                    Color = "White",
                    Description = "Like new condition, fully loaded, automatic gearbox.",
                    ImageUrl = "/images/audi.jpg",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = true
                },
                new Car
                {
                    Id = Guid.NewGuid(),
                    ModelId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Year = 2015,
                    HorsePower = 110,
                    Price = 9500,
                    Mileage = 160000,
                    FuelType = "Diesel",
                    Transmission = "Manual",
                    Color = "Grey",
                    Description = "Reliable and economic car, perfect for daily driving.",
                    ImageUrl = "/images/cars/vw_golf.jpg",
                    IsReserved = false,
                    IsSold = false,
                    IsAutomatic = false
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}