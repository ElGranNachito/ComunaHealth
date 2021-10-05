using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunaHealth.Migrations
{
    public partial class cita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TICitaMedico");

            migrationBuilder.DropTable(
                name: "TICitaPaciente");

            migrationBuilder.DropTable(
                name: "TICitaSolicitudCambioHorarioCita");

            migrationBuilder.DropTable(
                name: "TIUsuarioNoAdministradorSolicitudCambioHorarioCita");

            migrationBuilder.AddColumn<int>(
                name: "IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "ModeloCita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "ModeloCita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloSolicitudCambioHorarioDeCita_IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita",
                column: "IdCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloSolicitudCambioHorarioDeCita_SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita",
                column: "SolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloCita_MedicoId",
                table: "ModeloCita",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloCita_PacienteId",
                table: "ModeloCita",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloCita_AspNetUsers_MedicoId",
                table: "ModeloCita",
                column: "MedicoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloCita_AspNetUsers_PacienteId",
                table: "ModeloCita",
                column: "PacienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloSolicitudCambioHorarioDeCita_AspNetUsers_SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita",
                column: "SolicitanteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloSolicitudCambioHorarioDeCita_ModeloCita_IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita",
                column: "IdCita",
                principalTable: "ModeloCita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModeloCita_AspNetUsers_MedicoId",
                table: "ModeloCita");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloCita_AspNetUsers_PacienteId",
                table: "ModeloCita");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloSolicitudCambioHorarioDeCita_AspNetUsers_SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloSolicitudCambioHorarioDeCita_ModeloCita_IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropIndex(
                name: "IX_ModeloSolicitudCambioHorarioDeCita_IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropIndex(
                name: "IX_ModeloSolicitudCambioHorarioDeCita_SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropIndex(
                name: "IX_ModeloCita_MedicoId",
                table: "ModeloCita");

            migrationBuilder.DropIndex(
                name: "IX_ModeloCita_PacienteId",
                table: "ModeloCita");

            migrationBuilder.DropColumn(
                name: "IdCita",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropColumn(
                name: "SolicitanteId",
                table: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "ModeloCita");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "ModeloCita");

            migrationBuilder.CreateTable(
                name: "TICitaMedico",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICitaMedico", x => new { x.IdCita, x.IdMedico });
                    table.ForeignKey(
                        name: "FK_TICitaMedico_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TICitaMedico_ModeloCita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "ModeloCita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICitaPaciente",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICitaPaciente", x => new { x.IdPaciente, x.IdCita });
                    table.ForeignKey(
                        name: "FK_TICitaPaciente_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TICitaPaciente_ModeloCita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "ModeloCita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICitaSolicitudCambioHorarioCita",
                columns: table => new
                {
                    IdSolicitudCambioHorarioDeCita = table.Column<int>(type: "int", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TICitaSolicitudCambioHorarioCita", x => new { x.IdSolicitudCambioHorarioDeCita, x.IdCita });
                    table.ForeignKey(
                        name: "FK_TICitaSolicitudCambioHorarioCita_ModeloCita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "ModeloCita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TICitaSolicitudCambioHorarioCita_ModeloSolicitudCambioHorarioDeCita_IdSolicitudCambioHorarioDeCita",
                        column: x => x.IdSolicitudCambioHorarioDeCita,
                        principalTable: "ModeloSolicitudCambioHorarioDeCita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIUsuarioNoAdministradorSolicitudCambioHorarioCita",
                columns: table => new
                {
                    IdSolicitudPostergacionCita = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioNoAdministrador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIUsuarioNoAdministradorSolicitudCambioHorarioCita", x => new { x.IdSolicitudPostergacionCita, x.IdUsuarioNoAdministrador });
                    table.ForeignKey(
                        name: "FK_TIUsuarioNoAdministradorSolicitudCambioHorarioCita_AspNetUsers_IdUsuarioNoAdministrador",
                        column: x => x.IdUsuarioNoAdministrador,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIUsuarioNoAdministradorSolicitudCambioHorarioCita_ModeloSolicitudCambioHorarioDeCita_IdSolicitudPostergacionCita",
                        column: x => x.IdSolicitudPostergacionCita,
                        principalTable: "ModeloSolicitudCambioHorarioDeCita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TICitaMedico_IdCita",
                table: "TICitaMedico",
                column: "IdCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICitaMedico_IdMedico",
                table: "TICitaMedico",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_TICitaPaciente_IdCita",
                table: "TICitaPaciente",
                column: "IdCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICitaSolicitudCambioHorarioCita_IdCita",
                table: "TICitaSolicitudCambioHorarioCita",
                column: "IdCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TICitaSolicitudCambioHorarioCita_IdSolicitudCambioHorarioDeCita",
                table: "TICitaSolicitudCambioHorarioCita",
                column: "IdSolicitudCambioHorarioDeCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIUsuarioNoAdministradorSolicitudCambioHorarioCita_IdSolicitudPostergacionCita",
                table: "TIUsuarioNoAdministradorSolicitudCambioHorarioCita",
                column: "IdSolicitudPostergacionCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TIUsuarioNoAdministradorSolicitudCambioHorarioCita_IdUsuarioNoAdministrador",
                table: "TIUsuarioNoAdministradorSolicitudCambioHorarioCita",
                column: "IdUsuarioNoAdministrador");
        }
    }
}
