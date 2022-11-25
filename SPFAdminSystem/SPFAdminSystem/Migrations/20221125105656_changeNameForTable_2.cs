using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class changeNameForTable_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileChangeProducts_Products_ProductId",
                table: "FileChangeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_FileChangeProducts_UserActions_UserActionId",
                table: "FileChangeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileChangeProducts",
                table: "FileChangeProducts");

            migrationBuilder.RenameTable(
                name: "FileChangeProducts",
                newName: "UploadFileChangeProducts");

            migrationBuilder.RenameIndex(
                name: "IX_FileChangeProducts_UserActionId",
                table: "UploadFileChangeProducts",
                newName: "IX_UploadFileChangeProducts_UserActionId");

            migrationBuilder.RenameIndex(
                name: "IX_FileChangeProducts_ProductId",
                table: "UploadFileChangeProducts",
                newName: "IX_UploadFileChangeProducts_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "UploadFileChangeProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadFileChangeProducts",
                table: "UploadFileChangeProducts",
                column: "FileChangeProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadFileChangeProducts_Products_ProductId",
                table: "UploadFileChangeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadFileChangeProducts_UserActions_UserActionId",
                table: "UploadFileChangeProducts",
                column: "UserActionId",
                principalTable: "UserActions",
                principalColumn: "UserActionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadFileChangeProducts_Products_ProductId",
                table: "UploadFileChangeProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadFileChangeProducts_UserActions_UserActionId",
                table: "UploadFileChangeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadFileChangeProducts",
                table: "UploadFileChangeProducts");

            migrationBuilder.RenameTable(
                name: "UploadFileChangeProducts",
                newName: "FileChangeProducts");

            migrationBuilder.RenameIndex(
                name: "IX_UploadFileChangeProducts_UserActionId",
                table: "FileChangeProducts",
                newName: "IX_FileChangeProducts_UserActionId");

            migrationBuilder.RenameIndex(
                name: "IX_UploadFileChangeProducts_ProductId",
                table: "FileChangeProducts",
                newName: "IX_FileChangeProducts_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "FileChangeProducts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileChangeProducts",
                table: "FileChangeProducts",
                column: "FileChangeProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileChangeProducts_Products_ProductId",
                table: "FileChangeProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileChangeProducts_UserActions_UserActionId",
                table: "FileChangeProducts",
                column: "UserActionId",
                principalTable: "UserActions",
                principalColumn: "UserActionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
