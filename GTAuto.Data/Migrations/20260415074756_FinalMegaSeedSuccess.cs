using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GTAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalMegaSeedSuccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "General" });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaa0000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), "BMW M4 Competition" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), "Audi RS7" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), "Mercedes AMG GT" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), "VW Golf 6 GTI" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), "Tesla Model S" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111"), "BMW X5" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111"), "Audi A6" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111"), "Toyota Corolla" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111"), "Mercedes G-Class" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111"), "BMW М5 Е39" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), "Mercedes ML 63 AMG" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111"), "Ford F150 Raptor" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111"), "Lamborghini Urus" },
                    { new Guid("aaaa0000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111"), "Nissan 350Z Tuned" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Color", "Description", "FuelType", "HorsePower", "ImageUrl", "IsAutomatic", "IsReserved", "IsSold", "Mileage", "ModelId", "Price", "Transmission", "Year" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), "Green", "M-Track Package, Carbon Seats, Laser Lights.", "Petrol", 510, "https://images.unsplash.com/photo-1617531653332-bd46c24f2068", true, false, false, 8500, new Guid("aaaa0000-0000-0000-0000-000000000001"), 145000m, "Automatic", 2022 },
                    { new Guid("c0000000-0000-0000-0000-000000000002"), "Grey", "Ceramic Brakes, RS Dynamic Plus, Bang & Olufsen.", "Petrol", 600, "https://images.unsplash.com/photo-1606152421702-427b4584554d", true, false, false, 1200, new Guid("aaaa0000-0000-0000-0000-000000000002"), 178000m, "Automatic", 2023 },
                    { new Guid("c0000000-0000-0000-0000-000000000003"), "Black", "AMG Night Package, Performance Exhaust.", "Petrol", 530, "https://images.unsplash.com/photo-1552519507-da3b142c6e3d", true, false, false, 14000, new Guid("aaaa0000-0000-0000-0000-000000000003"), 155000m, "Automatic", 2021 },
                    { new Guid("c0000000-0000-0000-0000-000000000004"), "White", "Stage 1, Akrapovic tips, Edition 35 wheels.", "Petrol", 211, "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d", false, false, false, 155000, new Guid("aaaa0000-0000-0000-0000-000000000004"), 18000m, "Manual", 2012 },
                    { new Guid("c0000000-0000-0000-0000-000000000005"), "Red", "Plaid version, Ludicrous mode, Full self-driving.", "Electric", 1020, "https://images.unsplash.com/photo-1617788138017-80ad40651399", true, false, false, 10000, new Guid("aaaa0000-0000-0000-0000-000000000005"), 95000m, "Automatic", 2022 },
                    { new Guid("c0000000-0000-0000-0000-000000000006"), "Blue", "M-Sport, Sky Lounge, Harman Kardon.", "Diesel", 400, "https://images.unsplash.com/photo-1555215695-3004980ad54e", true, false, false, 5000, new Guid("aaaa0000-0000-0000-0000-000000000006"), 110000m, "Automatic", 2023 },
                    { new Guid("c0000000-0000-0000-0000-000000000007"), "Silver", "S-line, Matrix lights, Virtual cockpit.", "Diesel", 286, "https://images.unsplash.com/photo-1605515298946-d062f2e9da53", true, false, false, 65000, new Guid("aaaa0000-0000-0000-0000-000000000007"), 55000m, "Automatic", 2020 },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), "Blue", "Brand new, 10 years warranty, Hybrid system.", "Hybrid", 140, "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb", true, false, false, 0, new Guid("aaaa0000-0000-0000-0000-000000000008"), 32000m, "Automatic", 2023 },
                    { new Guid("c0000000-0000-0000-0000-000000000009"), "Matte Black", "G63 AMG, Night Package, Carbon interior.", "Petrol", 585, "https://images.unsplash.com/photo-1520031441872-265e4ff70366", true, false, false, 12000, new Guid("aaaa0000-0000-0000-0000-000000000009"), 235000m, "Automatic", 2022 },
                    { new Guid("c0000000-0000-0000-0000-000000000010"), "LeMans Blue", "Collector's car, Perfect condition, V8 Manual.", "Petrol", 400, "https://images.unsplash.com/photo-1607853202273-797f1c22a38e", false, false, false, 180000, new Guid("aaaa0000-0000-0000-0000-000000000010"), 45000m, "Manual", 2002 },
                    { new Guid("c0000000-0000-0000-0000-000000000011"), "White", "AMG Performance, Panoramic roof, Full service history.", "Petrol", 525, "https://images.unsplash.com/photo-1511702771955-42b52e1cd168", true, false, false, 160000, new Guid("aaaa0000-0000-0000-0000-000000000011"), 38000m, "Automatic", 2014 },
                    { new Guid("c0000000-0000-0000-0000-000000000012"), "Orange", "Fox Shocks, 37 Performance Package, Off-road monster.", "Petrol", 450, "https://images.unsplash.com/photo-1591438122444-06b2c5838491", true, false, false, 2000, new Guid("aaaa0000-0000-0000-0000-000000000012"), 125000m, "Automatic", 2023 },
                    { new Guid("c0000000-0000-0000-0000-000000000013"), "Yellow", "Lamborghini Urus Performante, Titanium Exhaust.", "Petrol", 666, "https://images.unsplash.com/photo-1580273916550-e323be2ae537", true, false, false, 1500, new Guid("aaaa0000-0000-0000-0000-000000000013"), 350000m, "Automatic", 2023 },
                    { new Guid("c0000000-0000-0000-0000-000000000014"), "Sunset Orange", "Widebody, Custom wheels, Drift setup.", "Petrol", 350, "https://images.unsplash.com/photo-1616422285623-13ff0167c95c", false, false, false, 120000, new Guid("aaaa0000-0000-0000-0000-000000000014"), 25000m, "Manual", 2007 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
