using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class StockEquity_LTPDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "LTPDate",
                table: "EquityStocks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LTPDate",
                table: "EquityStocks");
        }
    }
}
