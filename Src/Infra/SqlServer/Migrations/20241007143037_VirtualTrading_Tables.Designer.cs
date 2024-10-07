﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareMarket.SqlServer;

#nullable disable

namespace ShareMarket.WinApp.Migrations
{
    [DbContext(typeof(ShareMarketContext))]
    [Migration("20241007143037_VirtualTrading_Tables")]
    partial class VirtualTrading_Tables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShareMarket.Core.Entities.Audits.ChangeLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NewValue")
                        .HasMaxLength(2056)
                        .HasColumnType("nvarchar(2056)");

                    b.Property<string>("OldValue")
                        .HasMaxLength(2056)
                        .HasColumnType("nvarchar(2056)");

                    b.Property<long>("PrimaryKey")
                        .HasColumnType("bigint");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ChangeLogs", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Audits.EmailAuditLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Attachments")
                        .HasMaxLength(2056)
                        .HasColumnType("nvarchar(2056)");

                    b.Property<string>("CCEmails")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Error")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("HeaderText")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.Property<string>("ToEmails")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("EmailAuditLogs", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Audits.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsLoginSuccess")
                        .HasColumnType("bit");

                    b.Property<bool>("IsValidUser")
                        .HasColumnType("bit");

                    b.Property<string>("OperatingSystem")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Equities.EquityHistorySyncLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("EquityHistorySyncLog");
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Equities.EquityPriceHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Avg14DaysLoss")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Avg14DaysProfit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Close")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("DMA10")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA100")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA20")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA200")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA5")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA50")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("EquityId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("High")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Loss")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Low")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Open")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Profit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RS")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI14EMA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI14EMADiff")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EquityId");

                    b.ToTable("EquityPriceHistories", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Equities.EquityStock", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("BookValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Change")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("DMA10")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA100")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA20")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA200")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA5")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DMA50")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DayHigh")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DayLow")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("DebtEquity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Dividend")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EPS")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FaceValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IndexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstrumentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsETFSec")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFNOSec")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRaising")
                        .HasColumnType("bit");

                    b.Property<decimal>("LTP")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("LTPDate")
                        .HasColumnType("date");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PChange")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ROE")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI14EMA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RSI14EMADiff")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RankByGroww")
                        .HasColumnType("int");

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SharekhanId")
                        .HasColumnType("int");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("YearHigh")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("YearHighOn")
                        .HasColumnType("date");

                    b.Property<decimal>("YearLow")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("YearLowOn")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("EquityStocks", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Schemes.Scheme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("GrowwRating")
                        .HasColumnType("int");

                    b.Property<string>("MetaDesc")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MetaRobots")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SchemeCode")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SchemeName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SearchId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Schemes", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Schemes.SchemeEquityHolding", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double?>("CorpusPer")
                        .HasColumnType("float");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("EquityId")
                        .HasColumnType("bigint");

                    b.Property<string>("InstrumentName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MarketCap")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MarketValue")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NatureName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Rating")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RatingMarketCap")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SchemeCode")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("SchemeId")
                        .HasColumnType("bigint");

                    b.Property<string>("SectorName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("StockSearchId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EquityId");

                    b.HasIndex("SchemeId");

                    b.ToTable("SchemeEquityHoldings", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Tradings.VirtualTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("BuyDate")
                        .HasColumnType("date");

                    b.Property<decimal>("BuyRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BuyValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("EquityId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Holding")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LTP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("ReleasedPL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SellAction")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("SellDate")
                        .HasColumnType("date");

                    b.Property<decimal>("SellRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SellValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StopLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Stratergy")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Target")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TargetPer")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EquityId");

                    b.ToTable("VirtualTradings", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("nvarchar(28)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastLoginDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("nvarchar(28)");

                    b.Property<DateTimeOffset?>("PasswordChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress", "DeletedOn")
                        .IsUnique()
                        .HasFilter("[DeletedOn] IS NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Audits.UserLogin", b =>
                {
                    b.HasOne("ShareMarket.Core.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Equities.EquityPriceHistory", b =>
                {
                    b.HasOne("ShareMarket.Core.Entities.Equities.EquityStock", "Equity")
                        .WithMany("PriceHistories")
                        .HasForeignKey("EquityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Equity");
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Schemes.SchemeEquityHolding", b =>
                {
                    b.HasOne("ShareMarket.Core.Entities.Equities.EquityStock", "Equity")
                        .WithMany()
                        .HasForeignKey("EquityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ShareMarket.Core.Entities.Schemes.Scheme", "Scheme")
                        .WithMany()
                        .HasForeignKey("SchemeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Equity");

                    b.Navigation("Scheme");
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Tradings.VirtualTrade", b =>
                {
                    b.HasOne("ShareMarket.Core.Entities.Equities.EquityStock", "Equity")
                        .WithMany()
                        .HasForeignKey("EquityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Equity");
                });

            modelBuilder.Entity("ShareMarket.Core.Entities.Equities.EquityStock", b =>
                {
                    b.Navigation("PriceHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
