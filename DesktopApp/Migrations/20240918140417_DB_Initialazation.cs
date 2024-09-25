using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialazation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avg14DaysProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Avg14DaysLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Loss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquityStocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCASec = table.Column<bool>(type: "bit", nullable: false),
                    IsDebtSec = table.Column<bool>(type: "bit", nullable: false),
                    IsDelisted = table.Column<bool>(type: "bit", nullable: false),
                    IsETFSec = table.Column<bool>(type: "bit", nullable: false),
                    IsFNOSec = table.Column<bool>(type: "bit", nullable: false),
                    IsSLBSec = table.Column<bool>(type: "bit", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PChange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerChange365d = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerChange30d = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSIUpdateOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    YearHigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearLow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquityStocks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyHistories");

            migrationBuilder.DropTable(
                name: "EquityStocks");
        }
    }
}
