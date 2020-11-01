using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZipCo.Users.Infrastructure.Persistence.Migrations
{
    public partial class EnableAutoPopulateCreatedOnAndModifiedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountSignUpStrategy",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2020, 10, 31, 8, 24, 9, 466, DateTimeKind.Utc).AddTicks(3731));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccountSignUpStrategy",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2020, 10, 30, 10, 0, 40, 353, DateTimeKind.Utc).AddTicks(7129), new DateTime(2020, 10, 30, 10, 0, 40, 353, DateTimeKind.Utc).AddTicks(7457) });
        }
    }
}
