using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoesStoreApi.Migrations.ShoesStoreApi
{
    public partial class Rename_amount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
