using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunaHealth.Migrations
{
    public partial class prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacientesMedicos",
                columns: table => new
                {
                    IdPaciente = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdMedico = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PacienteId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientesMedicos", x => new { x.IdMedico, x.IdPaciente });
                    table.ForeignKey(
                        name: "FK_PacientesMedicos_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientesMedicos_AspNetUsers_PacienteId",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacientesMedicos_PacienteId",
                table: "PacientesMedicos",
                column: "PacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientesMedicos");
        }
    }
}
