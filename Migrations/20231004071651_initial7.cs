using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SriTel.Migrations
{
    /// <inheritdoc />
    public partial class initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueAmount",
                table: "Payment",
                newName: "Outstanding");

            migrationBuilder.AlterColumn<double>(
                name: "TotalData",
                table: "AddOnActivation",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "DataUsage",
                table: "AddOnActivation",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Outstanding",
                table: "Payment",
                newName: "DueAmount");

            migrationBuilder.AlterColumn<float>(
                name: "TotalData",
                table: "AddOnActivation",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "DataUsage",
                table: "AddOnActivation",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
