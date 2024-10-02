using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class Equity_SharekhanId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharekhanId",
                table: "EquityStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharekhanId",
                table: "EquityStocks");
        }
    }
}
