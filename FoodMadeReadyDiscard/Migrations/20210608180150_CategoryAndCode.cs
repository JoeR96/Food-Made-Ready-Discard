using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodMadeReadyDiscard.Migrations
{
    public partial class CategoryAndCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCode",
                table: "Foods",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Foods");
        }
    }
}
