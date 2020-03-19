using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class pillar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pillar",
                table: "D_Tces",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100,
                columns: new[] { "FIOnachRod", "Name" },
                values: new object[] { "Булавина Виталия Федоровича", "Пинский Городской" });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200,
                columns: new[] { "FIOnachRod", "Name" },
                values: new object[] { "Забавнюка Владимира Францевича", "Пинский Сельский" });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54300,
                column: "Name",
                value: "Лунинецкий");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54400,
                column: "Name",
                value: "Столинский");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54500,
                column: "Name",
                value: "Ивановский");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54600,
                column: "Name",
                value: "Дрогичинский");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Pillar",
                table: "D_Tces",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100,
                columns: new[] { "FIOnachRod", "Name" },
                values: new object[] { "Булавина В.Ф.", "Пинский Городской РЭС" });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200,
                columns: new[] { "FIOnachRod", "Name" },
                values: new object[] { "Забавнюка В.Ф.", "Пинский Сельский РЭС" });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54300,
                column: "Name",
                value: "Лунинецкий РЭС");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54400,
                column: "Name",
                value: "Столинский РЭС");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54500,
                column: "Name",
                value: "Ивановский РЭС");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54600,
                column: "Name",
                value: "Дрогичинский РЭС");
        }
    }
}
