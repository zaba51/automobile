using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    public partial class Supplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "CatalogItems");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "CatalogItems",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.europcar.com/_nuxt/img/europcar-signature-green@3x.a2d761a.png", "Europcar" },
                    { 2, "https://www.enterprise.com/content/experience-fragments/ecom/en/footer/master/_jcr_content/root/footer/footer/container/container/image.coreimg.png/1692607172448/logo-enterprise.png", "Enterpsie" }
                });

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "SupplierId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "SupplierId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "SupplierId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_SupplierId",
                table: "CatalogItems",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Suppliers_SupplierId",
                table: "CatalogItems",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Suppliers_SupplierId",
                table: "CatalogItems");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_SupplierId",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "CatalogItems");

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "CatalogItems",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Supplier",
                value: "Europcar");

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Supplier",
                value: "Express");

            migrationBuilder.UpdateData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Supplier",
                value: "Budget");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2023, 12, 30, 15, 40, 42, 130, DateTimeKind.Utc).AddTicks(7629), new DateTime(2024, 1, 1, 15, 40, 42, 130, DateTimeKind.Utc).AddTicks(7636) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BeginTime", "EndTime" },
                values: new object[] { new DateTime(2024, 1, 3, 15, 40, 42, 130, DateTimeKind.Utc).AddTicks(7638), new DateTime(2024, 1, 6, 15, 40, 42, 130, DateTimeKind.Utc).AddTicks(7639) });
        }
    }
}
