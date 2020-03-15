using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class d_tc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_Tces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<string>(type: "varchar(20)", nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    FIO = table.Column<string>(type: "varchar(50)", nullable: true),
                    ResId = table.Column<int>(nullable: true),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    Pow = table.Column<string>(type: "varchar(7)", nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Point = table.Column<string>(type: "varchar(50)", nullable: true),
                    InvNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Tces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Tces_D_Reses_ResId",
                        column: x => x.ResId,
                        principalTable: "D_Reses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Tces_ResId",
                table: "D_Tces",
                column: "ResId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_Tces");
        }
    }
}
