using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class Add_D_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_d_Reses",
                table: "d_Reses");

            migrationBuilder.RenameTable(
                name: "d_Reses",
                newName: "D_Reses");

            migrationBuilder.AddColumn<int>(
                name: "BuhId",
                table: "D_Reses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GlInzhId",
                table: "D_Reses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NachId",
                table: "D_Reses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZamNachId",
                table: "D_Reses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_D_Reses",
                table: "D_Reses",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "D_Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Reses_BuhId",
                table: "D_Reses",
                column: "BuhId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Reses_GlInzhId",
                table: "D_Reses",
                column: "GlInzhId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Reses_NachId",
                table: "D_Reses",
                column: "NachId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Reses_ZamNachId",
                table: "D_Reses",
                column: "ZamNachId");

            migrationBuilder.AddForeignKey(
                name: "FK_D_Reses_D_Persons_BuhId",
                table: "D_Reses",
                column: "BuhId",
                principalTable: "D_Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_D_Reses_D_Persons_GlInzhId",
                table: "D_Reses",
                column: "GlInzhId",
                principalTable: "D_Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_D_Reses_D_Persons_NachId",
                table: "D_Reses",
                column: "NachId",
                principalTable: "D_Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_D_Reses_D_Persons_ZamNachId",
                table: "D_Reses",
                column: "ZamNachId",
                principalTable: "D_Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_D_Reses_D_Persons_BuhId",
                table: "D_Reses");

            migrationBuilder.DropForeignKey(
                name: "FK_D_Reses_D_Persons_GlInzhId",
                table: "D_Reses");

            migrationBuilder.DropForeignKey(
                name: "FK_D_Reses_D_Persons_NachId",
                table: "D_Reses");

            migrationBuilder.DropForeignKey(
                name: "FK_D_Reses_D_Persons_ZamNachId",
                table: "D_Reses");

            migrationBuilder.DropTable(
                name: "D_Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_D_Reses",
                table: "D_Reses");

            migrationBuilder.DropIndex(
                name: "IX_D_Reses_BuhId",
                table: "D_Reses");

            migrationBuilder.DropIndex(
                name: "IX_D_Reses_GlInzhId",
                table: "D_Reses");

            migrationBuilder.DropIndex(
                name: "IX_D_Reses_NachId",
                table: "D_Reses");

            migrationBuilder.DropIndex(
                name: "IX_D_Reses_ZamNachId",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "BuhId",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "GlInzhId",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "NachId",
                table: "D_Reses");

            migrationBuilder.DropColumn(
                name: "ZamNachId",
                table: "D_Reses");

            migrationBuilder.RenameTable(
                name: "D_Reses",
                newName: "d_Reses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_d_Reses",
                table: "d_Reses",
                column: "ID");
        }
    }
}
