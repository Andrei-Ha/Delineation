using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class inv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Line04InvNum",
                table: "D_Tces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Line10InvNum",
                table: "D_Tces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PSInvNum",
                table: "D_Tces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TPInvNum",
                table: "D_Tces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TPnum",
                table: "D_Tces",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Line04InvNum",
                table: "D_Tces");

            migrationBuilder.DropColumn(
                name: "Line10InvNum",
                table: "D_Tces");

            migrationBuilder.DropColumn(
                name: "PSInvNum",
                table: "D_Tces");

            migrationBuilder.DropColumn(
                name: "TPInvNum",
                table: "D_Tces");

            migrationBuilder.DropColumn(
                name: "TPnum",
                table: "D_Tces");
        }
    }
}
