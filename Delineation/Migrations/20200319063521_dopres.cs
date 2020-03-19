using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class dopres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "D_Reses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dover",
                table: "D_Reses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FIOnachRod",
                table: "D_Reses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RESa",
                table: "D_Reses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RESom",
                table: "D_Reses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "D_Persons",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "D_Persons",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "D_Persons",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100,
                columns: new[] { "City", "Dover", "FIOnachRod", "RESa", "RESom" },
                values: new object[] { "Пинск", "от 01.09.2019 №2432", "Булавина В.Ф.", "Пинского Городского", "Пинским Городским" });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200,
                columns: new[] { "City", "Dover", "FIOnachRod", "RESa", "RESom" },
                values: new object[] { "Пинск", "от 01.09.2019 №2432", "Забавнюка В.Ф.", "Пинсого Сельского", "Пинским Сельским" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "Dover",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "FIOnachRod",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "RESa",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "RESom",
                table: "D_Reses");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "D_Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "D_Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "D_Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true);
        }
    }
}
