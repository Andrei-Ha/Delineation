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
                    Num = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ResId = table.Column<int>(nullable: true),
                    Company = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ObjName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Pow = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Point = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
