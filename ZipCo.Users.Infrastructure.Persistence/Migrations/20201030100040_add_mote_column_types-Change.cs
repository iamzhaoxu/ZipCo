using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZipCo.Users.Infrastructure.Persistence.Migrations
{
    public partial class add_mote_column_typesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Frequency",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthNetIncomeLimit",
                table: "AccountSignUpStrategy",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountSignUpStrategy",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2020, 10, 30, 10, 0, 40, 353, DateTimeKind.Utc).AddTicks(7129), new DateTime(2020, 10, 30, 10, 0, 40, 353, DateTimeKind.Utc).AddTicks(7457) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Frequency",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthNetIncomeLimit",
                table: "AccountSignUpStrategy",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountSignUpStrategy",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2020, 10, 30, 4, 16, 36, 727, DateTimeKind.Utc).AddTicks(8849), new DateTime(2020, 10, 30, 4, 16, 36, 727, DateTimeKind.Utc).AddTicks(9927) });
        }
    }
}
