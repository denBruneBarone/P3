using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPFAdminSystem.Migrations
{
    public partial class MatchNew1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "MappingProductIdMapping",
                table: "UserActions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleGWS",
                table: "Mappings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArriveDate",
                table: "Mappings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvailableAmount",
                table: "Mappings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InHouseTitle",
                table: "Mappings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderAmount",
                table: "Mappings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OrderPrice",
                table: "Mappings",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderQuantity",
                table: "Mappings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ordered",
                table: "Mappings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedFromStockDate",
                table: "Mappings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockAmount",
                table: "Mappings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_MappingProductIdMapping",
                table: "UserActions",
                column: "MappingProductIdMapping");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_Mappings_MappingProductIdMapping",
                table: "UserActions",
                column: "MappingProductIdMapping",
                principalTable: "Mappings",
                principalColumn: "ProductIdMapping");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_Mappings_MappingProductIdMapping",
                table: "UserActions");

            migrationBuilder.DropIndex(
                name: "IX_UserActions_MappingProductIdMapping",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "MappingProductIdMapping",
                table: "UserActions");

            migrationBuilder.DropColumn(
                name: "ArriveDate",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "AvailableAmount",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "InHouseTitle",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "OrderAmount",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "OrderQuantity",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "Ordered",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "RemovedFromStockDate",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "StockAmount",
                table: "Mappings");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TitleGWS",
                table: "Mappings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
