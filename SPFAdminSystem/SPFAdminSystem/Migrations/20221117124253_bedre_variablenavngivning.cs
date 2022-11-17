using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    /// <inheritdoc />
    public partial class bedrevariablenavngivning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InStock",
                table: "Products",
                newName: "StockAmount");

            migrationBuilder.RenameColumn(
                name: "InOrder",
                table: "Products",
                newName: "OrderAmount");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Products",
                newName: "AvailableAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockAmount",
                table: "Products",
                newName: "InStock");

            migrationBuilder.RenameColumn(
                name: "OrderAmount",
                table: "Products",
                newName: "InOrder");

            migrationBuilder.RenameColumn(
                name: "AvailableAmount",
                table: "Products",
                newName: "Available");
        }
    }
}
