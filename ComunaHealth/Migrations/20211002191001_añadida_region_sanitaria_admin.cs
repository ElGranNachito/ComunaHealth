using Microsoft.EntityFrameworkCore.Migrations;

namespace ComunaHealth.Migrations
{
    public partial class añadida_region_sanitaria_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionSanitaria",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionSanitaria",
                table: "AspNetUsers");
        }
    }
}
