using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class conversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Model_ModelId",
                table: "CatalogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "Gear",
                table: "Model");

            migrationBuilder.RenameTable(
                name: "Model",
                newName: "Models");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "CatalogItems",
                newName: "Supplier");

            migrationBuilder.AlterColumn<string>(
                name: "Engine",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Color", "Company", "DoorCount", "Engine", "ImageUrl", "Name", "Power", "SeatCount" },
                values: new object[,]
                {
                    { 1, "White", "FIAT", 5, "HYBRID", "https://flib.carshow360.net/700/900/705993b922662ed39c6.webp", "Panda", 69, 5 },
                    { 2, "Silver", "TOYOTA", 5, "HYBRID", "https://cfm.pl/wp-content/uploads/2021/04/toyota-corolla-sd-srebrna-dlugoterminowy-glowne-cfm.jpg", "Corolla", 140, 5 },
                    { 3, "White", "TOYOTA", 5, "GASOLINE", "https://media.ed.edmunds-media.com/toyota/corolla/2023/oem/2023_toyota_corolla_sedan_xse_fq_oem_1_600.jpg", "Corolla", 140, 6 }
                });

            migrationBuilder.InsertData(
                table: "CatalogItems",
                columns: new[] { "Id", "ModelId", "Price", "Supplier" },
                values: new object[,]
                {
                    { 1, 1, 50.0, "Europcar" },
                    { 2, 2, 80.0, "Express" },
                    { 3, 3, 60.0, "Budget" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Models_ModelId",
                table: "CatalogItems",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_Models_ModelId",
                table: "CatalogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.DeleteData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Supplier",
                table: "CatalogItems",
                newName: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "Engine",
                table: "Model",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Gear",
                table: "Model",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                table: "Model",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_Model_ModelId",
                table: "CatalogItems",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
