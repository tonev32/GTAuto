using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GTAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedDeterministicImageGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "CarImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarImages_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepositPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarImages",
                columns: new[] { "Id", "CarId", "ImagePath", "Order" },
                values: new object[,]
                {
                    { new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("c0000000-0000-0000-0000-000000000001"), "/images/cars/m4-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000002"), new Guid("c0000000-0000-0000-0000-000000000001"), "/images/cars/m4-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000003"), new Guid("c0000000-0000-0000-0000-000000000001"), "/images/cars/m4-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000004"), new Guid("c0000000-0000-0000-0000-000000000002"), "/images/cars/rs7-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000005"), new Guid("c0000000-0000-0000-0000-000000000002"), "/images/cars/rs7-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000006"), new Guid("c0000000-0000-0000-0000-000000000002"), "/images/cars/rs7-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000007"), new Guid("c0000000-0000-0000-0000-000000000003"), "/images/cars/gt63-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000008"), new Guid("c0000000-0000-0000-0000-000000000003"), "/images/cars/gt63-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000009"), new Guid("c0000000-0000-0000-0000-000000000003"), "/images/cars/gt63-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000010"), new Guid("c0000000-0000-0000-0000-000000000004"), "/images/cars/golf6-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000011"), new Guid("c0000000-0000-0000-0000-000000000004"), "/images/cars/golf6-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000012"), new Guid("c0000000-0000-0000-0000-000000000004"), "/images/cars/golf6-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000013"), new Guid("c0000000-0000-0000-0000-000000000005"), "/images/cars/tesla-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000014"), new Guid("c0000000-0000-0000-0000-000000000005"), "/images/cars/tesla-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000015"), new Guid("c0000000-0000-0000-0000-000000000005"), "/images/cars/tesla-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000016"), new Guid("c0000000-0000-0000-0000-000000000006"), "/images/cars/x5-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000017"), new Guid("c0000000-0000-0000-0000-000000000006"), "/images/cars/x5-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000018"), new Guid("c0000000-0000-0000-0000-000000000006"), "/images/cars/x5-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000019"), new Guid("c0000000-0000-0000-0000-000000000007"), "/images/cars/a6-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000020"), new Guid("c0000000-0000-0000-0000-000000000007"), "/images/cars/a6-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000021"), new Guid("c0000000-0000-0000-0000-000000000007"), "/images/cars/a6-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000022"), new Guid("c0000000-0000-0000-0000-000000000008"), "/images/cars/toyota-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000023"), new Guid("c0000000-0000-0000-0000-000000000008"), "/images/cars/toyota-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000024"), new Guid("c0000000-0000-0000-0000-000000000008"), "/images/cars/toyota-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000025"), new Guid("c0000000-0000-0000-0000-000000000009"), "/images/cars/gclass-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000026"), new Guid("c0000000-0000-0000-0000-000000000009"), "/images/cars/gclass-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000027"), new Guid("c0000000-0000-0000-0000-000000000009"), "/images/cars/gclass-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000028"), new Guid("c0000000-0000-0000-0000-000000000010"), "/images/cars/e39-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000029"), new Guid("c0000000-0000-0000-0000-000000000010"), "/images/cars/e39-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000030"), new Guid("c0000000-0000-0000-0000-000000000010"), "/images/cars/e39-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000031"), new Guid("c0000000-0000-0000-0000-000000000011"), "/images/cars/ml63-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000032"), new Guid("c0000000-0000-0000-0000-000000000011"), "/images/cars/ml63-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000033"), new Guid("c0000000-0000-0000-0000-000000000011"), "/images/cars/ml63-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000034"), new Guid("c0000000-0000-0000-0000-000000000012"), "/images/cars/f150-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000035"), new Guid("c0000000-0000-0000-0000-000000000012"), "/images/cars/f150-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000036"), new Guid("c0000000-0000-0000-0000-000000000012"), "/images/cars/f150-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000037"), new Guid("c0000000-0000-0000-0000-000000000013"), "/images/cars/urus-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000038"), new Guid("c0000000-0000-0000-0000-000000000013"), "/images/cars/urus-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000039"), new Guid("c0000000-0000-0000-0000-000000000013"), "/images/cars/urus-interior.jpg", 3 },
                    { new Guid("f0000000-0000-0000-0000-000000000040"), new Guid("c0000000-0000-0000-0000-000000000014"), "/images/cars/350z-front.jpg", 1 },
                    { new Guid("f0000000-0000-0000-0000-000000000041"), new Guid("c0000000-0000-0000-0000-000000000014"), "/images/cars/350z-back.jpg", 2 },
                    { new Guid("f0000000-0000-0000-0000-000000000042"), new Guid("c0000000-0000-0000-0000-000000000014"), "/images/cars/350z-interior.jpg", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarImages_CarId",
                table: "CarImages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarImages");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000001"),
                column: "ImageUrl",
                value: "/images/m4.jpg");

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
                column: "ImageUrl",
                value: "/images/e39.jpg");

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
    }
}
