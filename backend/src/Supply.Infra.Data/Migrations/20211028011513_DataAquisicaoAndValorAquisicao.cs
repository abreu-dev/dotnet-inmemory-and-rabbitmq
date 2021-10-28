using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Supply.Infra.Data.Migrations
{
    public partial class DataAquisicaoAndValorAquisicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAquisicao",
                table: "Veiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "ValorAquisicao",
                table: "Veiculo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAquisicao",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "ValorAquisicao",
                table: "Veiculo");
        }
    }
}
