using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMG_Med1000_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Marques",
                columns: table => new
                {
                    MarqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    modeleModelId = table.Column<int>(type: "int", nullable: false),
                    NomMarq = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marques", x => x.MarqId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modeles",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nomModele = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anneeModele = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MarqId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modeles", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Modeles_Marques_MarqId",
                        column: x => x.MarqId,
                        principalTable: "Marques",
                        principalColumn: "MarqId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Voitures",
                columns: table => new
                {
                    VoitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    marqueMarqId = table.Column<int>(type: "int", nullable: false),
                    statutVoiture = table.Column<int>(type: "int", nullable: false),
                    photoVoiture = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descrVoiture = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anneeVoiture = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MarqId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voitures", x => x.VoitId);
                    table.ForeignKey(
                        name: "FK_Voitures_Marques_marqueMarqId",
                        column: x => x.marqueMarqId,
                        principalTable: "Marques",
                        principalColumn: "MarqId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_modeleModelId",
                table: "Marques",
                column: "modeleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_NomMarq",
                table: "Marques",
                column: "NomMarq",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modeles_MarqId",
                table: "Modeles",
                column: "MarqId");

            migrationBuilder.CreateIndex(
                name: "IX_Modeles_nomModele",
                table: "Modeles",
                column: "nomModele",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voitures_marqueMarqId",
                table: "Voitures",
                column: "marqueMarqId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marques_Modeles_modeleModelId",
                table: "Marques",
                column: "modeleModelId",
                principalTable: "Modeles",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marques_Modeles_modeleModelId",
                table: "Marques");

            migrationBuilder.DropTable(
                name: "Voitures");

            migrationBuilder.DropTable(
                name: "Modeles");

            migrationBuilder.DropTable(
                name: "Marques");
        }
    }
}
