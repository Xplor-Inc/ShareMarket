using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class MFSS782_123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstrumentName",
                table: "EquityStocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectorName",
                table: "EquityStocks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstrumentName",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "SectorName",
                table: "EquityStocks");
        }
    }
}
