using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class MFSS78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MFSchemes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo),
                    NfoRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaRobots = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrowwRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrisilRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MFSchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockHoldings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchemeId = table.Column<long>(type: "bigint", nullable: false),
                    SchemeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorpusPer = table.Column<double>(type: "float", nullable: true),
                    MarketCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatingMarketCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockSearchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHoldings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHoldings_MFSchemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "MFSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockHoldings_SchemeId",
                table: "StockHoldings",
                column: "SchemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHoldings");

            migrationBuilder.DropTable(
                name: "MFSchemes");
        }
    }
}
