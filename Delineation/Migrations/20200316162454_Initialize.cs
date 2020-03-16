using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "D_Reses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NachId = table.Column<int>(nullable: true),
                    ZamNachId = table.Column<int>(nullable: true),
                    GlInzhId = table.Column<int>(nullable: true),
                    BuhId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Reses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Reses_D_Persons_BuhId",
                        column: x => x.BuhId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_D_Reses_D_Persons_GlInzhId",
                        column: x => x.GlInzhId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_D_Reses_D_Persons_NachId",
                        column: x => x.NachId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_D_Reses_D_Persons_ZamNachId",
                        column: x => x.ZamNachId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    InvNum = table.Column<int>(nullable: false),
                    Pillar = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Tces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Tces_D_Reses_ResId",
                        column: x => x.ResId,
                        principalTable: "D_Reses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "D_Persons",
                columns: new[] { "Id", "Name", "Patronymic", "Surname" },
                values: new object[] { 1, "-", "-", "-" });

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

            migrationBuilder.CreateIndex(
                name: "IX_D_Tces_ResId",
                table: "D_Tces",
                column: "ResId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_Tces");

            migrationBuilder.DropTable(
                name: "D_Reses");

            migrationBuilder.DropTable(
                name: "D_Persons");
        }
    }
}
