using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 24, 21, 36, 52, 651, DateTimeKind.Utc).AddTicks(8832), new DateTime(2024, 1, 26, 21, 36, 52, 651, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 28, 21, 36, 52, 651, DateTimeKind.Utc).AddTicks(8843), new DateTime(2024, 1, 31, 21, 36, 52, 651, DateTimeKind.Utc).AddTicks(8844) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 6, 21, 30, 54, 367, DateTimeKind.Utc).AddTicks(9261), new DateTime(2024, 1, 8, 21, 30, 54, 367, DateTimeKind.Utc).AddTicks(9269) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 10, 21, 30, 54, 367, DateTimeKind.Utc).AddTicks(9272), new DateTime(2024, 1, 13, 21, 30, 54, 367, DateTimeKind.Utc).AddTicks(9273) });
        }
    }
}
