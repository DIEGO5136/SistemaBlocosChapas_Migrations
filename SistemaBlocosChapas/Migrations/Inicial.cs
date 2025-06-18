
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBlocosChapas.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blocos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySqlValueGenerationStrategy.IdentityColumn),
                    Espressura = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Metragem = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NotaFiscal = table.Column<string>(type: "longtext", nullable: false),
                    ArmazemId = table.Column<int>(type: "int", nullable: false),
                    TipoBlocoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySql.EntityFrameworkCore.Metadata.MySqlValueGenerationStrategy.IdentityColumn),
                    BlocoOrigemId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false),
                    Dimensoes = table.Column<string>(type: "longtext", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Usuarios");
            migrationBuilder.DropTable(name: "Blocos");
            migrationBuilder.DropTable(name: "Chapas");
        }
    }
}
