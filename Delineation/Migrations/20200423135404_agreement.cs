using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class agreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_Agreement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    Accept = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Agreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Agreement_D_Act_ActId",
                        column: x => x.ActId,
                        principalTable: "D_Act",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_D_Agreement_D_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Agreement_ActId",
                table: "D_Agreement",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Agreement_PersonId",
                table: "D_Agreement",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_Agreement");
        }
    }
}
