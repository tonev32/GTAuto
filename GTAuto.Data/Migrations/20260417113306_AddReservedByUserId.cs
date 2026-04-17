using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservedByUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservedByUserId",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000001"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000002"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000003"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000004"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000005"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000006"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000007"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000008"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000009"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000010"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000011"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000012"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000013"),
                column: "ReservedByUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000014"),
                column: "ReservedByUserId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedByUserId",
                table: "Cars");
        }
    }
}
