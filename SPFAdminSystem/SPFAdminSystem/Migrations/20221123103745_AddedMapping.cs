using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class AddedMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackSize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TitleGWS",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Mappings",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", nullable: true),
                    TitleGWS = table.Column<string>(type: "TEXT", nullable: false),
                    PackSize = table.Column<int>(type: "INTEGER", nullable: true),
                    MinOrder = table.Column<int>(type: "INTEGER", nullable: true),
                    Target = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mappings", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mappings");

            migrationBuilder.AddColumn<int>(
                name: "Barcode",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinOrder",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackSize",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleGWS",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }
    }
}
