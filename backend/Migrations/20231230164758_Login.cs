using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class Login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2023, 12, 31, 16, 47, 57, 928, DateTimeKind.Utc).AddTicks(7614), new DateTime(2024, 1, 2, 16, 47, 57, 928, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 4, 16, 47, 57, 928, DateTimeKind.Utc).AddTicks(7622), new DateTime(2024, 1, 7, 16, 47, 57, 928, DateTimeKind.Utc).AddTicks(7622) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role" },
                values: new object[] { "pass1", "user" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Role" },
                values: new object[] { "pass1", "Supplier" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2023, 12, 31, 10, 32, 24, 832, DateTimeKind.Utc).AddTicks(2816), new DateTime(2024, 1, 2, 10, 32, 24, 832, DateTimeKind.Utc).AddTicks(2827) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 4, 10, 32, 24, 832, DateTimeKind.Utc).AddTicks(2829), new DateTime(2024, 1, 7, 10, 32, 24, 832, DateTimeKind.Utc).AddTicks(2830) });
        }
    }
}
