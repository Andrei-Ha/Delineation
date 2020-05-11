using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Delineation.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Position", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "D_Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Kod_long = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Linom = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Persons_D_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "D_Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Company = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    ObjName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Pow = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Category2 = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    PS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PSInvNum = table.Column<int>(nullable: false),
                    Line10 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Line10InvNum = table.Column<int>(nullable: false),
                    TP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TPnum = table.Column<int>(nullable: false),
                    TPInvNum = table.Column<int>(nullable: false),
                    Line04 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Line04InvNum = table.Column<int>(nullable: false),
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
                name: "D_Acts",
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
                    Validity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<int>(nullable: false, defaultValue: 0),
                    StrPSline10 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Acts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Acts_D_Tces_TcId",
                        column: x => x.TcId,
                        principalTable: "D_Tces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    Accept = table.Column<bool>(nullable: false, defaultValue: false),
                    Note = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Info = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_Agreements_D_Acts_ActId",
                        column: x => x.ActId,
                        principalTable: "D_Acts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_D_Agreements_D_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "D_Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "D_Position",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Начальник РЭС" },
                    { 2, "Зам. начальника РЭС" },
                    { 3, "Главный инженер РЭС" },
                    { 4, "Бухгалтер РЭС" },
                    { 5, "Главный инженер ВРЭС" },
                    { 6, "Начальник ВРЭС" },
                    { 7, "Старший мастер ССЭЭ" },
                    { 8, "Бухгалтер по учёту ОС" },
                    { 9, "Начальник ОДС" },
                    { 10, "Начальник СРС" },
                    { 11, "Начальник ССЭЭ" },
                    { 12, "Главный бухгалтер" }
                });

            migrationBuilder.InsertData(
                table: "D_Persons",
                columns: new[] { "Id", "Kod_long", "Linom", "Name", "Patronymic", "PositionId", "Surname" },
                values: new object[,]
                {
                    { 1, "542000", "505", "Владимир", "Францевич", 1, "Забавнюк" },
                    { 2, "542000", "1785", "Федор", "Иванович", 2, "Калилец" },
                    { 3, "542000", "1672", "Анатолий", "Леонидович", 3, "Климович" },
                    { 4, "542000", "2149", "Сиана", "Владимировна", 4, "Крейдич" }
                });

            migrationBuilder.InsertData(
                table: "D_Reses",
                columns: new[] { "Id", "BuhId", "City", "Dover", "FIOnachRod", "GlInzhId", "NachId", "Name", "RESa", "RESom", "ZamNachId" },
                values: new object[,]
                {
                    { 541000, 1, "Пинск", "от 01.09.2019 №2432", "Булавина Виталия Федоровича", 1, 1, "Пинский Городской", "Пинского Городского", "Пинским Городским", 1 },
                    { 543000, 1, null, null, null, 1, 1, "Лунинецкий", null, null, 1 },
                    { 544000, 1, null, null, null, 1, 1, "Столинский", null, null, 1 },
                    { 545000, 1, null, null, null, 1, 1, "Ивановский", null, null, 1 },
                    { 546000, 1, null, null, null, 1, 1, "Дрогичинский", null, null, 1 },
                    { 542000, 4, "Пинск", "от 01.09.2019 №2432", "Забавнюка Владимира Францевича", 3, 1, "Пинский Сельский", "Пинского Сельского", "Пинским Сельским", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Acts_TcId",
                table: "D_Acts",
                column: "TcId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Agreements_ActId",
                table: "D_Agreements",
                column: "ActId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Agreements_PersonId",
                table: "D_Agreements",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_D_Persons_PositionId",
                table: "D_Persons",
                column: "PositionId");

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
                name: "D_Agreements");

            migrationBuilder.DropTable(
                name: "sprpodr");

            migrationBuilder.DropTable(
                name: "D_Acts");

            migrationBuilder.DropTable(
                name: "D_Tces");

            migrationBuilder.DropTable(
                name: "D_Reses");

            migrationBuilder.DropTable(
                name: "D_Persons");

            migrationBuilder.DropTable(
                name: "D_Position");
        }
    }
}
