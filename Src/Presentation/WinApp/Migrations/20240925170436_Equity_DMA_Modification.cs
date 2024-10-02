using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class Equity_DMA_Modification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RSI10DMA",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSI10DMADiff",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSI15DMA",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSI15DMADiff",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "RSI10DMA",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI10DMADiff",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI5DMA",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI5DMADiff",
                table: "EquityPriceHistories");

            migrationBuilder.RenameColumn(
                name: "RSI5DMADiff",
                table: "EquityStocks",
                newName: "RSI14DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI5DMA",
                table: "EquityStocks",
                newName: "RSI14DMA");

            migrationBuilder.RenameColumn(
                name: "RSI15DMADiff",
                table: "EquityPriceHistories",
                newName: "RSI14DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI15DMA",
                table: "EquityPriceHistories",
                newName: "RSI14DMA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RSI14DMADiff",
                table: "EquityStocks",
                newName: "RSI5DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14DMA",
                table: "EquityStocks",
                newName: "RSI5DMA");

            migrationBuilder.RenameColumn(
                name: "RSI14DMADiff",
                table: "EquityPriceHistories",
                newName: "RSI15DMADiff");

            migrationBuilder.RenameColumn(
                name: "RSI14DMA",
                table: "EquityPriceHistories",
                newName: "RSI15DMA");

            migrationBuilder.AddColumn<decimal>(
                name: "RSI10DMA",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI10DMADiff",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI15DMA",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI15DMADiff",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI10DMA",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI10DMADiff",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI5DMA",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI5DMADiff",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
