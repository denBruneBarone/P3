using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class AddedAttributesFromMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinOrder",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Packsize",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Products",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Packsize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Products");
        }
    }
}
