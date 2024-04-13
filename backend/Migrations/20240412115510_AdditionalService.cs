using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    public partial class AdditionalService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceCategory = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalServiceReservation",
                columns: table => new
                {
                    AdditionalServicesId = table.Column<int>(type: "integer", nullable: false),
                    ReservationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceReservation", x => new { x.AdditionalServicesId, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_AdditionalServiceReservation_AdditionalServices_AdditionalS~",
                        column: x => x.AdditionalServicesId,
                        principalTable: "AdditionalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceReservation_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdditionalServices",
                columns: new[] { "Id", "Description", "Name", "Price", "ServiceCategory" },
                values: new object[,]
                {
                    { 1, "", "Additional Insurance", 15.0, "INSURANCE" },
                    { 2, "", "Bike holder", 10.0, "BIKE_HOLDER" },
                    { 3, "", "Child seat", 15.0, "CHILD_SEAT" },
                    { 4, "", "Animal Carrier", 7.0, "ANIMAL_CARRIER" }
                });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 4, 13, 11, 55, 9, 104, DateTimeKind.Utc).AddTicks(9327), new DateTime(2024, 4, 15, 11, 55, 9, 104, DateTimeKind.Utc).AddTicks(9338) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 4, 17, 11, 55, 9, 104, DateTimeKind.Utc).AddTicks(9342), new DateTime(2024, 4, 20, 11, 55, 9, 104, DateTimeKind.Utc).AddTicks(9344) });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceReservation_ReservationsId",
                table: "AdditionalServiceReservation",
                column: "ReservationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceReservation");

            migrationBuilder.DropTable(
                name: "AdditionalServices");

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
        }
    }
}
