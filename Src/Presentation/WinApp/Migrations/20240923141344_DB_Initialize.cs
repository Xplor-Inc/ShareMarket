using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class DB_Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquityStocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Change = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayHigh = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DayLow = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IndexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsETFSec = table.Column<bool>(type: "bit", nullable: false),
                    IsFNOSec = table.Column<bool>(type: "bit", nullable: false),
                    LTP = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PChange = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearHigh = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    YearHighOn = table.Column<DateOnly>(type: "date", nullable: false),
                    YearLow = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    YearLowOn = table.Column<DateOnly>(type: "date", nullable: false),
                    RSI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI5DMA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI5DMADiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI10DMA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI10DMADiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI15DMA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSI15DMADiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo),
                    MetaTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MetaDesc = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MetaRobots = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SchemeCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SchemeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SearchId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    GrowwRating = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquityPriceHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Avg14DaysProfit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Avg14DaysLoss = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    EquityId = table.Column<long>(type: "bigint", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Loss = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RS = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RSI = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RSI5DMA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RSI5DMADiff = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquityPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquityPriceHistories_EquityStocks_EquityId",
                        column: x => x.EquityId,
                        principalTable: "EquityStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchemeEquityHoldings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CorpusPer = table.Column<double>(type: "float", nullable: true),
                    EquityId = table.Column<long>(type: "bigint", nullable: true),
                    InstrumentName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MarketCap = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MarketValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NatureName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RatingMarketCap = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SchemeCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SectorName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SchemeId = table.Column<long>(type: "bigint", nullable: true),
                    StockSearchId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemeEquityHoldings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemeEquityHoldings_EquityStocks_EquityId",
                        column: x => x.EquityId,
                        principalTable: "EquityStocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchemeEquityHoldings_Schemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "Schemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquityPriceHistories_EquityId",
                table: "EquityPriceHistories",
                column: "EquityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeEquityHoldings_EquityId",
                table: "SchemeEquityHoldings",
                column: "EquityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemeEquityHoldings_SchemeId",
                table: "SchemeEquityHoldings",
                column: "SchemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquityPriceHistories");

            migrationBuilder.DropTable(
                name: "SchemeEquityHoldings");

            migrationBuilder.DropTable(
                name: "EquityStocks");

            migrationBuilder.DropTable(
                name: "Schemes");
        }
    }
}
