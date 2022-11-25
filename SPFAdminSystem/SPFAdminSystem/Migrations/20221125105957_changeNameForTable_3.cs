using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class changeNameForTable_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadFileChangeProducts");

            migrationBuilder.CreateTable(
                name: "UploadMappingFileChangeProducts",
                columns: table => new
                {
                    FileChangeProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    UserActionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadMappingFileChangeProducts", x => x.FileChangeProductId);
                    table.ForeignKey(
                        name: "FK_UploadMappingFileChangeProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadMappingFileChangeProducts_UserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "UserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadMappingFileChangeProducts_ProductId",
                table: "UploadMappingFileChangeProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadMappingFileChangeProducts_UserActionId",
                table: "UploadMappingFileChangeProducts",
                column: "UserActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadMappingFileChangeProducts");

            migrationBuilder.CreateTable(
                name: "UploadFileChangeProducts",
                columns: table => new
                {
                    FileChangeProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    UserActionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFileChangeProducts", x => x.FileChangeProductId);
                    table.ForeignKey(
                        name: "FK_UploadFileChangeProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadFileChangeProducts_UserActions_UserActionId",
                        column: x => x.UserActionId,
                        principalTable: "UserActions",
                        principalColumn: "UserActionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadFileChangeProducts_ProductId",
                table: "UploadFileChangeProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFileChangeProducts_UserActionId",
                table: "UploadFileChangeProducts",
                column: "UserActionId");
        }
    }
}
