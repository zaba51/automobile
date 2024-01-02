using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    public partial class location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "CatalogItems",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationSupplier",
                columns: table => new
                {
                    LocationsId = table.Column<int>(type: "integer", nullable: false),
                    SuppliersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSupplier", x => new { x.LocationsId, x.SuppliersId });
                    table.ForeignKey(
                        name: "FK_LocationSupplier_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationSupplier_Suppliers_SuppliersId",
                        column: x => x.SuppliersId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CityName", "CountryName" },
                values: new object[,]
                {
                    { 1, "Warsaw", "Poland" },
                    { 2, "Prague", "Chech Republic" },
                    { 3, "Cracow", "Poland" },
                    { 4, "Gdansk", "Poland" },
                    { 5, "Berlin", "Germany" },
                    { 6, "Paris", "France" }
                });

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

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocationId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_LocationId",
                table: "CatalogItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationSupplier_SuppliersId",
                table: "LocationSupplier",
                column: "SuppliersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Locations_LocationId",
                table: "CatalogItems",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Locations_LocationId",
                table: "CatalogItems");

            migrationBuilder.DropTable(
                name: "LocationSupplier");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_LocationId",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CatalogItems");

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
        }
    }
}
