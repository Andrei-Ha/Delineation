using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "D_Acts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "D_Acts");
        }
    }
}
