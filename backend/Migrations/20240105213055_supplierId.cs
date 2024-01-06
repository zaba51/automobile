using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class supplierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Users",
                type: "integer",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 3, 8, 54, 2, 401, DateTimeKind.Utc).AddTicks(7490), new DateTime(2024, 1, 5, 8, 54, 2, 401, DateTimeKind.Utc).AddTicks(7495) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 7, 8, 54, 2, 401, DateTimeKind.Utc).AddTicks(7497), new DateTime(2024, 1, 10, 8, 54, 2, 401, DateTimeKind.Utc).AddTicks(7497) });
        }
    }
}
