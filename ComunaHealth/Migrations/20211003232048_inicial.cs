using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunaHealth.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecializacionRepresentada = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    TiposCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoCuenta = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionSanitaria = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MailEsPublico = table.Column<bool>(type: "bit", nullable: true),
                    TelefonoEsPublico = table.Column<bool>(type: "bit", nullable: true),
                    FotoDePerfil = table.Column<byte[]>(type: "image", nullable: true),
                    FotoAnversoDNI = table.Column<byte[]>(type: "image", nullable: true),
                    FotoReversoDNI = table.Column<byte[]>(type: "image", nullable: true),
                    Municipio = table.Column<int>(type: "int", nullable: true),
                    Especializaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.UniqueConstraint("AK_AspNetUsers_DNI", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser<int>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser<int>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloCita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspecializacionCita = table.Column<int>(type: "int", nullable: false),
                    EstadoCita = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloCita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContenedorDeEntradas<ModeloEntrada>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PuedeSerModificado = table.Column<bool>(type: "bit", nullable: false),
                    ClaveEncriptado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContenedorDeEntradas<ModeloEntrada>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PuedeSerModificado = table.Column<bool>(type: "bit", nullable: false),
                    ClaveEncriptado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloSolicitudCambioHorarioDeCita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NuevaDuracion = table.Column<int>(type: "int", nullable: false),
                    NuevaFecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloSolicitudCambioHorarioDeCita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuidChat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModeloUsuarioId = table.Column<int>(type: "int", nullable: true),
                    PuedeSerModificado = table.Column<bool>(type: "bit", nullable: false),
                    ClaveEncriptado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_ModeloUsuarioId",
                        column: x => x.ModeloUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloMedicoModeloPaciente",
                columns: table => new
                {
                    MedicosId = table.Column<int>(type: "int", nullable: false),
                    PacientesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloMedicoModeloPaciente", x => new { x.MedicosId, x.PacientesId });
                    table.ForeignKey(
                        name: "FK_ModeloMedicoModeloPaciente_AspNetUsers_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloMedicoModeloPaciente_AspNetUsers_PacientesId",
                        column: x => x.PacientesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

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
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
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
                name: "TIMedicoContenedorEntrada",
                columns: table => new
                {
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    IdEntrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMedicoContenedorEntrada", x => new { x.IdMedico, x.IdEntrada });
                    table.ForeignKey(
                        name: "FK_TIMedicoContenedorEntrada_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIMedicoContenedorEntrada_ModeloContenedorDeEntradas<ModeloEntrada>_IdEntrada",
                        column: x => x.IdEntrada,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntrada>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPacienteContenedorEntrada",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdContenedorEntrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPacienteContenedorEntrada", x => new { x.IdPaciente, x.IdContenedorEntrada });
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntrada_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntrada_ModeloContenedorDeEntradas<ModeloEntrada>_IdContenedorEntrada",
                        column: x => x.IdContenedorEntrada,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntrada>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPacienteContenedorEntradaCambiosEstiloVida",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdContenedorEntrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPacienteContenedorEntradaCambiosEstiloVida", x => new { x.IdPaciente, x.IdContenedorEntrada });
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntradaCambiosEstiloVida_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntradaCambiosEstiloVida_ModeloContenedorDeEntradas<ModeloEntrada>_IdContenedorEntrada",
                        column: x => x.IdContenedorEntrada,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntrada>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIMedicoContenedorEntradaHistorialMedico",
                columns: table => new
                {
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    IdEntradaHistorialMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMedicoContenedorEntradaHistorialMedico", x => new { x.IdMedico, x.IdEntradaHistorialMedico });
                    table.ForeignKey(
                        name: "FK_TIMedicoContenedorEntradaHistorialMedico_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIMedicoContenedorEntradaHistorialMedico_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>_IdEntradaHistorialMedico",
                        column: x => x.IdEntradaHistorialMedico,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIPacienteContenedorEntradaHistorialMedico",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdEntradaHistorialMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPacienteContenedorEntradaHistorialMedico", x => new { x.IdPaciente, x.IdEntradaHistorialMedico });
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntradaHistorialMedico_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIPacienteContenedorEntradaHistorialMedico_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>_IdEntradaHistorialMedico",
                        column: x => x.IdEntradaHistorialMedico,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TICitaSolicitudCambioHorarioCita",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdSolicitudCambioHorarioDeCita = table.Column<int>(type: "int", nullable: false)
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
                    IdUsuarioNoAdministrador = table.Column<int>(type: "int", nullable: false),
                    IdSolicitudPostergacionCita = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ModeloEntrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    FechaDeCreacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloContenedorDeEntradasModeloEntradaId = table.Column<int>(name: "ModeloContenedorDeEntradas<ModeloEntrada>Id", type: "int", nullable: true),
                    MedicoCreadorId = table.Column<int>(type: "int", nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    Especializacion = table.Column<int>(type: "int", nullable: true),
                    ModeloContenedorDeEntradasModeloEntradaHistorialMedicoId = table.Column<int>(name: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>Id", type: "int", nullable: true),
                    RemitenteId = table.Column<int>(type: "int", nullable: true),
                    ModeloChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloEntrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_AspNetUsers_MedicoCreadorId",
                        column: x => x.MedicoCreadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_AspNetUsers_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_AspNetUsers_RemitenteId",
                        column: x => x.RemitenteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_Chats_ModeloChatId",
                        column: x => x.ModeloChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_ModeloContenedorDeEntradas<ModeloEntrada>_ModeloContenedorDeEntradas<ModeloEntrada>Id",
                        column: x => x.ModeloContenedorDeEntradasModeloEntradaId,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntrada>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModeloEntrada_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedi~",
                        column: x => x.ModeloContenedorDeEntradasModeloEntradaHistorialMedicoId,
                        principalTable: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ModeloUsuarioId",
                table: "Chats",
                column: "ModeloUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_MedicoCreadorId",
                table: "ModeloEntrada",
                column: "MedicoCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_ModeloChatId",
                table: "ModeloEntrada",
                column: "ModeloChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_ModeloContenedorDeEntradas<ModeloEntrada>Id",
                table: "ModeloEntrada",
                column: "ModeloContenedorDeEntradas<ModeloEntrada>Id");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>Id",
                table: "ModeloEntrada",
                column: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>Id");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_PacienteId",
                table: "ModeloEntrada",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloEntrada_RemitenteId",
                table: "ModeloEntrada",
                column: "RemitenteId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloMedicoModeloPaciente_PacientesId",
                table: "ModeloMedicoModeloPaciente",
                column: "PacientesId");

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
                name: "IX_TIMedicoContenedorEntrada_IdEntrada",
                table: "TIMedicoContenedorEntrada",
                column: "IdEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_TIMedicoContenedorEntradaHistorialMedico_IdEntradaHistorialMedico",
                table: "TIMedicoContenedorEntradaHistorialMedico",
                column: "IdEntradaHistorialMedico");

            migrationBuilder.CreateIndex(
                name: "IX_TIPacienteContenedorEntrada_IdContenedorEntrada",
                table: "TIPacienteContenedorEntrada",
                column: "IdContenedorEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_TIPacienteContenedorEntradaCambiosEstiloVida_IdContenedorEntrada",
                table: "TIPacienteContenedorEntradaCambiosEstiloVida",
                column: "IdContenedorEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_TIPacienteContenedorEntradaHistorialMedico_IdEntradaHistorialMedico",
                table: "TIPacienteContenedorEntradaHistorialMedico",
                column: "IdEntradaHistorialMedico");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "IdentityUser<int>");

            migrationBuilder.DropTable(
                name: "ModeloEntrada");

            migrationBuilder.DropTable(
                name: "ModeloMedicoModeloPaciente");

            migrationBuilder.DropTable(
                name: "TICitaMedico");

            migrationBuilder.DropTable(
                name: "TICitaPaciente");

            migrationBuilder.DropTable(
                name: "TICitaSolicitudCambioHorarioCita");

            migrationBuilder.DropTable(
                name: "TIMedicoContenedorEntrada");

            migrationBuilder.DropTable(
                name: "TIMedicoContenedorEntradaHistorialMedico");

            migrationBuilder.DropTable(
                name: "TIPacienteContenedorEntrada");

            migrationBuilder.DropTable(
                name: "TIPacienteContenedorEntradaCambiosEstiloVida");

            migrationBuilder.DropTable(
                name: "TIPacienteContenedorEntradaHistorialMedico");

            migrationBuilder.DropTable(
                name: "TIUsuarioNoAdministradorSolicitudCambioHorarioCita");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "ModeloCita");

            migrationBuilder.DropTable(
                name: "ModeloContenedorDeEntradas<ModeloEntrada>");

            migrationBuilder.DropTable(
                name: "ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>");

            migrationBuilder.DropTable(
                name: "ModeloSolicitudCambioHorarioDeCita");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
