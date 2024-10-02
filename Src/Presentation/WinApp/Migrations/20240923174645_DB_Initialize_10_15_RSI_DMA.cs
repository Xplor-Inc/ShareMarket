using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialize_10_15_RSI_DMA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "RSI15DMA",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RSI15DMADiff",
                table: "EquityPriceHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RSI10DMA",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI10DMADiff",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI15DMA",
                table: "EquityPriceHistories");

            migrationBuilder.DropColumn(
                name: "RSI15DMADiff",
                table: "EquityPriceHistories");
        }
    }
}
