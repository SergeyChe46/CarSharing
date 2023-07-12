using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSharing.Migrations
{
    public partial class Brands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrand_BrandId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarBrand",
                table: "CarBrand");

            migrationBuilder.RenameTable(
                name: "CarBrand",
                newName: "Brands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "CarBrand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarBrand",
                table: "CarBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrand_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "CarBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
