using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiru.Web.Migrations
{
    public partial class AgregadoIdExternoEnBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdExterno",
                table: "Bugs",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdExterno",
                table: "Bugs");
        }
    }
}
