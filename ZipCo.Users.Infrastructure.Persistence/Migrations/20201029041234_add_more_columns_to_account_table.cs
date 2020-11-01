using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZipCo.Users.Infrastructure.Persistence.Migrations
{
    public partial class add_more_columns_to_account_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_ACCOUNT_NUMBER",
                startValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "MemberSalary",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "MemberSalary",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "PayFrequencyId",
                table: "MemberSalary",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "MemberExpense",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "MemberExpense",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "BillFrequencyId",
                table: "MemberExpense",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Member",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Member",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountStatus",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Account",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Account",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Account",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<decimal>(
                name: "AccountBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingBalance",
                table: "Account",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AccountSignUpStrategy",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ModifiedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MonthNetIncomeLimit = table.Column<decimal>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSignUpStrategy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AccountSignUpStrategy",
                columns: new[] { "Id", "CreatedOn", "IsDefault", "ModifiedOn", "MonthNetIncomeLimit", "Name" },
                values: new object[] { 1L, new DateTime(2020, 10, 29, 4, 12, 33, 888, DateTimeKind.Utc).AddTicks(5179), true, new DateTime(2020, 10, 29, 4, 12, 33, 888, DateTimeKind.Utc).AddTicks(5712), 1000m, "Premium member program" });

            migrationBuilder.InsertData(
                table: "Frequency",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Month" });

            migrationBuilder.InsertData(
                table: "Frequency",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Annual" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSalary_PayFrequencyId",
                table: "MemberSalary",
                column: "PayFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberExpense_BillFrequencyId",
                table: "MemberExpense",
                column: "BillFrequencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberExpense_Frequency_BillFrequencyId",
                table: "MemberExpense",
                column: "BillFrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSalary_Frequency_PayFrequencyId",
                table: "MemberSalary",
                column: "PayFrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberExpense_Frequency_BillFrequencyId",
                table: "MemberExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSalary_Frequency_PayFrequencyId",
                table: "MemberSalary");

            migrationBuilder.DropTable(
                name: "AccountSignUpStrategy");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropIndex(
                name: "IX_MemberSalary_PayFrequencyId",
                table: "MemberSalary");

            migrationBuilder.DropIndex(
                name: "IX_MemberExpense_BillFrequencyId",
                table: "MemberExpense");

            migrationBuilder.DropSequence(
                name: "SEQ_ACCOUNT_NUMBER");

            migrationBuilder.DropColumn(
                name: "PayFrequencyId",
                table: "MemberSalary");

            migrationBuilder.DropColumn(
                name: "BillFrequencyId",
                table: "MemberExpense");

            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "PendingBalance",
                table: "Account");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "MemberSalary",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "MemberSalary",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "MemberExpense",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "MemberExpense",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Member",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Member",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountStatus",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOn",
                table: "Account",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Account",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "Account",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
