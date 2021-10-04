using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunaHealth.Migrations
{
    public partial class prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_ModeloUsuarioId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ModeloUsuarioId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ModeloUsuarioId",
                table: "Chats");

            migrationBuilder.CreateTable(
                name: "ModeloChatModeloUsuario",
                columns: table => new
                {
                    ChatsId = table.Column<int>(type: "int", nullable: false),
                    ParticipantesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloChatModeloUsuario", x => new { x.ChatsId, x.ParticipantesId });
                    table.ForeignKey(
                        name: "FK_ModeloChatModeloUsuario_AspNetUsers_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModeloChatModeloUsuario_Chats_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModeloChatModeloUsuario_ParticipantesId",
                table: "ModeloChatModeloUsuario",
                column: "ParticipantesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeloChatModeloUsuario");

            migrationBuilder.AddColumn<int>(
                name: "ModeloUsuarioId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ModeloUsuarioId",
                table: "Chats",
                column: "ModeloUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_ModeloUsuarioId",
                table: "Chats",
                column: "ModeloUsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
