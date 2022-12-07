using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class InitialDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RemovedFromStockDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    InHouseTitle = table.Column<string>(type: "TEXT", nullable: false),
                    TitleGWS = table.Column<string>(type: "TEXT", nullable: true),
                    OrderPrice = table.Column<double>(type: "REAL", nullable: true),
                    StockAmount = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderAmount = table.Column<int>(type: "INTEGER", nullable: true),
                    AvailableAmount = table.Column<int>(type: "INTEGER", nullable: true),
                    Ordered = table.Column<int>(type: "INTEGER", nullable: true),
                    Barcode = table.Column<int>(type: "INTEGER", nullable: true),
                    PackSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Target = table.Column<int>(type: "INTEGER", nullable: true),
                    MinOrder = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderQuantity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    UserActionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionType = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.UserActionId);
                    table.ForeignKey(
                        name: "FK_UserActions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_ProductId",
                table: "UserActions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_UserId",
                table: "UserActions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
