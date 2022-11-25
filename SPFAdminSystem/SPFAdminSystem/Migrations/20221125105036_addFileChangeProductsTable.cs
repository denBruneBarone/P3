using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class addFileChangeProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UserActions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FileChangeProducts",
                columns: table => new
                {
                    FileChangeProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<string>(type: "TEXT", nullable: true),
                    UserActionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileChangeProducts", x => x.FileChangeProductId);
                    table.ForeignKey(
                        name: "FK_FileChangeProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_FileChangeProducts_UserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "UserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileChangeProducts_ProductId",
                table: "FileChangeProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FileChangeProducts_UserActionId",
                table: "FileChangeProducts",
                column: "UserActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileChangeProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "UserActions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
