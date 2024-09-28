using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class Equity_DMA_Calc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RSI14DMADiff",
                table: "EquityStocks",
                newName: "RSI14EMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14DMA",
                table: "EquityStocks",
                newName: "RSI14EMA");

            migrationBuilder.RenameColumn(
                name: "RSI14DMADiff",
                table: "EquityPriceHistories",
                newName: "RSI14EMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14DMA",
                table: "EquityPriceHistories",
                newName: "RSI14EMA");

            migrationBuilder.AddColumn<decimal>(
                name: "DMA10",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA100",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA20",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA200",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA5",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA50",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA10",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA100",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA20",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA200",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA5",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DMA50",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DMA10",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA100",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA20",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA200",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA5",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA50",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "DMA10",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "DMA100",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "DMA20",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "DMA200",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "DMA5",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "DMA50",
                table: "EquityPriceHistories");

            migrationBuilder.RenameColumn(
                name: "RSI14EMADiff",
                table: "EquityStocks",
                newName: "RSI14DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14EMA",
                table: "EquityStocks",
                newName: "RSI14DMA");

            migrationBuilder.RenameColumn(
                name: "RSI14EMADiff",
                table: "EquityPriceHistories",
                newName: "RSI14DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14EMA",
                table: "EquityPriceHistories",
                newName: "RSI14DMA");
        }
    }
}
