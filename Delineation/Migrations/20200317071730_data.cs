using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "D_Persons",
                columns: new[] { "Id", "Name", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 2, "Виталий", "Федорович", "Булавин" },
                    { 3, "Александр", "Иванович", "Литвинчук" },
                    { 4, "Андрей", "Михайлович", "Германович" },
                    { 5, "Татьяна", "Вячеславовна", "Велесницкая" },
                    { 6, "Владимир", "Францевич", "Забавнюк" },
                    { 7, "Федор", "Иванович", "Калилец" },
                    { 8, "Анатолий", "Леонидович", "Климович" },
                    { 9, "Сиана", "Владимировна", "Крейдич" }
                });

            migrationBuilder.InsertData(
                table: "D_Reses",
                columns: new[] { "Id", "BuhId", "GlInzhId", "NachId", "Name", "ZamNachId" },
                values: new object[,]
                {
                    { 54100, 1, 1, 1, "Пинский Городской РЭС", 1 },
                    { 54200, 1, 1, 1, "Пинский Сельский РЭС", 1 },
                    { 54300, 1, 1, 1, "Лунинецкий РЭС", 1 },
                    { 54400, 1, 1, 1, "Столинский РЭС", 1 },
                    { 54500, 1, 1, 1, "Ивановский РЭС", 1 },
                    { 54600, 1, 1, 1, "Дрогичинский РЭС", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "D_Persons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54300);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54400);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54500);

            migrationBuilder.DeleteData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54600);
        }
    }
}
