using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocalImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "M-Track Package, Carbon Seats, Laser Lights, Like new!", "/images/m4.jpg" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "/images/rs7.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "/images/gt63.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "/images/golf6.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "/images/tesla.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "/images/x5.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "/images/a6.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "/images/toyota.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "/images/gclass.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000010"),
                columns: new[] { "Color", "ImageUrl" },
                values: new object[] { "Red", "/images/e39.jpg" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000011"),
                column: "ImageUrl",
                value: "/images/ml63.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000012"),
                column: "ImageUrl",
                value: "/images/f150.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000013"),
                column: "ImageUrl",
                value: "/images/urus.jpg");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000014"),
                column: "ImageUrl",
                value: "/images/350z.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "M-Track Package, Carbon Seats, Laser Lights.", "https://images.unsplash.com/photo-1617531653332-bd46c24f2068" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000002"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1606152421702-427b4584554d");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000003"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1552519507-da3b142c6e3d");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000004"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000005"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1617788138017-80ad40651399");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000006"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1555215695-3004980ad54e");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000007"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1605515298946-d062f2e9da53");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000008"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1621007947382-bb3c3994e3fb");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000009"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1520031441872-265e4ff70366");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000010"),
                columns: new[] { "Color", "ImageUrl" },
                values: new object[] { "LeMans Blue", "https://images.unsplash.com/photo-1607853202273-797f1c22a38e" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000011"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1511702771955-42b52e1cd168");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000012"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1591438122444-06b2c5838491");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000013"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1580273916550-e323be2ae537");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000014"),
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1616422285623-13ff0167c95c");
        }
    }
}
