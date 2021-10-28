using Microsoft.EntityFrameworkCore.Migrations;

namespace Supply.Infra.Data.Migrations
{
    public partial class FieldCodigo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "VeiculoModelo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "VeiculoMarca",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "VeiculoModelo");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "VeiculoMarca");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Veiculo");
        }
    }
}
