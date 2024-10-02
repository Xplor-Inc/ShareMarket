using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class StockEquity_Fundamental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BookValue",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DebtEquity",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dividend",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EPS",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FaceValue",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketCap",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PD",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PE",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ROE",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookValue",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DebtEquity",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "Dividend",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "EPS",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "FaceValue",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "PD",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "PE",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "ROE",
                table: "EquityStocks");
        }
    }
}
