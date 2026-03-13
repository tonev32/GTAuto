using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Models_ModelID",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Models_ModelId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ModelId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ModelID",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Models",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                newName: "IX_Models_BrandID");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandID",
                table: "Models",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandID",
                table: "Models");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Models",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_BrandID",
                table: "Models",
                newName: "IX_Models_BrandId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ModelId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelID",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Models_ModelId",
                table: "Models",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ModelID",
                table: "Brands",
                column: "ModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Models_ModelID",
                table: "Brands",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Models_ModelId",
                table: "Models",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }
    }
}
