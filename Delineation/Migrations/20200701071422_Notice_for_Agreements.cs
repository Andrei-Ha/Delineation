using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class Notice_for_Agreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notice",
                table: "D_Agreements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notice",
                table: "D_Agreements");
        }
    }
}
