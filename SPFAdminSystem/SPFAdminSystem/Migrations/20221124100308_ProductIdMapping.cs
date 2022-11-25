using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class ProductIdMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mappings_Products_ProductId",
                table: "Mappings");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Mappings",
                newName: "ProductIdMapping");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductIdMapping",
                table: "Mappings",
                newName: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mappings_Products_ProductId",
                table: "Mappings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
