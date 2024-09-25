using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialazation_BOHunting_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CYHYL",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CYHYLPercent",
                table: "EquityStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CYHYL",
                table: "EquityStocks");

            migrationBuilder.DropColumn(
                name: "CYHYLPercent",
                table: "EquityStocks");
        }
    }
}
