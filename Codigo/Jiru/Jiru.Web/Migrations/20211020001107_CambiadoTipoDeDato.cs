using Microsoft.EntityFrameworkCore.Migrations;

namespace Jiru.Web.Migrations
{
    public partial class CambiadoTipoDeDato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CostoPorHora",
                table: "Usuarios",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Desarrollador_CostoPorHora",
                table: "Usuarios",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DuracionHoras",
                table: "Tareas",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "CostoPorHora",
                table: "Tareas",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "DuracionHoras",
                table: "Bugs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostoPorHora",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Desarrollador_CostoPorHora",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DuracionHoras",
                table: "Bugs");

            migrationBuilder.AlterColumn<int>(
                name: "DuracionHoras",
                table: "Tareas",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "CostoPorHora",
                table: "Tareas",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
