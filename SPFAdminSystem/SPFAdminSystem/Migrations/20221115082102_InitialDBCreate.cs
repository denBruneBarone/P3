using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    InHouseProductId = table.Column<string>(type: "TEXT", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RemovedFromStockDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InHouseTitle = table.Column<string>(type: "TEXT", nullable: false),
                    TitleGWS = table.Column<string>(type: "TEXT", nullable: false),
                    OrderPrice = table.Column<double>(type: "REAL", nullable: false),
                    InStock = table.Column<int>(type: "INTEGER", nullable: false),
                    InOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Available = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordered = table.Column<int>(type: "INTEGER", nullable: false),
                    Barcode = table.Column<int>(type: "INTEGER", nullable: false),
                    PackSize = table.Column<int>(type: "INTEGER", nullable: false),
                    Target = table.Column<int>(type: "INTEGER", nullable: false),
                    MinOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.InHouseProductId);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionType = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    AffectedProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    AffectedUser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegisterDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
