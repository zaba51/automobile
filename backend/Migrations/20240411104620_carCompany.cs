using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    public partial class carCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Models");

            migrationBuilder.AddColumn<int>(
                name: "CarCompanyId",
                table: "Models",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "CarCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "FIAT", "Fiat" },
                    { 2, "TOYOTA", "Toyota" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 4, 12, 10, 46, 19, 855, DateTimeKind.Utc).AddTicks(1484), new DateTime(2024, 4, 14, 10, 46, 19, 855, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 4, 16, 10, 46, 19, 855, DateTimeKind.Utc).AddTicks(1491), new DateTime(2024, 4, 19, 10, 46, 19, 855, DateTimeKind.Utc).AddTicks(1492) });

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CarCompanyId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CarCompanyId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CarCompanyId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Models_CarCompanyId",
                table: "Models",
                column: "CarCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_CarCompanies_CarCompanyId",
                table: "Models",
                column: "CarCompanyId",
                principalTable: "CarCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_CarCompanies_CarCompanyId",
                table: "Models");

            migrationBuilder.DropTable(
                name: "CarCompanies");

            migrationBuilder.DropIndex(
                name: "IX_Models_CarCompanyId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "CarCompanyId",
                table: "Models");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "Company",
                value: "FIAT");

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "Company",
                value: "TOYOTA");

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "Company",
                value: "TOYOTA");

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
    }
}
