using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 1, 13, 45, 52, 630, DateTimeKind.Utc).AddTicks(1793), new DateTime(2024, 1, 3, 13, 45, 52, 630, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 5, 13, 45, 52, 630, DateTimeKind.Utc).AddTicks(1822), new DateTime(2024, 1, 8, 13, 45, 52, 630, DateTimeKind.Utc).AddTicks(1822) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: "supplier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
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
                column: "Name",
                value: "Alice");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "Role" },
                values: new object[] { "Bob", "Supplier" });
        }
    }
}
