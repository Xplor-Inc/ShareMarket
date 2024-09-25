using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialazation_BOHunting_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLastLowOfYear",
                table: "EquityStocks",
                newName: "IsYLAYH");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "YearLowOn",
                table: "EquityStocks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "YearHighOn",
                table: "EquityStocks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsYLAYH",
                table: "EquityStocks",
                newName: "IsLastLowOfYear");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "YearLowOn",
                table: "EquityStocks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "YearHighOn",
                table: "EquityStocks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
