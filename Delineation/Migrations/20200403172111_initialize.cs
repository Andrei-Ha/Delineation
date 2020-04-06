using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
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
                    BuhId = table.Column<int>(nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RESa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RESom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FIOnachRod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Dover = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
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
                    Id = table.Column<int>(nullable: false),
                    Num = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ResId = table.Column<int>(nullable: true),
                    Company = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ObjName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Pow = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Category2 = table.Column<int>(nullable: false),
                    PS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Line10 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Line04 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pillar = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
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

            migrationBuilder.InsertData(
                table: "D_Persons",
                columns: new[] { "Id", "Name", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 1, "-", "-", "-" },
                    { 2, "Виталий", "Федорович", "Булавин" },
                    { 3, "Александр", "Иванович", "Литвинчук" },
                    { 4, "Андрей", "Михайлович", "Германович" },
                    { 5, "Татьяна", "Вячеславовна", "Велесницкая" },
                    { 6, "Владимир", "Францевич", "Забавнюк" },
                    { 7, "Федор", "Иванович", "Калилец" },
                    { 8, "Анатолий", "Леонидович", "Климович" },
                    { 9, "Сиана", "Владимировна", "Крейдич" }
                });

            migrationBuilder.InsertData(
                table: "D_Reses",
                columns: new[] { "Id", "BuhId", "City", "Dover", "FIOnachRod", "GlInzhId", "NachId", "Name", "RESa", "RESom", "ZamNachId" },
                values: new object[,]
                {
                    { 543000, 1, null, null, null, 1, 1, "Лунинецкий", null, null, 1 },
                    { 544000, 1, null, null, null, 1, 1, "Столинский", null, null, 1 },
                    { 545000, 1, null, null, null, 1, 1, "Ивановский", null, null, 1 },
                    { 546000, 1, null, null, null, 1, 1, "Дрогичинский", null, null, 1 },
                    { 541000, 5, "Пинск", "от 01.09.2019 №2432", "Булавина Виталия Федоровича", 3, 2, "Пинский Городской", "Пинского Городского", "Пинским Городским", 4 },
                    { 542000, 9, "Пинск", "от 01.09.2019 №2432", "Забавнюка Владимира Францевича", 8, 6, "Пинский Сельский", "Пинсого Сельского", "Пинским Сельским", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Act_TcId",
                table: "D_Act",
                column: "TcId");

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
                name: "D_Act");

            migrationBuilder.DropTable(
                name: "D_Tces");

            migrationBuilder.DropTable(
                name: "D_Reses");

            migrationBuilder.DropTable(
                name: "D_Persons");
        }
    }
}
