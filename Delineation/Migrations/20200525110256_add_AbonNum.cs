using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class add_AbonNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AbonNum",
                table: "D_Tces",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbonNum",
                table: "D_Tces");
        }
    }
}
