using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class agreement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "D_Agreement",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "D_Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "D_Agreement",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "D_Agreement");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "D_Agreement");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "D_Agreement");
        }
    }
}
