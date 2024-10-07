using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    /// <inheritdoc />
    public partial class VirtualTrading_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VirtualTradings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BuyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EquityId = table.Column<long>(type: "bigint", nullable: false),
                    Holding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LTP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ReleasedPL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellAction = table.Column<int>(type: "int", nullable: false),
                    SellDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SellRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StopLoss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stratergy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Target = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedById = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedById = table.Column<long>(type: "bigint", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualTradings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualTradings_EquityStocks_EquityId",
                        column: x => x.EquityId,
                        principalTable: "EquityStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualTradings_EquityId",
                table: "VirtualTradings",
                column: "EquityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualTradings");
        }
    }
}
