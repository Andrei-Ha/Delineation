using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class act : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_Act",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    TcId = table.Column<int>(nullable: true),
                    IsEntity = table.Column<bool>(nullable: false),
                    EntityDoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConsBalance = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DevBalance = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ConsExpl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DevExpl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsTransit = table.Column<bool>(nullable: false),
                    FIOtrans = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Validity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Act", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Act_D_Tces_TcId",
                        column: x => x.TcId,
                        principalTable: "D_Tces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100,
                columns: new[] { "BuhId", "GlInzhId", "NachId", "ZamNachId" },
                values: new object[] { 5, 3, 2, 4 });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200,
                columns: new[] { "BuhId", "GlInzhId", "NachId", "ZamNachId" },
                values: new object[] { 9, 8, 6, 7 });

            migrationBuilder.CreateIndex(
                name: "IX_D_Act_TcId",
                table: "D_Act",
                column: "TcId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_Act");

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54100,
                columns: new[] { "BuhId", "GlInzhId", "NachId", "ZamNachId" },
                values: new object[] { 1, 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "D_Reses",
                keyColumn: "Id",
                keyValue: 54200,
                columns: new[] { "BuhId", "GlInzhId", "NachId", "ZamNachId" },
                values: new object[] { 1, 1, 1, 1 });
        }
    }
}
