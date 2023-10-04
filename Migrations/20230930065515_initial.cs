using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SriTel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddOnActivation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DataServiceId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AddOnId = table.Column<long>(type: "bigint", nullable: false),
                    ActivatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUsage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOnActivation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataService",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsDataRoaming = table.Column<int>(type: "integer", nullable: false),
                    DataRoamingCharge = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataService", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Renewal = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Charge = table.Column<float>(type: "real", nullable: false),
                    OffPeekData = table.Column<float>(type: "real", nullable: false),
                    PeekData = table.Column<float>(type: "real", nullable: false),
                    AnytimeData = table.Column<float>(type: "real", nullable: false),
                    S2SCallMins = table.Column<int>(type: "integer", nullable: false),
                    S2SSmsCount = table.Column<int>(type: "integer", nullable: false),
                    AnyNetCallMins = table.Column<int>(type: "integer", nullable: false),
                    AnyNetSmsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageUsage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    PackageId = table.Column<long>(type: "bigint", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OffPeekDataUsage = table.Column<float>(type: "real", nullable: false),
                    PeekDataUsage = table.Column<float>(type: "real", nullable: false),
                    AnytimeDataUsage = table.Column<float>(type: "real", nullable: false),
                    S2SCallMinsUsage = table.Column<int>(type: "integer", nullable: false),
                    S2SSmsCountUsage = table.Column<int>(type: "integer", nullable: false),
                    AnyNetCallMinsUsage = table.Column<int>(type: "integer", nullable: false),
                    AnyNetSmsCountUsage = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUsage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BillId = table.Column<long>(type: "bigint", nullable: false),
                    PayDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    PayMethod = table.Column<string>(type: "text", nullable: false),
                    PayAmount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoiceService",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsRingingTone = table.Column<int>(type: "integer", nullable: false),
                    RingingToneName = table.Column<string>(type: "text", nullable: false),
                    RingingToneCharge = table.Column<float>(type: "real", nullable: false),
                    IsVoiceRoaming = table.Column<int>(type: "integer", nullable: false),
                    VoiceRoamingCharge = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceService", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AddOn",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ValidNoOfDays = table.Column<int>(type: "integer", nullable: false),
                    ChargePerGb = table.Column<float>(type: "real", nullable: false),
                    DataAmount = table.Column<float>(type: "real", nullable: false),
                    AddOnId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddOn_AddOnActivation_AddOnId",
                        column: x => x.AddOnId,
                        principalTable: "AddOnActivation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    TaxAmount = table.Column<float>(type: "real", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    DueAmount = table.Column<float>(type: "real", nullable: false),
                    BillId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Payment_BillId",
                        column: x => x.BillId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Charge = table.Column<float>(type: "real", nullable: false),
                    State = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DataServiceId = table.Column<long>(type: "bigint", nullable: true),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_AddOnActivation_DataServiceId",
                        column: x => x.DataServiceId,
                        principalTable: "AddOnActivation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Bill_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Bill",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_PackageUsage_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "PackageUsage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Service_Payment_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nic = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PastPassword = table.Column<string[]>(type: "text[]", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_AddOnActivation_UserId",
                        column: x => x.UserId,
                        principalTable: "AddOnActivation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Bill_UserId",
                        column: x => x.UserId,
                        principalTable: "Bill",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_DataService_UserId",
                        column: x => x.UserId,
                        principalTable: "DataService",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_User_Notification_UserId",
                        column: x => x.UserId,
                        principalTable: "Notification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_PackageUsage_UserId",
                        column: x => x.UserId,
                        principalTable: "PackageUsage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Payment_UserId",
                        column: x => x.UserId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_VoiceService_UserId",
                        column: x => x.UserId,
                        principalTable: "VoiceService",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddOn_AddOnId",
                table: "AddOn",
                column: "AddOnId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_BillId",
                table: "Bill",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_DataServiceId",
                table: "Service",
                column: "DataServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceId",
                table: "Service",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId",
                table: "User",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddOn");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AddOnActivation");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "DataService");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PackageUsage");

            migrationBuilder.DropTable(
                name: "VoiceService");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
