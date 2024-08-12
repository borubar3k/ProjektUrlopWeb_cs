using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektUrlopWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id_num = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsArch = table.Column<bool>(type: "bit", nullable: false),
                    DataZatr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DniUrlopu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id_num);
                });

            migrationBuilder.CreateTable(
                name: "Logi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracownikId_num = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logi_Pracownicy_PracownikId_num",
                        column: x => x.PracownikId_num,
                        principalTable: "Pracownicy",
                        principalColumn: "Id_num",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urlopy",
                columns: table => new
                {
                    Id_num = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracownikId_num = table.Column<int>(type: "int", nullable: false),
                    DataRozp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZak = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IloscDni = table.Column<int>(type: "int", nullable: false),
                    Rodzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAppr = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urlopy", x => x.Id_num);
                    table.ForeignKey(
                        name: "FK_Urlopy_Pracownicy_PracownikId_num",
                        column: x => x.PracownikId_num,
                        principalTable: "Pracownicy",
                        principalColumn: "Id_num",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logi_PracownikId_num",
                table: "Logi",
                column: "PracownikId_num");

            migrationBuilder.CreateIndex(
                name: "IX_Urlopy_PracownikId_num",
                table: "Urlopy",
                column: "PracownikId_num");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logi");

            migrationBuilder.DropTable(
                name: "Urlopy");

            migrationBuilder.DropTable(
                name: "Pracownicy");
        }
    }
}
