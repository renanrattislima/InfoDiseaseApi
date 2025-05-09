using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arbovirose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemanaInicio = table.Column<int>(type: "int", nullable: false),
                    SemanaFim = table.Column<int>(type: "int", nullable: false),
                    AnoInicio = table.Column<int>(type: "int", nullable: false),
                    AnoFim = table.Column<int>(type: "int", nullable: false),
                    CodigoIBGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCasos = table.Column<int>(type: "int", nullable: false),
                    SolicitanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relatorios_Solicitantes_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Solicitantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadoEpidemiologicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicioSemana = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SemanaEpidemiologica = table.Column<int>(type: "int", nullable: false),
                    Casos = table.Column<double>(type: "float", nullable: false),
                    CasosEstimados = table.Column<double>(type: "float", nullable: true),
                    Rt = table.Column<double>(type: "float", nullable: true),
                    NotificacoesAcumuladasAno = table.Column<int>(type: "int", nullable: false),
                    RelatorioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadoEpidemiologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadoEpidemiologicos_Relatorios_RelatorioId",
                        column: x => x.RelatorioId,
                        principalTable: "Relatorios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadoEpidemiologicos_RelatorioId",
                table: "DadoEpidemiologicos",
                column: "RelatorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_SolicitanteId",
                table: "Relatorios",
                column: "SolicitanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadoEpidemiologicos");

            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Solicitantes");
        }
    }
}
