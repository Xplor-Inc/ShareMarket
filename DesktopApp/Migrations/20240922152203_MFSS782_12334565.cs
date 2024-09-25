using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class MFSS782_12334565 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCASec",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "IsDebtSec",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "IsDelisted",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "IsSLBSec",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSIUpdateOn",
                table: "EquityStocks");

            migrationBuilder.RenameColumn(
                name: "LastPrice",
                table: "EquityStocks",
                newName: "RSI15DMADiff");

            migrationBuilder.RenameColumn(
                name: "IsYLAYH",
                table: "EquityStocks",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CYHYLPercent",
                table: "EquityStocks",
                newName: "RSI15DMA");

            migrationBuilder.RenameColumn(
                name: "CYHYL",
                table: "EquityStocks",
                newName: "RSI10DMADiff");

            migrationBuilder.AddColumn<decimal>(
                name: "LTP",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI10DMA",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LTP",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSI10DMA",
                table: "EquityStocks");

            migrationBuilder.RenameColumn(
                name: "RSI15DMADiff",
                table: "EquityStocks",
                newName: "LastPrice");

            migrationBuilder.RenameColumn(
                name: "RSI15DMA",
                table: "EquityStocks",
                newName: "CYHYLPercent");

            migrationBuilder.RenameColumn(
                name: "RSI10DMADiff",
                table: "EquityStocks",
                newName: "CYHYL");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "EquityStocks",
                newName: "IsYLAYH");

            migrationBuilder.AddColumn<bool>(
                name: "IsCASec",
                table: "EquityStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDebtSec",
                table: "EquityStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelisted",
                table: "EquityStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSLBSec",
                table: "EquityStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "EquityStocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdateTime",
                table: "EquityStocks",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RSIUpdateOn",
                table: "EquityStocks",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
