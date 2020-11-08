using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carti",
                columns: table => new
                {
                    IdCarte = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titlu = table.Column<string>(nullable: false),
                    Cod = table.Column<string>(nullable: false),
                    Autor = table.Column<string>(nullable: true),
                    Editura = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carti", x => x.IdCarte);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nume = table.Column<string>(nullable: false),
                    Adresa = table.Column<string>(nullable: false),
                    Contact = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "ImprumuturiCarti",
                columns: table => new
                {
                    IdImprumut = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCarte = table.Column<int>(nullable: false),
                    IdClient = table.Column<int>(nullable: false),
                    DataImprumut = table.Column<DateTime>(nullable: false),
                    DataReturnare = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprumuturiCarti", x => x.IdImprumut);
                    table.ForeignKey(
                        name: "FK_ImprumuturiCarti_Carti_IdCarte",
                        column: x => x.IdCarte,
                        principalTable: "Carti",
                        principalColumn: "IdCarte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImprumuturiCarti_Clienti_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clienti",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImprumuturiCarti_IdCarte",
                table: "ImprumuturiCarti",
                column: "IdCarte");

            migrationBuilder.CreateIndex(
                name: "IX_ImprumuturiCarti_IdClient",
                table: "ImprumuturiCarti",
                column: "IdClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImprumuturiCarti");

            migrationBuilder.DropTable(
                name: "Carti");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
